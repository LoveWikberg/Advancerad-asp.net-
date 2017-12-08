using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using LabbFem.Models;

namespace LabbFem.Controllers
{
    [Route("api/values")]
    public class ValuesController : Controller
    {
        DataHandler datahandler;
        public ValuesController(DataHandler datahandler)
        {
            this.datahandler = datahandler;
        }

        [HttpGet, Route("customer")]
        public IActionResult Customer(ApiCall apicall)
        {
            if (apicall.Id == null || apicall.Id < 1)
                return BadRequest("Felaktigt format");
            try
            {
                return Ok(datahandler.GetOneCustomer<object>(apicall));
            }
            catch (FileNotFoundException exc)
            {
                return NotFound($"Kunde inte hitta filen | {exc.Message}");
            }
            catch (ArgumentNullException exc)
            {
                return NotFound($"Kunden med id {apicall.Id} hittades inte  | {exc.Message}");
            }
            catch (NullReferenceException exc)
            {
                return NotFound($"Kunden med id {apicall.Id} hittades inte  | {exc.Message}");
            }
            catch (Exception exc)
            {
                return NotFound($"Något gick fel | {exc.Message}");
            }
        }

        [HttpPost, Route("customer")]
        public IActionResult Customer(AddCustomerVM viewModel)
        {
            try
            {
                var test = viewModel;
                datahandler.AddCustomerToTextFile(viewModel);
                return Ok("Customer successfully added!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("allcustomers")]
        public IActionResult AllCustomers()
        {
            return Ok(datahandler.GetAllCustomersFromTextFile());
        }

        [HttpDelete, Route("customer")]
        public IActionResult Customer(int id)
        {
            try
            {
                datahandler.RemoveLineFromTextFile(id);
                return Ok(datahandler.GetAllCustomersFromTextFile());
            }
            catch (Exception e)
            {
                return BadRequest($"Lyckades inte ta bort användaren | {e.Message}");
            }
        }

        [HttpOptions, Route("customer")]
        public IActionResult Customer(CustomerVM viewModel)
        {
            try
            {
                datahandler.EditCustomerInTextFile(viewModel);
                return Ok(datahandler.GetAllCustomersFromTextFile());
            }
            catch (Exception e)
            {
                return BadRequest($"Failed to edit customer | {e.Message}");
            }

        }

    }
}
