using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ApiCollection.Interfaces;
using WebApp.Models;

namespace WebApp
{
    public class OrderModel : PageModel
    {
        private readonly IOrderApi _orderApi;

        public OrderModel(IOrderApi orderApi)
        {
            _orderApi = orderApi ?? throw new ArgumentNullException(nameof(orderApi));
        }

        public IEnumerable<OrderResponse> Orders { get; set; } = new List<OrderResponse>();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "Robin";
            Orders = await _orderApi.GetOrdersByUserName(userName);

            return Page();
        }       
    }
}