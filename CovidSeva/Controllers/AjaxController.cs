using CovidSeva.DataAccess;
using CovidSeva.Services;
using System.Web.Mvc;

namespace CovidSeva.Controllers
{
    public class AjaxController : Controller
    {
        private readonly RecordContext _context;
        private readonly IStateCityService _stateCityService;
        public AjaxController()
        {
            _context = new RecordContext();
            _stateCityService = new StateCityService();
        }

        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }
    }
}