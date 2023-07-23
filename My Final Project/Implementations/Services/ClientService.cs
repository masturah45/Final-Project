using My_Final_Project.Implementations.Repositories;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.Implementations.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public ClientService(IClientRepository clientRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            clientRepository = _clientRepository;
            userRepository = _userRepository;
            roleRepository = _roleRepository;
        }

        public async Task<BaseResponse<ClientDto>> Create(CreateClientRequestModel model)
        {
            var clientExist = await _userRepository.Get(a => a.Email == model.Email);
            if (clientExist != null) return new BaseResponse<ClientDto>
            {
                Message = "User already exist",
                Status = false,
            };

            var role = await
            _roleRepository.Get(a => a.Name == "Client");

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Email = model.Email,
                PhoneNumber = model.Email,

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
            var client = new Client
            {
                User = user,
                UserId = Guid.NewGuid(),
                WalletBalance = model.WalletBalance,
                State = model.State,
                Address = model.Address,
                Country = model.Country,
                DateOfBirth = model.DateOfBirth,
                Gender = Models.Enum.Gender.Male,
                
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
                }
            };
        }

        public async Task<BaseResponse<ClientDto>> Delete(Guid id)
        {
            var client = await _clientRepository.GetClient(id);
            if (client == null) return new
            BaseResponse<ClientDto>
            {
                Message = "client Not Found",
                Status = false,
            };

            _clientRepository.save();

            return new BaseResponse<ClientDto>
            {
                Message = "Delete Successfully",
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
                    Id = Guid.NewGuid(),
                    FirstName = client.User.FirstName,
                    LastName = client.User.LastName,
                    PhoneNumber = client.User.PhoneNumber,
                    Email = client.User.Email,
                    WalletBalance =  client.WalletBalance,
                    DateOfBirth = client.DateOfBirth,
                    State = client.State,
                    Address = client.Address,
                    Country = client.Country,
                    Gender = client.Gender,
                }
            };
        }

        public async Task<IEnumerable<ClientDto>> GetAllClient()
        {
            var clients = await _clientRepository.GetAll();
            var listOfClients = clients.Select(a => new ClientDto
            {
                Id = Guid.NewGuid(),
                UserId = a.UserId,
                FirstName = a.User.FirstName,
                LastName = a.User.LastName,
                Email = a.User.Email,
                PhoneNumber = a.User.PhoneNumber,
                DateOfBirth= a.DateOfBirth,
                Address = a.Address,
                Country = a.Country,
                State = a.State,
                Gender = a.Gender,
                WalletBalance = a.WalletBalance,
            }).ToList();
            return listOfClients;
        }

        public async Task<BaseResponse<ClientDto>> Update(Guid id, UpdateClientRequestModel model)
        {
            var client = await _clientRepository.GetClient(id);
            if (client == null) return new BaseResponse<ClientDto>
            {
                Message = "client not found",
                Status = false,
            };

            client.User.FirstName = model.FirstName;
            client.User.LastName = model.LastName;
            client.User.Password = model.Password;
            client.User.Email = model.Password;
            client.User.PhoneNumber = model.PhoneNumber;
            client.WalletBalance = model.WalletBalance;
            client.Address = model.Address;
            client.Country = model.Country;
            client.DateOfBirth = model.DateOfBirth;
            client.State = model.State;
            client.Gender = model.Gender;
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
    }
}
