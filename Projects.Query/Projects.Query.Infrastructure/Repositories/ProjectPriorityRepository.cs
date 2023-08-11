using Projects.Common.Enum;
using Projects.Query.Domain.Enum;
using Projects.Query.Domain.Interfaces;

namespace Projects.Query.Infrastructure.Repositories
{
    public class ProjectPriorityRepository : IProjectPriorityRepository
    {
        public async Task<List<ProjectPriorityEnum>> ListAllAsync()
        {
            var enumValues = Enum.GetValues(typeof(ProjectPriority)).Cast<ProjectPriority>();


            var enumMap = enumValues.ToDictionary(
                enumValue => (int)enumValue,
                enumValue => enumValue.ToString()
            );

            ProjectPriorityEnum priorityEnum = new()
            {
                Priority = enumMap
            };

            var enumList = new List<ProjectPriorityEnum>() { priorityEnum };

            return enumList;
        }
    }
}
