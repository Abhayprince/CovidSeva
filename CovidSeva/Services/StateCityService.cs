using CovidSeva.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CovidSeva.Services
{
    public class StateCityService : IStateCityService
    {
        public List<State> GetStates() => State.GetStates().ToList();

        public List<City> GetCities() => City.GetCities().ToList();

        public List<City> GetCitiesByState(int stateId) => City.GetCities().Where(c => c.StateId == stateId).ToList();
    }
}