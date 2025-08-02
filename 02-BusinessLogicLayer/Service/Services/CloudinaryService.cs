using _02_BusinessLogicLayer.Service.IServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:AK"],
                configuration["Cloudinary:AS"]
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file.Length <= 0)
                throw new ArgumentException("empty file");

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                UseFilename = false,
                UniqueFilename = true,
                Overwrite = false
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("cloudinary upload failed");

            return result.SecureUrl.ToString();
        }



        // upload and return the new url
        public async Task<string> UpdateDocumentAsync(IFormFile newFile)
        {
            return await UploadFileAsync(newFile);
        }






    }
}
