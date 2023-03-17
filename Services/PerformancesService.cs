using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.DAL.Models;
using Assignment1.DAL.Repositories.Contracts;
using Assignment1.BLL.Services.Contracts;

namespace Assignment1.BLL.Services
{
    public class PerformancesService : IPerformancesService
    {
        private readonly IGenericRepository<Performance> _performanceRepository;

        public PerformancesService(IGenericRepository<Performance> repository)
        {
            _performanceRepository = repository;
        }
       
        public async Task<List<Performance>> GetPerformances()
        {
            try
            {
                return await _performanceRepository.GetPerformances();
            }   
            catch
            {
                throw;
            }
        }

        public async Task<Performance> CreatePerformance(Performance newPerformance)
        {
            try
            {
                return await _performanceRepository.CreatePerformance(newPerformance);
            }
            catch
            {
                throw;
            }
            
        }

        public async Task<Performance> GetPerformanceById(int id)
        {
            return await _performanceRepository.GetPerformanceById(id);
        }

        public async Task DeletePerformance(int performanceId)
        {
            await _performanceRepository.DeletePerformance(performanceId);
        }

        public async Task EditPerformance(int id, string artist, string genre, string title, DateTime date, int nrOfTickets)
        {
            var performance = await _performanceRepository.GetPerformanceById(id);

            if (performance == null)
            {
                throw new ArgumentException("Performance not found");
            }

            performance.Artist = artist;
            performance.Genre = genre;
            performance.Title = title;
            performance.Date = date;
            performance.NrOfTickets = nrOfTickets;

            await _performanceRepository.UpdatePerformance(performance);
        }
    }
}
