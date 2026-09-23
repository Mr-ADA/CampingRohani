using CampingRohani.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Microsoft.EntityFrameworkCore;
using CampingRohani.Data;

namespace CampingRohani.Pages
{
    public class RegistrationPageModel : PageModel
    {

        private readonly CampingRohaniContext _context;

        public RegistrationPageModel(CampingRohaniContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Participant Participant { get; set; }

        [BindProperty]
        public Registration Registration { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CampId { get; set; }
        public Camp? SelectedCamp { get; private set; }

        public async Task OnGetAsync()
        {
            if (CampId > 0)
            {
                SelectedCamp = await _context.Camps
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CampId == CampId);
            }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //    return Page(); // re-renders Register.cshtml WITH the errors attached

            var registeredParticipant = new Participant
            {
                ParticipantId = Guid.NewGuid().ToString(),
                RegionId = 0,
                Name = Participant.Name,
                Phone = Participant.Phone,
                Email = Participant.Email,
                Parish = Participant.Parish,
                Age = Participant.Age,
                AdditionalDescription = Participant.AdditionalDescription,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now
            };

            _context.Participants.Add(registeredParticipant);
            await _context.SaveChangesAsync();

            List<RegistrationContactPerson> contactPersonAssignments = new List<RegistrationContactPerson>();
            
            RegistrationContactPerson registrationContactPerson = new RegistrationContactPerson
            {
                RegistrationId = Guid.NewGuid().ToString(),
                ContactPersonId = 2, // Assuming you have a contact person with ID 2
                AssignedDate = DateTime.Now,
                IsPrimary = true,
                Notes = "Primary contact person"
            };

            contactPersonAssignments.Add(registrationContactPerson);

            Payment payment = new Payment
            {
                PaymentId = Guid.NewGuid().ToString(),
                Amount = 0, // Example amount
                PaymentTime = DateTime.Now,
                PaymentType = PaymentType.NotPaid,
                Status = PaymentStatus.Pending,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now
            };

            //Masalah ketika CP ada 2, tidak bisa pakai one to one, one registration can have many CP
            var registration = new Registration
            {
                RegistrationId = Guid.NewGuid().ToString(),
                ParticipantId = registeredParticipant.ParticipantId,
                Participant = registeredParticipant,
                PaymentId = payment.PaymentId,
                Payment = payment,
                CampId = CampId,
                Camp = SelectedCamp,
                ContactPersonAssignments = contactPersonAssignments,
                RegistrationTime = DateTime.Now,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return RedirectToPage("PaymentPage", new { registrationId = registration.RegistrationId });
        }
    }
}
