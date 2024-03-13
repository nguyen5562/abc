using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShopPet.Repository.Components
{
	public class SupplierViewComponent:ViewComponent
	{
		private readonly DataContext _dataContext;
		public SupplierViewComponent(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.NhaCungCaps.ToListAsync());
	}
}
