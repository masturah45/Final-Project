using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;
using System.Data;

namespace My_Final_Project.Implementations.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly ITherapistRepository _therapistRepository;
        private readonly IClientRepository _clientRepository;

        public ChatService(IChatRepository chatRepository, ITherapistRepository therapistRepository, IClientRepository clientRepository)
        {
            _chatRepository = chatRepository;
            _therapistRepository = therapistRepository;
            _clientRepository = clientRepository;
        }

        public async Task<BaseResponse<ChatDto>> CreateChat(CreateChatRequestModel model, Guid loginId, Guid recieverId, string role)
        {
            Therapist therapist = null;
            Client client = null;
           
            if (role == "Therapist")
            {
                therapist = await _therapistRepository.GetTherapist(loginId);
            }
            else if (role == "Client")
            {
                client = await _clientRepository.GetClientByIdAsync(loginId);
            }

            //if (client == null || therapist == null)
            //{
            //    return new BaseResponse<ChatDto>
            //    {
            //        Message = "Sorry ! Something Bad went wrong",
            //        Status = false
            //    };
            //}

            var chat = new Chat
            {
                Message = model.Message,
                PostedTime = DateTime.Now.ToLongDateString(),
                SenderId = loginId,
                RecieverId = recieverId
            };

            await _chatRepository.Add(chat);
            return new BaseResponse<ChatDto>
            {
                Message = "Message Successfully Sent",
                Status = true
            };
        }

        //public async Task<BaseResponse<List<ChatDto>>> GetAllUnSeenChat(Guid therapistId)
        //{
        //    var therapist = await _therapistRepository.GetTherapist(therapistId);

        //    if(therapist == null)
        //    {
        //        return new BaseResponse<List<ChatDto>>
        //        {
        //            Message = "Sorry ! Something went wrong",
        //            Status = false
        //        };
        //    }

        //    var unseen = await _chatRepository.GetAllUnSeenChat(therapistId);

        //    return new BaseResponse<List<ChatDto>>
        //    {
        //        Message = "Successful",
        //        Status = true,
        //        Data = unseen.Select(x => new ChatDto
        //        {
        //             TherapistId = x.TherapistId
        //        }).ToList()
        //    };
        //}

        public async Task<BaseResponse<List<ChatDto>>> GetAllChatFromASender(Guid loginId, Guid senderId, string role)
        {
            //Therapist loginTherapist = null;
            //Client loginClient = null;
            //Therapist senderTherapist = null;
            //Client senderClient = null;

            //if (role == "Therapist")
            //{
            //    loginTherapist = await _therapistRepository.GetTherapist(loginId);
            //    senderTherapist = await _therapistRepository.GetTherapist(senderId);
            //}
            //else if (role == "Client")
            //{
            //    loginClient = await _clientRepository.GetClientByIdAsync(loginId);
            //    senderClient = await _clientRepository.GetClientByIdAsync(senderId);
            //}

            //if ((loginTherapist == null && loginClient == null) || (senderTherapist == null && senderClient == null))
            //{
            //    return new BaseResponse<List<ChatDto>>
            //    {
            //        Message = "Sorry! Something went wrong",
            //        Status = false
            //    };
            //}

            //var senderIdToUse = role == "Therapist" ? senderTherapist.Id : senderClient.Id;
            //var loginIdToUse = role == "Therapist" ? loginTherapist.Id : loginClient.Id;

            var chats = await _chatRepository.GetAllChatFromASender(loginId, senderId);

            var chatDtos = chats.Select(x => new ChatDto
            {
                RecieverId = x.RecieverId,
                SenderId = x.SenderId,
                Message = x.Message,
                PostedTime = x.DateCreated.ToShortTimeString(),
                Seen = x.Seen,
                LoggedInId = loginId
            }).ToList();

            return new BaseResponse<List<ChatDto>>
            {
                Message = "Chats restored Successfully",
                Status = true,
                Data = chatDtos
            };
        }

        public async Task<BaseResponse<ChatDto>> MarkAllChatAsRead(Guid clientId, Guid therapistId)
        {
            var client = await _clientRepository.GetClient(clientId);
            var therapist = await _therapistRepository.GetTherapist(therapistId);

            if(client == null || therapist == null)
            {
                return new BaseResponse<ChatDto>
                {
                    Message = "Sorry ! Something went wrong",
                    Status = false,
                };
            }

            var chats = await _chatRepository.GetAllUnSeenChat(client.Id, therapist.Id);

            foreach(var chat in chats)
            {
                chat.Seen = true;
                await _chatRepository.Update(chat);
            }

            return new BaseResponse<ChatDto>
            {
                Message = "Messages marked as seen",
                Status = true
            };
        }

        public Task<BaseResponse<ChatDto>> CreateChat(CreateChatRequestModel model, Guid id, Guid senderId)
        {
            throw new NotImplementedException();
        }
    }
}
