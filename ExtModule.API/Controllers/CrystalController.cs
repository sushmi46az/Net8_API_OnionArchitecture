using ExtModule.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net;
using ExtModule.API.Application.Factory;
using ExtModule.API.Application.Interfaces;
using ExtModule.API.Model.DataResponse;
using ExtModule.API.Core;
using ExtModule.API.Infrastructure.Repositories;
using System.Security.Cryptography;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;

namespace ExtModule.API.Controllers
{
    [ApiController]
    public class CrystalController : ControllerBase
    {
        private readonly ICrystalrepository _crystalRepository;
        public CrystalController(ICrystalrepository repositoryCrystal)
        {
            _crystalRepository = repositoryCrystal;
        }

        #region CrystalPrint
        [HttpPost]
        [Route("api/Crystal/CrystalPrint")]
        public async Task<IActionResult> PrintCrystal(ClsPrintCrystal obj)
        {
                    byte[] bytes = await _crystalRepository.PrintCrystal(obj);
            string fileName = obj.ScreenName + "_" + obj.iuserId + ".pdf";
                    //var stream = new MemoryStream(bytes);
                    //var result = new HttpResponseMessage(HttpStatusCode.OK)
                    //{
                    //    Content = new StreamContent(stream)
                    //};
                    //result.Content.Headers.ContentDisposition =
                    //    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    //    {
                    //        FileName = obj.ScreenName + "_" + obj.iuserId + ".pdf"
                    //    };
                    //result.Content.Headers.ContentType =
                    //    new MediaTypeHeaderValue("application/octet-stream");
            return File(bytes, "application/octet-stream", fileName);

            // processing the stream.


        }
        #endregion
    }
}
