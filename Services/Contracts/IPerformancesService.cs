using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment1.DAL.Models;

namespace Assignment1.BLL.Services.Contracts
{
    public interface IPerformancesService
    {
        Task<List<Performance>> GetPerformances();
        Task<Performance> CreatePerformance(Performance newPerformance);
        Task<Performance> GetPerformanceById(int id);
        Task DeletePerformance(int performanceId);
        Task EditPerformance(int id, string artist, string genre, string title, DateTime date, int nrOfTickets);
    }
}
