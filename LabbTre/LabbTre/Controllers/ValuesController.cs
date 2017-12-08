using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using LabbTre.Models;

namespace LabbTre.Controllers
{
    [Route("api/values")]
    public class ValuesController : Controller
    {
        //DataStorage datastg;
        //public ValuesController(DataStorage datastg)
        //{
        //    this.datastg = datastg;
        //}

        [HttpGet, Route("setlight")]
        public IActionResult SetLight(bool isLightOn)
        {
            if (isLightOn)
                return Content("<body style='background-color:yellow'></body>", "text/html");
            return Content("<body style='background-color:gray'></body>", "text/html");
        }

        [HttpGet, Route("sharechocolate")]
        public IActionResult ShareChocolate(double? number)
        {
            if (number <= 0 || number == null)
                return BadRequest("Så kan du inte göra, ju.");

            return Ok($"Det blir {Convert.ToDecimal(25 / number)} chocklad per person.");
        }

        [HttpGet, Route("getorder")]
        public IActionResult GetOrder(string order)
        {
            DataStorage datastg = new DataStorage();
            order = order.ToUpper();
            //Format must be XX-YYYY where X is a letter from A-Z and Y is a digit
            if (Regex.IsMatch(order, @"^[A-Z]{2}-\d{4}$"))
            {
                if (int.Parse(order.Substring(3)) >= 3000 || !datastg.orders.Select(o => o.Id).Contains(order))
                    return NotFound("Hittade inte ordern");
                return Ok($"Order {order} hittades i databasen \nOrdern säger \"{datastg.orders.SingleOrDefault(o => o.Id == order).Text}\"");
            }
            return BadRequest("Felaktigt format");
        }

        [HttpGet, Route("username")]
        public IActionResult Username(string name)
        {
            switch (name.ToLower())
            {
                case "stewie":
                    throw new Exception("DATA ERROR");
                case "peter":
                    return Content("<img src='https://opengameart.org/sites/default/files/Preview_107.png' />", "text/html");
                case "lois":
                case "meg":
                case "chris":
                case "brian":
                    return Content("<img src='https://thumbs.dreamstime.com/z/two-big-thumps-up-17948763.jpg' />", "text/html");
                default:
                    return Content("<img src='http://assets.nydailynews.com/polopoly_fs/1.3559445.1507866312!/img/httpImage/image.jpg_gen/derivatives/article_750/thumbsdown13s-1-web.jpg' />", "text/html");
            }
        }
    }
}
