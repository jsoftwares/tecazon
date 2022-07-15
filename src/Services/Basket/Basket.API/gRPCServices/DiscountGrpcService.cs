using Discount.gRPC.Protos;

namespace Basket.API.gRPCServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            // Create new discount gRPC request, prepare d request of d gRPC service which we will pass to GetDiscountAsync below
            var discountRequest = new GetDiscountRequest() { ProductName = productName };

            //here we consume gRPC as a client
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }

    }
}
