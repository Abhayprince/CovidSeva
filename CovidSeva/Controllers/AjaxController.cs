using CovidSeva.Models.Entities;
using CovidSeva.Models.ViewModels;
using CovidSeva.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<PartialViewResult> Search(SearchModel model)
        {
            var records = await _recordService.SearchRecords(model);
            return PartialView("_RecordsList", records);
        }

        [HttpPost]
        public async Task<JsonResult> Add(Record model)
        {
            var errorList = new List<string>();
            if (ModelState.IsValid)
            {
                var result = await _recordService.SaveRecord(model);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    errorList.Add(result);
                }
            }
            else
            {
                errorList = ModelState.Values
                                .SelectMany(m => m.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
            }
            return Json(errorList);
        }
    }
}