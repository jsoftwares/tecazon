using System.Collections.Generic;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public IndexModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _catalogService.GetCatalog();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            // Get data of product being add from Catalog using product Id
            var product = await _catalogService.GetCatalog(productId);

            // Retrieve the user's current Cart/Basket
            var userName = "jeffonochie";
            var basket = await _basketService.GetBasket(userName);

            //Add new item/product to the user's cart
            basket.Items.Add(new BasketItemModel 
            {
                Quantity = 1,
                Color = "Blue",
                Price = product.Price,
                ProductId = productId,
                ProductName = product.Name
            });

            var updatedBasket = await _basketService.UpdateBasket(basket);

            return RedirectToPage("Cart");
        }
    }
}
