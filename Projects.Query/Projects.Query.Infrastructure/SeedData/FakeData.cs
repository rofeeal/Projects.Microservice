using Newtonsoft.Json;
using Projects.Query.Domain.Entities;
using Projects.Query.Infrastructure.DataAccess;

namespace Projects.Query.Infrastructure.SeedData
{
    public static class FakeData
    {
        public static void Initialize(DatabaseContext context)
        {
            // Check if data already exists in the database
            if (context.Projects.Any())
            {
                return; // Data already seeded
            }

            // Get the path to the JSON file in the same directory as this class
            var jsonFileName = "ProjectFakeData.json";
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), jsonFileName);

            // Check if the JSON file exists
            if (!File.Exists(jsonFilePath))
            {
                return; // JSON file doesn't exist, no seeding required
            }

            // Read seed data from JSON file
            var jsonData = File.ReadAllText(jsonFilePath);
            var seedData = JsonConvert.DeserializeObject<FakeDataModel>(jsonData);

            // Seed Projects
            context.Projects.AddRange(seedData.Projects);

            context.SaveChanges();
        }
    }

    public class FakeDataModel
    {
        public List<ProjectEntity> Projects { get; set; }
    }
}
