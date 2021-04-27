using CovidSeva.Models.Entities;
using System.Collections.Generic;

namespace CovidSeva.Services
{
    public interface IStateCityService
    {
        List<City> GetCities();
        List<City> GetCitiesByState(int stateId);
        List<State> GetStates();
    }
}