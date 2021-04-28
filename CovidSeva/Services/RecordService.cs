using CovidSeva.DataAccess;
using CovidSeva.Models.Entities;
using CovidSeva.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidSeva.Services
{
    public class RecordService : IRecordService
    {
        private readonly RecordContext _context;

        public RecordService()
        {
            _context = new RecordContext();
        }

        public async Task<List<Record>> GetRecentlyAddedRecords() =>
            await _context.Records.OrderByDescending(r => r.Id).Take(20).ToListAsync();

        public async Task<string> SaveRecord(Record record)
        {
            if (await _context.Records.AnyAsync(r => r.Contact == record.Contact && r.ServiceType == record.ServiceType))
            {
                return "Already exists";
            }
            _context.Records.Add(record);
            if (await _context.SaveChangesAsync() > 0)
                return "";
            return "Record could not be saved";
        }

        public async Task<List<Record>> SearchRecords(SearchModel search)
        {
            var q = _context.Records.AsQueryable();
            if (search.ServiceType != null)
                q = q.Where(r => r.ServiceType == search.ServiceType);
            if (!string.IsNullOrWhiteSpace(search.Name))
                q = q.Where(r => r.Name.ToLower().Contains(search.Name.ToLower()));
            if (search.StateId > 0)
                q = q.Where(r => r.StateId == search.StateId);
            if (search.CityId > 0)
                q = q.Where(r => r.CityId == search.CityId);
            if (!string.IsNullOrWhiteSpace(search.Freetext))
            {
                var freetext = search.Freetext.ToLower();
                q = q.Where(r =>
                    r.Remarks.ToLower().Contains(freetext) ||
                    r.Name.ToLower().Contains(freetext) ||
                    r.Address.ToLower().Contains(freetext) ||
                    r.StateName.ToLower().Contains(freetext) ||
                    r.CityName.ToLower().Contains(freetext) ||
                    r.Contact.ToLower().Contains(freetext)
                   );
            }

            return await q.Take(100).ToListAsync();
        }
    }
}