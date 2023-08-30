using Newtonsoft.Json;
using Projects.Query.Domain.Entities;
using Projects.Query.Infrastructure.DataAccess;

namespace Projects.Query.Infrastructure.SeedData
{
    public static class SeedData
    {
        public static void Initialize(DatabaseContext context)
        {
            // Check if data already exists in the database
            if (context.ProjectPriorities.Any() || context.ProjectStatuses.Any())
            {
                return; // Data already seeded
            }

            // Get the path to the JSON file in the same directory as this class
            var jsonFileName = "ProjectSeedData.json";
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), jsonFileName);

            // Check if the JSON file exists
            if (!File.Exists(jsonFilePath))
            {
                return; // JSON file doesn't exist, no seeding required
            }

            // Read seed data from JSON file
            var jsonData = File.ReadAllText(jsonFilePath);
            var seedData = JsonConvert.DeserializeObject<SeedDataModel>(jsonData);

            // Seed Project Priorities
            context.ProjectPriorities.AddRange(seedData.ProjectPriorities);

            // Seed Project Statuses
            context.ProjectStatuses.AddRange(seedData.ProjectStatuses);

            context.SaveChanges();
        }
    }

    public class SeedDataModel
    {
        public List<ProjectPriorityEntity> ProjectPriorities { get; set; }
        public List<ProjectStatusEntity> ProjectStatuses { get; set; }
    }
}
