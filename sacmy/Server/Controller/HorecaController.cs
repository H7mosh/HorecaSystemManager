using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.DatabaseContext;
using sacmy.Server.Models;
using sacmy.Shared.ViewModel.HorecaViewModel;
using System.Data;


namespace sacmy.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorecaController : ControllerBase
    {
        private readonly SafeenCompanyDbContext _context;
        public HorecaController(SafeenCompanyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetHorecaInformationsViewModel>>> GetHoreca()
        {
            var list = await _context.HorecaInformations.Where(e => e.IsDeleted != true).Select(
                    e => new GetHorecaInformationsViewModel
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Governorate = e.Governorate,
                        Type = e.Type,
                        Concept = e.Concept,
                        OwnerName = e.OwnerName,
                        OwnerPhone = e.OwnerPhone,
                        PurcasingManagerName = e.PurcasingManagerName,
                        PurchasingManagerPhone = e.PurchasingManagerPhone,
                        Location = $"https://www.google.com/maps?q={e.Location}",
                        LocationDescription = e.LocationDescription,
                        //CreatedBy = e.CreatedBy.FirstName + e.CreatedByNavigation.LastName,
                        CreatedDate = e.CreatedDate,
                        getHorecaStatictsInformationViewModels = e.HorecaStatictsInformations.Select( x => new GetHorecaStatictsInformationViewModel {
                                                                    HorecaId = x.HorecaInfoId,
                                                                    Rating = x.Rating,
                                                                    BranchesCount = x.BranchesCount,
                                                                    TablesCount = x.TablesCount,
                                                                    ChairsCount = x.ChairsCount,
                                                                    IsHeSafeenAhmedCompanyCustomer = x.IsHeSafeenAhmedCompanyCustomer,
                                                                    IsHePasabahceBuyer = x.IsHePasabahceBuyer,
                                                                    PasabahceReason = x.PasabahceReason,
                                                                    PasabahcePercentage = x.PasabahcePercentage,
                                                                    IsHeBonnaBuyer = x.IsHeBonnaBuyer,
                                                                    BonnaReason = x.BonnaReason,
                                                                    BonnaPercentage = x.BonnaPercentage,
                                                                    IsHeNudeBuyer = x.IsHeNudeBuyer,
                                                                    NudeReason = x.NudeReason,
                                                                    NudePercentage = x.NudePercentage,
                                                                    IsHeBuyingFromAnotherGlassAgency = x.IsHeBuyingFromAnotherGlassAgency,
                                                                    CreatedDate = x.CreatedDate,
                                                                }).ToList(),
                        HorecaImage = e.HorecaImages.Select(e => "https://safinahmedcompany.com/assets/HorecaImages/" + e.Image).ToList(),

                    }
                ).OrderByDescending(e => e.CreatedDate).ToListAsync();

            return list;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetHorecaInformationsViewModel>> GetHorecaById(Guid id)
        {
            var horecaInfo = await _context.HorecaInformations
                .Where(e => e.Id == id)
                .Select(e => new GetHorecaInformationsViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Governorate = e.Governorate,
                    Type = e.Type,
                    Concept = e.Concept,
                    OwnerName = e.OwnerName,
                    OwnerPhone = e.OwnerPhone,
                    PurcasingManagerName = e.PurcasingManagerName,
                    PurchasingManagerPhone = e.PurchasingManagerPhone,
                    Location = $"https://www.google.com/maps?q={e.Location}",
                    LocationDescription = e.LocationDescription,
                    //CreatedBy = e.CreatedByNavigation.FirstName + e.CreatedByNavigation.LastName,
                    CreatedDate = e.CreatedDate,
                    getHorecaStatictsInformationViewModels = e.HorecaStatictsInformations.Select(x => new GetHorecaStatictsInformationViewModel
                    {
                        HorecaId = x.HorecaInfoId,
                        Rating = x.Rating,
                        BranchesCount = x.BranchesCount,
                        TablesCount = x.TablesCount,
                        ChairsCount = x.ChairsCount,
                        IsHeSafeenAhmedCompanyCustomer = x.IsHeSafeenAhmedCompanyCustomer,
                        IsHePasabahceBuyer = x.IsHePasabahceBuyer,
                        PasabahceReason = x.PasabahceReason,
                        PasabahcePercentage = x.PasabahcePercentage,
                        IsHeBonnaBuyer = x.IsHeBonnaBuyer,
                        BonnaReason = x.BonnaReason,
                        BonnaPercentage = x.BonnaPercentage,
                        IsHeNudeBuyer = x.IsHeNudeBuyer,
                        NudeReason = x.NudeReason,
                        NudePercentage = x.NudePercentage,
                        IsHeBuyingFromAnotherGlassAgency = x.IsHeBuyingFromAnotherGlassAgency,
                        CreatedDate = x.CreatedDate,
                    }).ToList(),
                    HorecaImage = e.HorecaImages.Select(e => "https://safinahmedcompany.com/assets/HorecaImages/" + e.Image).ToList(),
                })
                .FirstOrDefaultAsync();

            if (horecaInfo == null)
            {
                return NotFound();
            }

            return horecaInfo;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveHoreca(Guid Id) {
            HorecaInformation horecaInformation = await _context.HorecaInformations.FindAsync(Id);
            if (horecaInformation == null)
            {
                return NotFound();
            }
            else
            {
                horecaInformation.IsDeleted = true;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
         
    }
}
