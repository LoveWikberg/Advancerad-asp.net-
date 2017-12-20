using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Inlämning_Love_Wikberg.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inlämning_Love_Wikberg.Controllers
{
    [Route("api/system")]
    public class SystemInfoController : Controller
    {
        private readonly IHostingEnvironment env;
        private readonly MailConfiguration mailConfiguration;

        public SystemInfoController(IHostingEnvironment env, MailConfiguration mailConfiguration)
        {
            this.env = env;
            this.mailConfiguration = mailConfiguration;
        }

        [HttpGet, Route("systeminfo")]
        public IActionResult GetSystemInfo()
        {
            var systemInfo = new object[]
            {
                $"Is development mode: {env.IsDevelopment() }",
                $"Is live: {env.IsProduction()}",
                $"Path: {env.ContentRootPath}",
                $"App name: {env.ApplicationName}",
                $"Environment: {env.EnvironmentName}",
                $"wwwroot path:: {env.WebRootPath}",
                "-----Email-----",
                $"Is in dev: {mailConfiguration.IsDev}",
                mailConfiguration.BlindEmails,
                $"Send email: {mailConfiguration.IsSendEmail}"
            };
            return Ok(systemInfo);
        }

    }
}
