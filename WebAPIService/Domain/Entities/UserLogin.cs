using System.ComponentModel.DataAnnotations;

namespace WebAPIService.Domain.Entities
{
    public class UserLogin
    {
       [Required]
       public string UserName { get; set; }
       [Required]
       public string Password { get; set; }
        }

    }
