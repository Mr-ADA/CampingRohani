using CampingRohani.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampingRohani.Data;

namespace CampingRohani.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CampingRohaniContext _context;
        public List<ContactPerson> ContactPersons { get; private set; } = new();
        public List<Camp> Camps { get; private set; } = new();
        
        [BindProperty(SupportsGet = true)]
        public string SearchRegion { get; set; } = string.Empty;

        public IndexModel(ILogger<IndexModel> logger, CampingRohaniContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            IQueryable<ContactPerson> contactQuery = _context.ContactPersons
                .AsNoTracking()
                .Include(contactPerson => contactPerson.Region)
                .Where(contactPerson => contactPerson.Region.CampId == 1);

            if (!string.IsNullOrWhiteSpace(SearchRegion))
            {
                var searchValue = SearchRegion.Trim();
                contactQuery = contactQuery.Where(contactPerson =>
                    EF.Functions.Like(
                        contactPerson.Region.RegionName,
                        $"%{searchValue}%"));
            }

            // TODO: Load from database instead of hardcoding
            ContactPersons = await _context.ContactPersons
            .AsNoTracking()
            .Include(contactPerson => contactPerson.Region)
            .Where(contactPerson => contactPerson.Region.CampId == 1)
            .OrderBy(contactPerson => contactPerson.Region.RegionName)
            .ThenBy(contactPerson => contactPerson.Name)
            .ToListAsync();

            Camps = await _context.Camps.
                AsNoTracking()
                .OrderBy(camp => camp.StartDate)
                .ToListAsync();
        }
    }
}