using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShopPet.Repository.Components
{
	public class SimilarProductViewComponent:ViewComponent
	{
		private readonly DataContext _dataContext;
		public SimilarProductViewComponent(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.SanPhams.ToListAsync());
	}
}
