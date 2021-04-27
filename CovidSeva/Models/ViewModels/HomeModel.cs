using CovidSeva.Models.Entities;
using System.Collections.Generic;

namespace CovidSeva.Models.ViewModels
{
    public class HomeModel
    {
        public List<State> States { get; set; }
        public List<Record> Recent { get; set; }
        public HomeModel()
        {

        }
        public HomeModel(List<State> states, List<Record> recent)
        {
            States = states;
            Recent = recent;
        }
    }
}