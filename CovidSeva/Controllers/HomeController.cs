using CovidSeva.Models.ViewModels;
using CovidSeva.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CovidSeva.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStateCityService _stateCityService;
        private readonly IRecordService _recordService;
        public HomeController()
        {
            _stateCityService = new StateCityService();
            _recordService = new RecordService();
        }
        public async Task<ActionResult> Index()
        {
            var recentTask = _recordService.GetRecentlyAddedRecords();
            var states = _stateCityService.GetStates();
            var model = new HomeModel(states, await recentTask);
            return View(model);
        }
    }
}