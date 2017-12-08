using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Inlämning_Love_Wikberg.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inlämning_Love_Wikberg.Controllers
{
    [Route("api/log")]
    public class LogController : Controller
    {
        FileHandler fileHandler;

        public LogController(FileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        [HttpGet, Route("recentLogFile")]
        public IActionResult GetLogFile()
        {
            try
            {
                DirectoryInfo logFolder = new DirectoryInfo($"{Directory.GetCurrentDirectory()}\\Log");
                var mostRecentLogFile = fileHandler.GetAllFilesFromFolder(logFolder).GetMostRecentlyCreatedFile();
                return File(mostRecentLogFile.OpenRead(), "text/plain");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
