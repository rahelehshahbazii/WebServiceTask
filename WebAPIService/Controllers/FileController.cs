using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WebAPIService.Domain.Interfaces;

namespace WebAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
       private IServiceFile _serviceFile;
       
       public FileController(IServiceFile serviceFile)
       {
            this._serviceFile = serviceFile;

       }

       [HttpPost]
       public async Task PostFile(IFormFile fileModel)
       {
            if (!ModelState.IsValid)
            {
                  Response.StatusCode = (int)HttpStatusCode.RequestEntityTooLarge;
            }

           await _serviceFile.AddAsync(fileModel);
           Ok(fileModel);
       }

       [HttpDelete("{filename}")]
       public async Task<IActionResult> DeleteFile([FromRoute] string filename)
       {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\" + filename);
                //var path = "F:\\Source\\WebAPIService\\WebAPIService\\wwwroot\\"+ filename;
                string result = _serviceFile.RemoveAsync(path);

                if (result == StatusCodes.Status200OK.ToString())
                {
                    return Ok();
                }
               else
               {
                    return NotFound();
               }
            }
               catch
            {
                return StatusCode(500, "Internal Server Error");
            }
       }
    }
}

