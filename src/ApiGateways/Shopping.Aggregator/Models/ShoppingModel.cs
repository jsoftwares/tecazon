namespace Shopping.Aggregator.Models
{
    /**We use this for the response we are to send to client applications when they send a request with a given username**/
    public class ShoppingModel
    {
        public string UserName { get; set; }
        public BasketModel BasketWithProducts { get; set; }
        public IEnumerable<OrderResponseModel> Orders { get; set; }
    }
}
