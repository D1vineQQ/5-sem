using Microsoft.AspNetCore.Mvc;

namespace lab3a.Controllers
{
    public class Calc : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Calc/CalcPage.cshtml");
        }

        public IActionResult Sum(string inX, string inY)
        {
            ViewBag.press = "+";
            try
            {
                ViewBag.z = 0f;
                if (Request.Method == "POST")
                {
                    float X = float.Parse(inX), Y = float.Parse(inY);
                    ViewBag.z = X + Y;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View("/Views/Calc/CalcPage.cshtml");
        }
        public IActionResult Sub(string inX, string inY)
        {
            ViewBag.press = "-";
            try
            {
                ViewBag.z = 0f;
                if (Request.Method == "POST")
                {
                    float X = float.Parse(inX), Y = float.Parse(inY);
                    ViewBag.z = X - Y;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View("/Views/Calc/CalcPage.cshtml");
        }
        public IActionResult Mul(string inX, string inY)
        {
            ViewBag.press = "*";
            try
            {
                ViewBag.z = 0f;
                if (Request.Method == "POST")
                {
                    float X = float.Parse(inX), Y = float.Parse(inY);
                    ViewBag.z = X * Y;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View("/Views/Calc/CalcPage.cshtml");
        }
        public IActionResult Div(string inX, string inY)
        {
            ViewBag.press = "/";
            try
            {
                ViewBag.z = 0f;
                if (Request.Method == "POST")
                {
                    float X = float.Parse(inX), Y = float.Parse(inY);
                    ViewBag.z = X / Y;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View("/Views/Calc/CalcPage.cshtml");
        }
    }
}
