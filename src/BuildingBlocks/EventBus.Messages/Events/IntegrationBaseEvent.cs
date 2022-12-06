/**Cross-cutting-concerns refers to classes/code that is share between multiple services - Basket & Order microservices are are 
 * using RabbitMQ to dispatch & consumes order requests respectively, so we use cross-cutting-concerns here. We create the new
 * Solution Folder under Solutions (same level as Services), then we add a new folder (BuildingBlocks) then add a new project 
 * (class library) in /src/BuildingBlocks
 * This DTO will be common between BasketCheckout event and CreateOrder event**/
namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreateDate { get; private set; }

        //since these properties are d private set; we provide these ctor providing these values with Id & CreateDate
        //if you don't provide parameters to d ctor it generate a new Id & use current Date/Time for parameters.
        //If you provide parameter, d 2nd ctor is invoked & your parameters are used
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
        }
        public IntegrationBaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreateDate = createDate;
        }
    }
}
