using CovidSeva.Models.Entities;
using CovidSeva.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidSeva.Services
{
    public interface IRecordService
    {
        Task<List<Record>> GetRecentlyAddedRecords();
        Task<string> SaveRecord(Record record);
        Task<List<Record>> SearchRecords(SearchModel search);
    }
}