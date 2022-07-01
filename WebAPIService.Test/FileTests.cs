using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPIService.Controllers;
using WebAPIService.Domain.Interfaces;
using WebAPIService.Services;

namespace WebAPIService.Test
{
    [TestClass]
    public class FileTests
    {
        private readonly IServiceFile serviceFile;
        //Access To APIs based on using HTTP Request 
        private HttpClient _client;
        public FileTests()
        {

            //Startup is the Start up of the WebAPIService Project( including All of the Dependencies, contexts and etc).
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            //Createclient is used here in inorder to give an resuest 
            _client = server.CreateClient();

            /* because of using IOC in Filecontroller, the below lines needs to be writen here */
            var services = new ServiceCollection();
            services.AddTransient<IServiceFile, ServiceFile>();
            var serviceProvider = services.BuildServiceProvider();
            serviceFile = serviceProvider.GetService<IServiceFile>();
        }

        [TestMethod]
        public void FilePostTest()
        {

            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Testing A File For Inser on the disk";
            var fileName = "Bazaro.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var sut = new FileController(serviceFile);
            var file = fileMock.Object;

            //Act
            var result = sut.PostFile(file);

            //Assert
            Assert.AreEqual(file, result);
        }
        
        [TestMethod]
        [DataRow("Bazaro.pdf")]  // There is a File in the wwwroot is called Bazaro.pdf
        public void FileDeleteTest(string filename)
        {
            var request = new HttpRequestMessage(new HttpMethod("Delete"), $"/api/File/{filename}");

            // The respone in which is returened   
            var response = _client.SendAsync(request).Result;

            /* 
              Use Assert for executing test.
              The First Parameter is the one thing we want to happen and the second Parameter is returned by API and
              we need both of them be equal  
           */
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


            // [TestMethod]
            // public void FileDeleteTest()
            // {
            //var fileMock = new Mock<IFormFile>();
            //var filename = "Bazaro.pdf";
            //var sut = new FileController(serviceFile);
            //var file = fileMock.Object;

            //Act
            //var result = sut.DeleteFile(filename);

            //Assert
            //Assert.AreEqual(file, result);
        }
    }

}