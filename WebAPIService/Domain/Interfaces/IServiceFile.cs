using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAPIService.Domain.Entities;

namespace WebAPIService.Domain.Interfaces
{
    public interface IServiceFile
    {
        Task<IFormFile> AddAsync(IFormFile fileModel);
        string RemoveAsync( string fullpath);
        string CalculateHashCode(string filePath);
   }
}
