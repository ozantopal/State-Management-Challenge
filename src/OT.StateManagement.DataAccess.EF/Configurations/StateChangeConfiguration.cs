using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.StateManagement.Domain.Entities;

namespace OT.StateManagement.DataAccess.EF.Configurations
{
    public class StateChangeConfiguration : IEntityTypeConfiguration<StateChange>
    {
        public void Configure(EntityTypeBuilder<StateChange> builder)
        {
            builder.ToTable("StateChanges")
                .HasKey(sc => new { sc.Id });
            builder.HasOne(sc => sc.Task)
                .WithMany(t => t.StateChanges);

            /*
             * I prefer to use postgres as rdbms,
             * therefore prefer to use 'now()' as default value.
             * In case of change datasource type, keep in mind
             * to set proper default sql value. 
             * For example, 'getdate()' for MSSQL Server.
             */
            builder.Property(sc => sc.CreatedAt)
                .HasDefaultValueSql("now()");
        }
    }
}
