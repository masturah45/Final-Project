using My_Final_Project.Implementations.Repositories;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Implementations.Services
{
    public class TherapistIssuesService : ITherapistIssuesService
    {
        private readonly ITherapistIssuesRepository _therapistissuesRepository;

        public TherapistIssuesService(ITherapistIssuesRepository therapistissuesRepository)
        {
            _therapistissuesRepository = therapistissuesRepository;
        }
        public async Task<IEnumerable<TherapistIssuesDto>> GetAll()
        {
            var therapistissues = await _therapistissuesRepository.GetAllTherapistIssues();
            var listOftherapistIssues = therapistissues.Select(a => new TherapistIssuesDto
            {
                IssuesId = a.IssueId,
                TherapistId = a.TherapistId,
            }).ToList();
            return listOftherapistIssues;
        }

        public async Task<BaseResponse<TherapistIssuesDto>> GetTherapistIssues(Guid id)
        {
            var therapistIssues = await _therapistissuesRepository.Get(id);
            if (therapistIssues == null) return new BaseResponse<TherapistIssuesDto>
            {
                Message = "Issue Not Found",
                Status = false,
            };

            return new BaseResponse<TherapistIssuesDto>
            {
                Message = "Successfull",
                Status = true,
                Data = new TherapistIssuesDto
                {
                    IssuesId=id,
                    TherapistId=id,
                },
            };
        }

        public async Task<BaseResponse<IEnumerable<TherapistDto>>> GetTherapistByIssues(Guid IssuesId)
        {
            var therapistIssues = await _therapistissuesRepository.GetTherapistByIssues(IssuesId);
            if (therapistIssues == null) return new BaseResponse<IEnumerable<TherapistDto>>
            {
                Message = "TherapistIssue Not Found",
                Status = false,
            };

            return new BaseResponse<IEnumerable<TherapistDto>>
            {
                Message = "Successfull",
                Status = true,
                Data = therapistIssues.Select(x => new TherapistDto
                {
                    Id = IssuesId
                }).ToList(),
            };
        }
    }
}
