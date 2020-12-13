using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.StateManagement.Domain.Entities;

namespace OT.StateManagement.DataAccess.EF.Configurations
{
    public class FlowConfiguration : IEntityTypeConfiguration<Flow>
    {
        public void Configure(EntityTypeBuilder<Flow> builder)
        {
            builder.ToTable("Flows")
                .HasKey(f => new { f.Id });
            builder.HasMany(f => f.States)
                .WithOne(s => s.Flow);

            /*
             * I prefer to use postgres as rdbms,
             * therefore prefer to use 'now()' as default value.
             * In case of change datasource type, keep in mind
             * to set proper default sql value. 
             * For example, 'getdate()' for MSSQL Server.
             */
            builder.Property(f => f.CreatedAt)
                .HasDefaultValueSql("now()");
        }
    }
}
