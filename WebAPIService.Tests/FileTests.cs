using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebAPIService.Controllers;
using Xunit;
using static System.Net.WebRequestMethods;

namespace WebAPIService.Tests
{
  
    public class FileTests
    {
        //[Fact]
        //public async void Test1()
        //{
        //    var file = new Mock<IFormFile>();
        //    //   var sourceFile = System.IO.File.OpenRead(@"wwwroot");
        //    var sourceFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //    var stream = new MemoryStream();
        //    var writer = new StreamWriter(stream);
        //    writer.Write(sourceFile);
        //    writer.Flush();
        //    stream.Position = 0;
        //    var fileName = "bazaro.pdf";
        //    file.Setup(f => f.OpenReadStream()).Returns(stream);
        //    file.Setup(f => f.FileName).Returns(fileName);
        //    file.Setup(f => f.Length).Returns(stream.Length);
        //    var controller = new FileController();
        //    var inputFile = file.Object;
        //    var result = controller.PostFile(inputFile);
        //    //Assert.IsAssignableFrom(result, typeof(IActionResult));

        //}

    }
}
