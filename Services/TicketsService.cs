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
    public class TicketsService : ITicketsService
    {
        private readonly IGenericRepository<Ticket> _ticketRepository;

        public TicketsService(IGenericRepository<Ticket> repository)
        {
            _ticketRepository = repository;
        }

        public async Task<List<Ticket>> GetTickets()
        {
            try
            {
                return await _ticketRepository.GetPerformances();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Ticket> CreateTicket(Ticket newTicket)
        {
            try
            {
                return await _ticketRepository.CreateTicket(newTicket);
            }
            catch
            {
                throw;
            }

        }

        public async Task<Ticket> GetTicketById(int id)
        {
            return await _ticketRepository.GetTicketById(id);
        }

        public async Task DeleteTicket(int performanceId)
        {
            await _ticketRepository.DeleteTicket(performanceId);
        }

        public async Task EditTicket(int id, int performanceId, int nrOfPlaces)
        {
            var ticket = await _ticketRepository.GetTicketById(id);

            if (ticket == null)
            {
                throw new ArgumentException("Performance not found");
            }

            ticket.PerformanceId = performanceId;
            ticket.NrOfPlaces = nrOfPlaces;

            await _ticketRepository.UpdateTicket(ticket);
        }
    }
}
