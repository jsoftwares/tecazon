using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    //In this layer(Infrastruction) we are going to perform DB & email send operations with external systems; this will include d
    //implementation for the absraction in the Application(/Contracts) layer.
    public class OrderContext : DbContext
    {
        //we have no specific options here, but it is required for d Entity Frameworkcore to inherit from d base constructor
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        /**We are overriding entity Framework default savechange() method bcos we want to populate some default values/fields
         * before saving. We see these values in EntityBase which Order in Domain layer inherits from. This is one of the best 
         * practices when you have some of the common fields from the Entities, you can basically override d SaveChanges() from 
         * Entity Framework core & set these common columns before saving d actual entity in DB**/
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            /**Intereates over every entry that inherits from the Entitybase class & look for d state field, if it exist & the entry
             * is a new record, we set the CreatedDate & CreatedBy fields. If the entry is being modified, we set the 
             * LastModified and LastModifiedBy fields before we proceed to d saveChangeAsync() operation ie before saving the actual
             * entity in DB**/
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "jeffonochie";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "jeffonochie";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
