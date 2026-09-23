using CampingRohani.Model;
using Microsoft.EntityFrameworkCore;

namespace CampingRohani.Data;

public class DatabaseSeeder
{
    private readonly CampingRohaniContext _context;

    public DatabaseSeeder(CampingRohaniContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (await _context.Camps.AnyAsync())
        {
            Console.WriteLine("Database already contains a camp. Skipping development seed.");
            return;
        }

        var now = DateTime.UtcNow;
        var camp = new Camp
        {
            CampId = 1,
            CampName = "Camping Rohani Mahasiswa 2026",
            Price = 250000m,
            CampLocation = "Pertapaan Karmel, Ngadireso, Tumpang, Malang",
            CampTheme = "Be Courageous",
            StartDate = new DateTime(2026, 8, 5),
            EndDate = new DateTime(2026, 8, 9),
            CreatedTime = now,
            UpdatedTime = now
        };

        _context.Camps.Add(camp);
        await _context.SaveChangesAsync();

        var regions = new[]
        {
            "Jakarta", "Bandung", "Semarang", "Yogyakarta", "Surabaya",
            "Malang", "Bogor", "Cirebon", "Surakarta", "Tangerang"
        }.Select((name, id) => new Region
        {
            RegionId = id,
            RegionName = name,
            CampId = camp.CampId,
            CreatedTime = now,
            UpdatedTime = now
        }).ToList();

        _context.Regions.AddRange(regions);
        await _context.SaveChangesAsync();

        var names = new[]
        {
            "Agustinus Pranoto", "Maria Regina Lestari", "Fransiskus Wijaya",
            "Theresia Kartika", "Yohanes Setiawan", "Clara Anastasia",
            "Ignatius Haryanto", "Monika Puspita", "Petrus Adi Nugroho",
            "Veronica Kusuma", "Dominikus Santoso", "Bernadette Ayu",
            "Markus Gunawan", "Elisabeth Natalia", "Stefanus Hartono",
            "Cecilia Maharani", "Antonius Budi", "Magdalena Sari",
            "Benediktus Surya", "Theresia Maria"
        };

        var banks = new[]
        {
            "BCA", "BNI", "Bank Mandiri", "BRI", "CIMB Niaga",
            "OCBC", "BTN", "Danamon", "PermataBank", "Bank Mega"
        };

        var contactPersons = new List<ContactPerson>();
        for (var index = 0; index < names.Length; index++)
        {
            var region = regions[index / 2];
            var name = names[index];

            contactPersons.Add(new ContactPerson
            {
                RegionId = region.RegionId,
                Name = name,
                Phone = $"08{12 + index:D2}{index + 1:D8}",
                AccountNumber = $"900{index + 1:D10}",
                AccountName = name,
                BankName = banks[index / 2],
                CreatedTime = now,
                UpdatedTime = now
            });
        }

        _context.ContactPersons.AddRange(contactPersons);
        await _context.SaveChangesAsync();

        Console.WriteLine("Development seed completed: 1 camp, 10 regions, and 20 contact persons.");
    }
}
