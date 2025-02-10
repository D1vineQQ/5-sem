using Microsoft.AspNetCore.Mvc;

namespace ASPCMVC07.Controllers
{
    [Route("it")]
    public class TAResearch : Controller
    {
        [Route("{n:int}/{s}"), HttpGet]
        public ActionResult M04(int n, string s)
        {
            return Content($"GET:M04: /{n}/{s}");
        }
        [Route("{b:bool}/{letters:alpha}"), HttpGet, HttpPost]
        public ActionResult M05(bool b, string letters)
        {
            return Content($"{this.HttpContext.Request.Method}:M05: /{b}/{letters}");
        }
        [Route("{f:float}/{s:minlength(2):maxlength(5)}"), HttpGet, HttpDelete]
        public ActionResult M06(float f, string s)
        {
            return Content($"{this.HttpContext.Request.Method}:M06: /{f}/{s}");
        }
        [Route("{letters:alpha:minlength(3):maxlength(4)}/{n:int:min(100):max(200)}/"), HttpPut]
        public ActionResult M07(string letters, int n)
        {
            return Content($"PUT:M07: /{letters}/{n}/");
        }
        [Route("{mail:regex(^[[^@\\s]]+@[[^@\\s]]+.[[^@\\s]]+$)}"), HttpPost]
        public ActionResult M08(string mail)
        {
            return Content($"POST:M08/{mail}");
        }
    }
}
