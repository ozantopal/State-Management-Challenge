using Microsoft.EntityFrameworkCore;
using OT.StateManagement.DataAccess.EF.Configurations;
using OT.StateManagement.Domain.Entities;

namespace OT.StateManagement.DataAccess.EF
{
    public class StateContext : DbContext
    {
        public StateContext(DbContextOptions<StateContext> options)
            : base(options) { }

        public DbSet<Flow> Flows { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<StateChange> StateChanges { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new FlowConfiguration());
            builder.ApplyConfiguration(new StateConfiguration());
            builder.ApplyConfiguration(new TaskConfiguration());
            builder.ApplyConfiguration(new StateChangeConfiguration());
        }
    }
}
