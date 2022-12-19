using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class ProductModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public ProductModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var productList = await _catalogService.GetCatalog();
            //loops through array of product and select d categogy field for each, then return them in an array of string
            CategoryList = productList.Select(p => p.Category).Distinct();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                ProductList = productList.Where(p => p.Category == categoryName);
                SelectedCategory = categoryName;
                //SelectedCategory = CategoryList.FirstOrDefault(c => c.Id == categoryId.Value)?.Name;
            }
            else
            {
                ProductList = productList;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
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