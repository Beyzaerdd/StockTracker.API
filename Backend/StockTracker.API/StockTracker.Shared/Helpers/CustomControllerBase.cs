using Microsoft.AspNetCore.Mvc;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Shared.Helpers
{
    public class CustomControllerBase : ControllerBase
    {
        public static IActionResult CreateResponse<T>(ResponseDTO<T> responseDTO)
        {
            return new ObjectResult(responseDTO)
            {
                StatusCode = responseDTO.StatusCode
            };
        }
    }
}
