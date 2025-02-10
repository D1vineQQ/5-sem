using Microsoft.AspNetCore.Mvc;

namespace ASPCMVC06.Controllers
{
    public class TMResearchController : Controller
    {
        public string M01(string id)
        {
            return "GET:M01";
        }
        public string M02(string id)
        {
            return "GET:M02";
        }
        public string M03(string id)
        {
            return "GET:M03";
        }
        public string MXX(string id)
        {
            return "GET:MXX";
        }
    }
}
