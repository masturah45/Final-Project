using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.Implementations.Services
{
    public class TherapistService : ITherapistService
    {
        private readonly ITherapistRepository _therapistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public TherapistService(ITherapistRepository therapistRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _therapistRepository = therapistRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponse<TherapistDto>> Create(CreateTherapistRequestModel model)
        {
            var therapistExist = await _userRepository.Get(a => a.Email == model.Email);
            if (therapistExist != null) return new BaseResponse<TherapistDto>
            {
                Message = "User already exist",
                Status = false,
            };

            var role = await _roleRepository.Get(b => b.Name == "Therapist");

            var user = new User
            {
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
                UserId = Guid.NewGuid(),
                RoleId = Guid.NewGuid(),
                Role = role,
                User = user,
            };
            user.UserRoles.Add(userRole);
            await _userRepository.Add(user);

            var therapist = new Therapist
            {
                User = user,
                UserId = Guid.NewGuid(),
            };

            await _therapistRepository.Add(therapist);

            return new BaseResponse<TherapistDto>
            {
                Message = "Therapist created successfully",
                Status = true,
                Data = new TherapistDto
                {
                    FirstName = therapist.User.FirstName,
                    LastName = therapist.User.LastName,
                    Email = therapist.User.Email,
                }
            };
        }

        public async Task<BaseResponse<TherapistDto>> Delete(Guid id)
        {
            var therapist = await _therapistRepository.GetTherapist(id);
            if (therapist == null) return new BaseResponse<TherapistDto>
            {
                Message = "Therapist Not Found",
                Status = false,
            };

            await _therapistRepository.save();

            return new BaseResponse<TherapistDto>
            {
                Message = "Delete Successful",
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
                    Id = Guid.NewGuid(),
                    FirstName = therapist.User.FirstName,
                    LastName = therapist.User.LastName,
                    PhoneNumber = therapist.User.PhoneNumber,
                    Email = therapist.User.Email,
                    Password = therapist.User.Password,
                    Gender = Models.Enum.Gender.Male
                }
            };
        }

        public async Task<IEnumerable<TherapistDto>> GetAllTherapist()
        {
            var therapists = await _therapistRepository.GetAllTherapist();
            var listOftherapists = therapists.Select(a => new TherapistDto
            {
                Id = Guid.NewGuid(),
                UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                Password = a.User.Password,
                PhoneNumber = a.User.PhoneNumber,
            }).ToList();
            return listOftherapists;
        }

        public async Task<BaseResponse<TherapistDto>> Update(Guid id, UpdateTherapistRequestModel model)
        {
            var therapist = await _therapistRepository.GetTherapist(id);
            if (therapist == null) return new BaseResponse<TherapistDto>
            {
                Message = "therapist not found",
                Status = false,
            };

            therapist.User.FirstName = model.FirstName;
            therapist.User.LastName = model.LastName;
            therapist.User.Password = model.Password;
            therapist.User.Password = model.Password;
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
                Id = Guid.NewGuid(),
                UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                Password = a.User.Password,
                PhoneNumber = a.User.PhoneNumber,
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
                Id = Guid.NewGuid(),
                UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                Password = a.User.Password,
                PhoneNumber = a.User.PhoneNumber,
            }).ToList();
            return new BaseResponse<IEnumerable<TherapistDto>>
            {
                Message = "Successful",
                Status = true,
                Data = listOftherapists
            };
        }

        public async Task<BaseResponse<TherapistDto>> RemoveapprovedTherapist(Guid id)
        {
            var therapist = await _therapistRepository.GetTherapist(id);
            if (therapist == null) return new BaseResponse<TherapistDto>
            {
                Message = "Successful",
                Status = false,
            };
            therapist.IsDeleted = true;
            await _therapistRepository.save();

            return new BaseResponse<TherapistDto>
            {
                Message = "Delete Successful",
                Status = true,
            };

        }
    }
}
