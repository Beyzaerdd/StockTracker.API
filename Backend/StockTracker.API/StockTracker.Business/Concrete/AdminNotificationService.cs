using AutoMapper;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Http;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Concrete
{
    public class AdminNotificationService : IAdminNotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<AdminNotification> _adminNotification;

        public AdminNotificationService(IGenericRepository<AdminNotification> adminNotification, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _adminNotification = adminNotification;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<IEnumerable<AdminNotificationDTO>>> GetNotificationsAsync()
        {
            var adminNotifications = await _adminNotification.GetAllAsync(null, orderBy: x => x.OrderByDescending(x => x.CreatedAt));
            var adminNotificationDTOs = _mapper.Map<IEnumerable<AdminNotificationDTO>>(adminNotifications);
            if (adminNotificationDTOs.Any())
            {
                return ResponseDTO<IEnumerable<AdminNotificationDTO>>.Success(adminNotificationDTOs, StatusCodes.Status200OK);
            }
            return ResponseDTO<IEnumerable<AdminNotificationDTO>>.Fail("Bildirim bulunamadı", StatusCodes.Status404NotFound);




        }

        public async Task<ResponseDTO<AdminNotificationDTO>> MarkisRead(int id)
        {
            var adminNotification = await _adminNotification.GetByIdAsync(id);
            if (adminNotification == null)
            {
                return ResponseDTO<AdminNotificationDTO>.Fail("Bildirim bulunamadı", StatusCodes.Status404NotFound);
            }
            adminNotification.IsRead = true;
            _adminNotification.Update(adminNotification);
            adminNotification.UpdatedAt = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();
            var adminNotificationDTO = _mapper.Map<AdminNotificationDTO>(adminNotification);
            return ResponseDTO<AdminNotificationDTO>.Success(adminNotificationDTO, StatusCodes.Status200OK);
        }
    }
}
