using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAPIService.Domain.Entities;
using WebAPIService.Domain.Interfaces;

namespace WebAPIService.Services
{
    public class ServiceFile : IServiceFile
    {
        private IServiceFile _service;
        
        public async Task <IFormFile> AddAsync(IFormFile fileModel)
        {

             var directorypath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
             string filepath = Path.Combine(directorypath, fileModel.FileName);

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                fileModel.CopyTo(stream);
            }

            string HashCode = CalculateHashCode(filepath);
            return fileModel;
         }
        public string CalculateHashCode(string filePath)
        {
            var cryptoService = new SHA256CryptoServiceProvider();
            using (cryptoService)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))

                {
                    var hash = cryptoService.ComputeHash(fileStream);
                    var hashString = Convert.ToBase64String(hash);
                    return hashString.TrimEnd('=');
                }
            }
        }
        public string RemoveAsync(string fullpath)
        {
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
                return StatusCodes.Status200OK.ToString();
            }
            else
            {
                return StatusCodes.Status404NotFound.ToString();
            }
        }

    }
}
