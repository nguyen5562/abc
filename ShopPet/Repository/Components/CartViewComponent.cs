using Microsoft.AspNetCore.Mvc;
using ShoPet.Repository;
using ShopPet.Models;
using ShopPet.Models.ViewModels;
using System.Collections.Generic;

namespace ShopPet.Repository.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Lấy danh sách sản phẩm trong giỏ hàng từ Session
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            // Tạo một đối tượng CartItemViewModel
            var cartItemViewModel = new CartItemViewModel
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.soLuong * x.donGia),
            };

            // Trả về ViewComponent với đối tượng CartItemViewModel
            return View(cartItemViewModel);
        }
    }
}
