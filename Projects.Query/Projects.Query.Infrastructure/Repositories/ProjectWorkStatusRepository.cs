using Projects.Common.Enum;
using Projects.Query.Domain.Enum;
using Projects.Query.Domain.Interfaces;

namespace Projects.Query.Infrastructure.Repositories
{
    public class ProjectWorkStatusRepository : IProjectWorkStatusRepository
    {
        public async Task<List<ProjectWorkStatusEnum>> ListAllAsync()
        {
            var enumValues = Enum.GetValues(typeof(ProjectWorkStatus)).Cast<ProjectWorkStatus>();

            var enumMap = enumValues.ToDictionary(
                enumValue => (int)enumValue,
                enumValue => enumValue.ToString()
            );

            ProjectWorkStatusEnum projectWork = new()
            {
                WorkStatus = enumMap
            };

            var enumList = new List<ProjectWorkStatusEnum> { projectWork };

            return enumList;
        }
    }
}
