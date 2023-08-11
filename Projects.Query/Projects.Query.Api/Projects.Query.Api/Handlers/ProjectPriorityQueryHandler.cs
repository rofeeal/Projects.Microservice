﻿using Projects.Query.Api.Interfaces;
using Projects.Query.Api.Queries;
using Projects.Query.Domain.Enum;
using Projects.Query.Domain.Interfaces;

namespace Projects.Query.Api.Handlers
{
    public class ProjectPriorityQueryHandler : IProjectPriorityQueryHandler
    {
        private readonly IProjectPriorityRepository _projectPriorityRepository;

        public ProjectPriorityQueryHandler(IProjectPriorityRepository projectPriorityRepository)
        {
            _projectPriorityRepository = projectPriorityRepository;
        }

        public async Task<List<ProjectPriorityEnum>> HandleAsync(FindProjectPriorityListQuery query)
        {
            return await _projectPriorityRepository.ListAllAsync();
        }
    }
}
