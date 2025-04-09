using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Abstract
{
    public interface IAdminNotificationService
    {

        Task<ResponseDTO<IEnumerable<AdminNotificationDTO>>> GetNotificationsAsync();
        Task<ResponseDTO<AdminNotificationDTO>> MarkisRead(int id);
    }
}
