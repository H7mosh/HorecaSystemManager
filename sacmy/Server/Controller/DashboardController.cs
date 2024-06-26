using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Shared.ViewModels.DashboardViewModel;
using sacmy.Shared.ViewModels.InvoiceViewModel;

namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;

        public DashboardController(SafeenCompanyDbContext context)
        {
            _context = context;
        }

        /*        [HttpGet("GetBranchSales")]
                public async Task<IActionResult> GetBranchSales(DateTime? startDate, DateTime? endDate)
                {
                    var query = from bf in _context.BuyFatoras
                                join bfi in _context.BuyFatoraItems on bf.Id equals bfi.BuId
                                where (startDate == null || bf.Date >= startDate) &&
                                      (endDate == null || bf.Date <= endDate)
                                group new { bf, bfi } by bf.Subb into g
                                select new GetBranchSales
                                {
                                    Branch = g.Key,
                                    TotalSales = g.Sum(x => Convert.ToDouble(x.bf.Tootal.GetValueOrDefault(0))),
                                };

                    var result = await query.ToListAsync();
                    return Ok(result);
                }*/

        [HttpGet("GetBranchSales")]
        public IActionResult GetBranchSales(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.BuyFatoras.AsQueryable();

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(f => f.Date >= startDate && f.Date <= endDate);
            }

            var result = query
                .Where(f => f.Subb == "zakho" || f.Subb == "Erbil" || f.Subb == "Baghdad")
                .GroupBy(f => f.Subb)
                .Select(g => new
                {
                    Branch = g.Key,
                    TotalSales = g.Where(f => f.Customer != "pasabahce erbil" && f.Customer != "pasabahce Baghdad")
                                  .Sum(f => f.Tootal ?? 0)
                })
                .ToList();

            return Ok(result);
        }







    }
}
