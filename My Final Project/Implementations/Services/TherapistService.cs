using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using My_Final_Project.FileManager;
using My_Final_Project.Implementations.Repositories;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;
using My_Final_Project.Models.Enum;

namespace My_Final_Project.Implementations.Services
{
    public class TherapistService : ITherapistService
    {
        private readonly ITherapistRepository _therapistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IFileManager _fileManager;
        private readonly IIssuesRepository _issuesRepository;
        private readonly INotificationMessage _notificationMessage;
        private readonly UserManager<User> _userManager;
        public TherapistService(ITherapistRepository therapistRepository, IUserRepository userRepository, IRoleRepository roleRepository, IFileManager fileManager, IIssuesRepository issuesRepository, INotificationMessage notificationMessage, UserManager<User> userManager)
        {
            _therapistRepository = therapistRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _fileManager = fileManager;
            _issuesRepository = issuesRepository;
            _notificationMessage = notificationMessage;  
            _userManager = userManager;
        }

        public async Task<BaseResponse<TherapistDto>> Create(CreateTherapistRequestModel model)
        {
            var request = new WhatsappMessageSenderRequestModel { ReciprantNumber = model.PhoneNumber, MessageBody = "Therapist created Successfully" };
            await _notificationMessage.SendWhatsappMessageAsync(request);

            var checkIfExist = await _therapistRepository.CheckIfExist(model.Email);
            if (checkIfExist != null) return new BaseResponse<TherapistDto>
            {
                Message = "User already exist",
                Status = false,
            };
            var role = await _roleRepository.Get<Role>(b => b.Name == "Therapist");

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                IsDeleted = false,

                UserRoles = new List<UserRole>()
            };

            var userRole = new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = role.Id, 
                Role = role,
                
            };

            var certificatefile = await _fileManager.UploadFileToSystem(model.Certificate);
            var credentialsfile = await _fileManager.UploadFileToSystem(model.Credential);
            var profilepicturefile = await _fileManager.UploadFileToSystem(model.ProfilePicture);   
            var issues = new List<TherapistIssue>();

            var therapist = new Therapist
            {
                Id = Guid.NewGuid(),
                Certificate = certificatefile.Data.Name,
                Credential = credentialsfile.Data.Name,
                ProfilePicture = profilepicturefile.Data.Name,
                UserName = model.UserName,
                RegNo = model.RegNo,
                Description = model.Description, 
                UserId = user.Id, 
            };
            foreach (var item in model.IssueIds)
            {
                var issue = await _issuesRepository.Get<Issue>(item);
                var therapistIssue = new TherapistIssue
                {
                    Id = Guid.NewGuid(),
                    Issue = issue,
                    IssueId = item,
                    TherapistId = therapist.Id,  
                };
                issues.Add(therapistIssue);
            }
            user.UserRoles.Add(userRole);
            user.Therapist = therapist;
            await _userManager.CreateAsync(user);


            return new BaseResponse<TherapistDto>
            {
                Message = "Therapist created successfully",
                Status = true,
                Data = new TherapistDto
                {
                    FirstName = therapist.User.FirstName,
                    LastName = therapist.User.LastName,
                    Email = therapist.User.Email,
                    Gender = therapist.User.Gender,
                }
            };
        }

        public async Task<BaseResponse<TherapistDto>> Delete(Guid id)
        {
            var therapist = await _therapistRepository.Get<Therapist>(id);
            if (therapist == null) return new BaseResponse<TherapistDto>
            {
                Message = "Therapist Not Found",
                Status = false,
            };

            therapist.IsDeleted = true;
            await _therapistRepository.save();

            return new BaseResponse<TherapistDto>
            {
                Message = "Deleted Successfully",
                Status = true,
            };
        }

        public async Task<BaseResponse<TherapistDto>> GetTherapist(Guid id)
        {
            var therapist = await _therapistRepository.GetTherapist(id);
            if (therapist == null) return new BaseResponse<TherapistDto>
            {
                Message = "Therapist not found",
                Status = false,
            };

            return new BaseResponse<TherapistDto>
            {
                Message = "Successful",
                Status = true,
                Data = new TherapistDto
                {
                    Id = therapist.Id,
                    FirstName = therapist.User.FirstName,
                    LastName = therapist.User.LastName,
                    PhoneNumber = therapist.User.PhoneNumber,
                    Email = therapist.User.Email,
                    RegNo = therapist.RegNo,
                    Certificate = therapist.Certificate,
                    Credential = therapist.Credential,
                    ProfilePicture = therapist.ProfilePicture,
                    Password = therapist.User.Password,
                    Gender = therapist.User.Gender,

                }
            };
        }

        public async Task<IEnumerable<TherapistDto>> GetAll()
        {
            var therapists = await _therapistRepository.GetAllTherapist();
            var listOftherapists = therapists.Select(a => new TherapistDto
            {
                Id = a.Id,
                UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                Password = a.User.Password,
                RegNo = a.RegNo,
                Certificate = a.Certificate,
                IsAvailable = a.IsAvalaible,
                Credential = a.Credential,
                ProfilePicture = a.ProfilePicture,
                PhoneNumber = a.User.PhoneNumber,
                Description = a.Description,
                TherapistIssues = a.TherapistIssues,
            }).ToList();
            return listOftherapists;
        }

        public async Task<BaseResponse<TherapistDto>> Update(Guid id, UpdateTherapistRequestModel model)
        {
            var request = new WhatsappMessageSenderRequestModel { ReciprantNumber = model.PhoneNumber, MessageBody = "Therapist updated successfully" };
            await _notificationMessage.SendWhatsappMessageAsync(request);
            var therapist = await _therapistRepository.GetTherapist(id);
            if (therapist == null) return new BaseResponse<TherapistDto>
            {
                Message = "therapist not found",
                Status = false,
            };

            therapist.User.FirstName = model.FirstName;
            therapist.User.LastName = model.LastName;
            therapist.User.Password = model.Password;
            therapist.User.Email = model.Email;
            therapist.User.PhoneNumber = model.PhoneNumber;
            therapist.DateCreated = DateTime.Now;
            therapist.DateUpdated = DateTime.Now;
            therapist.IsDeleted = false;

            await _therapistRepository.Update(therapist);

            return new BaseResponse<TherapistDto>
            {
                Message = "Successful",
                Status = true,
                Data = new TherapistDto
                {
                    FirstName = therapist.User.FirstName,
                    LastName = therapist.User.LastName,
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<TherapistDto>>> ViewUnapprovedTherapist()
        {
            var therapist = await _therapistRepository.GetUnapprovedTherapist();
            if (therapist == null) return new BaseResponse<IEnumerable<TherapistDto>>
            {
                Message = "Therapist not found",
                Status = false,
            };
            var listOftherapists = therapist.Select(a => new TherapistDto
            {
                Id = a.Id,
                UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                PhoneNumber = a.User.PhoneNumber,
                RegNo = a.RegNo,
                Certificate = a.Certificate,
                Credential = a.Credential,
            }).ToList();
            return new BaseResponse<IEnumerable<TherapistDto>>
            {
                Message = "Successful",
                Status = true,
                Data = listOftherapists
            };

        }

        public async Task<BaseResponse<IEnumerable<TherapistDto>>> ViewapprovedTherapist()
        {
            var therapist = await _therapistRepository.GetApprovedTherapist();
            if (therapist == null) return new BaseResponse<IEnumerable<TherapistDto>>
            {
                Message = "Therapist not found",
                Status = false,
            };
            var listOftherapists = therapist.Select(a => new TherapistDto
            {
                Id = a.Id,
                UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                PhoneNumber = a.User.PhoneNumber,
                Certificate = a.Certificate,
                RegNo = a.RegNo,
                Credential = a.Credential,
            }).ToList();
            return new BaseResponse<IEnumerable<TherapistDto>>
            {
                Message = "Successful",
                Status = true,
                Data = listOftherapists
            };
        }

        public async Task<BaseResponse<TherapistDto>> RejectapprovedTherapist(Guid id)
        {
            var therapist = await _therapistRepository.GetTherapist(id);
            if (therapist == null) return new BaseResponse<TherapistDto>
            {
                Message = "Not Successful",
                Status = false,
            };
            therapist.Status = Aprrove.Rejected;
            await _therapistRepository.save();

            return new BaseResponse<TherapistDto>
            {
                Message = "Delete Successful",
                Status = true,
                Data = new TherapistDto
                {
                    Id = therapist.Id,
                    FirstName = therapist.User.FirstName,
                    LastName = therapist.User.LastName,
                    PhoneNumber = therapist.User.PhoneNumber,
                    RegNo = therapist.RegNo,
                    Certificate = therapist.Certificate,
                    Credential = therapist.Credential,
                }
            };

        }

        public async Task<IEnumerable<TherapistDto>> GetAllAvailableTherapist()
        {
            var therapists = await _therapistRepository.GetAllAvailableTherapist();
            var listOftherapists = therapists.Select(a => new TherapistDto
            {
                Id = a.Id,
                //UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                RegNo = a.RegNo,
            }).ToList();
            return listOftherapists;
        }

        public async Task<List<UserDto>> GetAllTherapistByChat()
        {
            var therapists = await _therapistRepository.GetAllTherapist();
            var listOfTherapists = therapists.Select(a => new UserDto
            {
                Id = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
            }).ToList();
            return listOfTherapists;

        }

        public async Task<BaseResponse<TherapistDto>> Approve(Guid id)
        {
            var therapist = await _therapistRepository.GetTherapist(id);
            if (therapist == null) return new BaseResponse<TherapistDto>
            {
                Message = "Not Successful",
                Status = false,
            };
            therapist.Status = Aprrove.Approved;
            therapist.IsDeleted = false;
            await _therapistRepository.save();

            return new BaseResponse<TherapistDto>
            {
                Message = "Delete Successful",
                Status = true,
                Data = new TherapistDto
                {
                    Id = therapist.Id,
                    FirstName = therapist.User.FirstName,
                    LastName = therapist.User.LastName,
                    PhoneNumber = therapist.User.PhoneNumber,
                    RegNo = therapist.RegNo,
                    Certificate = therapist.Certificate,
                    Credential = therapist.Credential,
                }

            };
        }

        public async Task<BaseResponse<IEnumerable<TherapistDto>>> ViewRejectedTherapist()
        {
            var therapist = await _therapistRepository.GetRejectedTherapist();
            if (therapist == null) return new BaseResponse<IEnumerable<TherapistDto>>
            {
                Message = "Therapist not found",
                Status = false,
            };
            var listOftherapists = therapist.Select(a => new TherapistDto
            {
                Id = a.Id,
                UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                PhoneNumber = a.User.PhoneNumber,
                Certificate = a.Certificate,
                RegNo = a.RegNo,
                Credential = a.Credential,
            }).ToList();
            return new BaseResponse<IEnumerable<TherapistDto>>
            {
                Message = "Successful",
                Status = true,
                Data = listOftherapists
            };
        }
    }
}
