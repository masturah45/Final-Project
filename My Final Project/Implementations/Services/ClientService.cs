using Microsoft.AspNetCore.Identity;
using My_Final_Project.Implementations.Repositories;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;
using My_Final_Project.Models.Enum;

namespace My_Final_Project.Implementations.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly INotificationMessage _notificationMessage;
        private readonly UserManager<User> _userManager;
        public ClientService(IClientRepository clientRepository, IUserRepository userRepository, IRoleRepository roleRepository, INotificationMessage notificationMessage, UserManager<User> userManager)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _notificationMessage = notificationMessage;
            _userManager = userManager;
        }

        public async Task<BaseResponse<ClientDto>> Create(CreateClientRequestModel model)
        {
            var request = new WhatsappMessageSenderRequestModel { ReciprantNumber = model.PhoneNumber, MessageBody = "Client created successfully" };
            await _notificationMessage.SendWhatsappMessageAsync(request);
            //var clientExist = await _userRepository.Get(a => a.Email == model.Email);
            //if (clientExist != null) return new BaseResponse<ClientDto>
            //{
            //    Message = "User already exist",
            //    Status = false,
            //};

            var checkIfExist = await _clientRepository.CheckIfExist(model.Email);
            if (checkIfExist != null) return new BaseResponse<ClientDto>
            {
                Message = "User already exist",
                Status = false,
            };

            var role = await _roleRepository.Get<Role>(a => a.Name == "Client");

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,

                UserRoles = new List<UserRole>()
            };

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = Guid.NewGuid(),
                Role = role,
                User = user,
            };
            user.UserRoles.Add(userRole);
            await _userManager.CreateAsync(user);
            var client = new Client
            {
                User = user,
                UserId = userRole.UserId.ToString(),
                //UserId = Guid.NewGuid(),
                State = model.State,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,

            };
            await _clientRepository.Add(client);

            return new BaseResponse<ClientDto>
            {
                Message = "Client created successfully",
                Status = true,
                Data = new ClientDto
                {
                    FirstName = client.User.FirstName,
                    LastName = client.User.LastName,
                    Email = client.User.Email,
                    Password = client.User.Password,
                    Gender = client.User.Gender,    
                }
            };
        }

        public async Task<BaseResponse<ClientDto>> Delete(Guid id)
        {
            var client = await _clientRepository.Get<Client>(id);
            if (client == null) return new BaseResponse<ClientDto>
            {
                Message = "Client Not Found",
                Status = false,
            };

            client.IsDeleted = true;
            await _clientRepository.save();

            return new BaseResponse<ClientDto>
            {
                Message = "Deleted Successfully",
                Status = true,
            };
        }

        public async Task<BaseResponse<ClientDto>> GetClient(Guid id)
        {
            var client = await _clientRepository.GetClient(id);
            if (client == null) return new BaseResponse<ClientDto>
            {
                Message = "Client not found",
                Status = false,
            };

            return new BaseResponse<ClientDto>
            {
                Message = "Successful",
                Status = true,
                Data = new ClientDto
                {
                    Id = client.Id,
                    FirstName = client.User.FirstName,
                    LastName = client.User.LastName,
                    PhoneNumber = client.User.PhoneNumber,
                    Email = client.User.Email,
                    DateOfBirth = client.DateOfBirth,
                    State = client.State,
                    Gender = client.Gender,
                }
            };
        }

        public async Task<IEnumerable<ClientDto>> GetAll()
        {
            var clients = await _clientRepository.GetAll();
            var listOfClients = clients.Select(a => new ClientDto
            {
                Id = a.Id,
                UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                PhoneNumber = a.User.PhoneNumber,
                DateOfBirth = a.DateOfBirth,
                State = a.State,
            }).ToList();
            return listOfClients;
        }

        public async Task<BaseResponse<ClientDto>> Update(Guid id, UpdateClientRequestModel model)
        {
            var request = new WhatsappMessageSenderRequestModel { ReciprantNumber = model.PhoneNumber, MessageBody = "Client edited successfully" };
            await _notificationMessage.SendWhatsappMessageAsync(request);

            var client = await _clientRepository.GetClient(id);
            if (client == null) return new BaseResponse<ClientDto>
            {
                Message = "client not found",
                Status = false,
            };

            client.User.FirstName = model.FirstName;
            client.User.LastName = model.LastName;
            client.User.Email = model.Email;
            client.User.PhoneNumber = model.PhoneNumber;
            client.DateOfBirth = model.DateOfBirth;
            client.State = model.State;
            client.DateCreated = DateTime.Now;
            client.DateUpdated = DateTime.Now;
            client.IsDeleted = false;

            await _clientRepository.Update(client);

            return new BaseResponse<ClientDto>
            {
                Message = "Successful",
                Status = true,
                Data = new ClientDto
                {
                    FirstName = client.User.FirstName,
                    LastName = client.User.LastName,
                }
            };
        }

        public async Task<List<UserDto>> GetAllClientByChat()
        {
            var clients = await _clientRepository.GetAll();
            var listOfClients = clients.Select(a => new UserDto
            {
                Id = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
            }).ToList();
            return listOfClients;
        }
    }
}
