using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.StateManagement.Domain.Entities;

namespace OT.StateManagement.DataAccess.EF.Configurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States")
                .HasKey(s => new { s.Id });
            builder.HasOne(s => s.NextState)
                .WithOne(s => s.PreviousState);
            builder.HasOne(s => s.Flow)
                .WithMany(f => f.States);
            builder.HasMany(s => s.Tasks)
                .WithOne(t => t.State);

            /*
             * I prefer to use postgres as rdbms,
             * therefore prefer to use 'now()' as default value.
             * In case of change datasource type, keep in mind
             * to set proper default sql value. 
             * For example, 'getdate()' for MSSQL Server.
             */
            builder.Property(s => s.CreatedAt)
                .HasDefaultValueSql("now()");
        }
    }
}
