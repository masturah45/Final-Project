﻿using My_Final_Project.Implementations.Repositories;
using My_Final_Project.Interfaces.IRepositories;
using My_Final_Project.Interfaces.IService;
using My_Final_Project.Models.DTOs;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.Implementations.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITherapistRepository _therapistRepository;
        private readonly IClientRepository _clientRepository;
        private readonly INotificationMessage _notificationMessage;

        public BookingService(IBookingRepository bookingRepository, ITherapistRepository therapistRepository, IClientRepository clientRepository, INotificationMessage notificationMessage)
        {
            _bookingRepository = bookingRepository;
            _therapistRepository = therapistRepository;
            _clientRepository = clientRepository;
            _notificationMessage = notificationMessage;
        }

        public async Task<BaseResponse<BookingDto>> CancelBooking(Guid TherapistId)
        {
            var booking = await _bookingRepository.GetBooking(TherapistId);
            if (booking == null || booking.IsDeleted) return new BaseResponse<BookingDto>
            {
                Message = "Booking has already been cancelled",
                Status = false
            };

            booking.IsDeleted = true;

            await _bookingRepository.save();

            return new BaseResponse<BookingDto>
            {
                Message = "Booking Cancelled Successfully",
                Status = true,
            };
        }

        public async Task<BaseResponse<BookingDto>> Create(CreateBookingRequestModel model, Guid userId)
        {
            var request = new WhatsappMessageSenderRequestModel { ReciprantNumber = "+2347054770135", MessageBody = "Created Successfully" };
            await _notificationMessage.SendWhatsappMessageAsync(request);

            var bookingExist = await _bookingRepository.GetBooking(b => b.AppointmentDateTime == model.AppointmentDateTime);
            //var therapistExist = await _therapistRepository.Get(b => b.RegNo ==  model.TherapistName);
            var clientExist = await _clientRepository.Get<Client>(u => u.UserId == userId.ToString());
            if (clientExist == null) return new BaseResponse<BookingDto>
            {
                Message = "Client does not exist",
                Status = false,
            };
            if (bookingExist != null) return new BaseResponse<BookingDto>
            {
                Message = "Booking already exist",
                Status = false,
            };

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                TherapistId = model.TherapistId,
                ClientId = clientExist.Id,
                AppointmentDateTime = model.AppointmentDateTime,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                IsDeleted = false,
                
               // IsApproved = false,
            };
            
            await _bookingRepository.Add(booking);
            var that = new Therapist
            {
                IsAvalaible = false,
            };
            await _therapistRepository.Update(that);


            return new BaseResponse<BookingDto>
            {
                Status = true,
                Message = "Created Successfully",
                Data = new BookingDto
                {
                    AppointmentDateTime = model.AppointmentDateTime,
                }
            };
        }

        public async Task<BaseResponse<BookingDto>> Delete(Guid Therapistid)
        {
            var booking = await _bookingRepository.GetBooking(Therapistid);
            if (booking == null) return new BaseResponse<BookingDto>
            {
                Message = "Booking Not Found",
                Status = false,
            };

            booking.IsDeleted = true;

            await _bookingRepository.save();

            return new BaseResponse<BookingDto>
            {
                Message = "Deleted Successfully",
                Status = true,
            };
        }

        public async Task<BaseResponse<IEnumerable<BookingDto>>> GetAll()
        {
            var bookings = await _bookingRepository.GetAll();
            var listOfBookings = bookings.ToList().Select(b => new BookingDto
            {
                ClientId = b.ClientId,
                TherapistId = b.TherapistId,
                AppointmentDateTime = b.AppointmentDateTime,
            });
            return new BaseResponse<IEnumerable<BookingDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfBookings
            };
        }

        public async Task<BaseResponse<BookingDto>> GetBooking(Guid TherapistId)
        {
            var booking = await _bookingRepository.GetBooking(TherapistId);
            if (booking == null) return new BaseResponse<BookingDto>
            {
                Message = "Booking Not Found",
                Status = false,
            };

            return new BaseResponse<BookingDto>
            {
                Message = "Successfull",
                Status = true,
                Data = new BookingDto
                {
                    ClientId =booking.ClientId,
                    TherapistId=booking.TherapistId,
                    AppointmentDateTime=booking.AppointmentDateTime,
                },
            };
        }

        public async Task<BaseResponse<BookingDto>> Update(Guid Therapistid, UpdateBookingRequestModel model)
        {
            var booking = await _bookingRepository.GetBooking(Therapistid);
            if (booking == null) return new BaseResponse<BookingDto>
            {
                Message = "issue not found",
                Status = false,
            };
            booking.AppointmentDateTime = model.AppointmentDateTime;
            await _bookingRepository.Update(booking);

            return new BaseResponse<BookingDto>
            {
                Message = "Successfully Updated",
                Status = true,
                Data = new BookingDto
                {
                    AppointmentDateTime = model.AppointmentDateTime,
                }
            };
        }
    }
}
