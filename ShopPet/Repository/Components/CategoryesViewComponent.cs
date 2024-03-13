using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShopPet.Repository.Components
{
    public class CategoryesViewComponent:ViewComponent
    {
        private readonly DataContext _dataContext;
        public CategoryesViewComponent(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult>InvokeAsync()=>View(await _dataContext.LoaiSanPhams.ToListAsync());
    }
}
