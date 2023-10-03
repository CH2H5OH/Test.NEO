using Microsoft.AspNetCore.Mvc;

namespace Programmer.Test.NEO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgrammController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(FileStreamResult), 200)]
        public async Task<FileStreamResult> GetFile1(SineWave sw)
        {
            Stream b = ImageHelperSkiaSharp.CreateImageFile(sw);

            return File(b, "image/jpeg", "gofgo.jpeg");
        }
    }
}