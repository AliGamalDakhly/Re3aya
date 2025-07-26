using _02_BusinessLogicLayer.DTOs.DocumentDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface ICloudinaryService
    {
        Task<string> UploadFileAsync(IFormFile file);

    
    }

}
