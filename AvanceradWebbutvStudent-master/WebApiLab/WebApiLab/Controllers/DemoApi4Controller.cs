using Microsoft.AspNetCore.Mvc;
using WebApiLab.Models;

namespace WebApiLab.Controllers
{
    [Route("demo4")]
    public class DemoApi4Controller : Controller
    {

        [HttpPost, Route("AddMeeting")]
        public IActionResult AddMeeting(Meeting meeting)
        {
            return Ok($"Du vill lägga till mötet '{meeting.Name}' med agendan '{meeting.Agenda}'");
        }
    }
}