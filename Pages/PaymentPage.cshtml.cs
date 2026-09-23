using CampingRohani.Data;
using CampingRohani.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampingRohani.Pages
{
    public class PaymentPageModel : PageModel
    {
        private readonly ILogger<PaymentPageModel> _logger;
        private readonly CampingRohaniContext _context;

        public PaymentPageModel(ILogger<PaymentPageModel> logger, CampingRohaniContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public Participant Participant { get; set; }

        [BindProperty]
        public Registration Registration { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CampId { get; set; }
        public Camp? SelectedCamp { get; private set; }

        public string RegistrationId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public double TotalAmount { get; set; }
        public string ParticipantName { get; set; }
        public string CampName { get; set; }
        public List<ContactPerson> RegionsWithContacts { get; set; } = new();

        public async void OnGetAsync()
        {
            // TODO: Implement database query to load regions with contact persons
            // Example structure:
            RegionsWithContacts = await _context.ContactPersons
            .AsNoTracking()
            .Include(contactPerson => contactPerson.Region)
            .Where(contactPerson => contactPerson.Region.CampId == 1)
            .OrderBy(contactPerson => contactPerson.Region.RegionName)
            .ThenBy(contactPerson => contactPerson.Name)
            .ToListAsync();

        }


    }
}