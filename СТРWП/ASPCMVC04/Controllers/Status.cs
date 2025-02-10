using Microsoft.AspNetCore.Mvc;

namespace ASPCMVC04.Controllers
{
    public class Status : Controller
    {
        [HttpGet]
        public IActionResult S200()
        {
            return View();
        }
        public IActionResult S300()
        {
            return Redirect("/Index.html");
        }
        public string S500()
        {
            throw new System.Exception("Code 500");
            return "oaoaoa";
        }
    }
}
