using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.Helpers;

namespace StockTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : CustomControllerBase
    {
        private readonly IAdminNotificationService _admin;

        public NotificationsController(IAdminNotificationService admin)
        {
            _admin = admin;
        }

        [HttpGet("getNotifications")]
        public async Task<IActionResult> GetNotifications()
        {
        var RESPONSE = await _admin.GetNotificationsAsync();
            return CreateResponse(RESPONSE);
        }

        [HttpPut("markAsRead/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var RESPONSE = await _admin.MarkisRead(id);
            return CreateResponse(RESPONSE);
        }
    }
}
