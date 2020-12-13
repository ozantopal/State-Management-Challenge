using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OT.StateManagement.Domain.Entities;

namespace OT.StateManagement.DataAccess.EF.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Tasks")
                .HasKey(t => new { t.Id });
            builder.HasMany(t => t.StateChanges)
                .WithOne(sc => sc.Task);

            /*
             * I prefer to use postgres as rdbms,
             * therefore prefer to use 'now()' as default value.
             * In case of change datasource type, keep in mind
             * to set proper default sql value. 
             * For example, 'getdate()' for MSSQL Server.
             */
            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("now()");
        }
    }
}
