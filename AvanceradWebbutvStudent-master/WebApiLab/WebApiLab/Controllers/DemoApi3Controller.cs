using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApiLab.Controllers
{
    [Route("demo3")]
    public class DemoApi3Controller : Controller
    {

        [HttpGet, Route("CheckFileFormat")]
        public IActionResult CheckFileFormat(string fileformat)
        {
            fileformat = (fileformat ?? "").ToUpper();

            if (fileformat.Length!=3)
            {
                return BadRequest("Fel format");
            }

            if (fileformat == "GIF")
            {
                throw new Exception("Något gick snett");
            }

            if (fileformat == "CAT")
            {
                var caturl = "https://media.giphy.com/media/bXEcr96oNPllu/giphy-facebook_s.jpg";
                return Content($"<h1>En katt</h1><img src='{caturl}'/>", "text/html");
            }

            if (fileformat == "PDF" || fileformat == "DOC" || fileformat == "JPG")
            {
                return Ok($"{fileformat} är ett fint filformat");
            }
            else
            {
                return NotFound($"Hittar inte filformatet {fileformat}");
            }

        }

    }
}