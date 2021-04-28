using CovidSeva.DataAccess;
using CovidSeva.Models.ViewModels;
using CovidSeva.Services;
using System.Web.Mvc;

namespace CovidSeva.Controllers
{
    public class AjaxController : Controller
    {
        private readonly IRecordService _recordService;
        private readonly IStateCityService _stateCityService;
        public AjaxController()
        {
            _recordService = new RecordService();
            _stateCityService = new StateCityService();
        }

        public JsonResult Cities(int stateId)
        {
            var cities = _stateCityService.GetCitiesByState(stateId);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult Search(SearchModel model)
        {
            var records = _recordService.SearchRecords(model);
            return PartialView("_RecordsList",records);
        }
    }
}