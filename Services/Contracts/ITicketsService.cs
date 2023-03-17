using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment1.DAL.Models;

namespace Assignment1.BLL.Services.Contracts
{
    public interface ITicketsService
    {
        Task<List<Ticket>> GetTickets();
        Task<Ticket> CreateTicket(Ticket newTicket);
        Task<Ticket> GetTicketById(int id);
        Task DeleteTicket(int TicketId);
        Task EditTicket(int id, int performanceId, int nrOfPlaces);
    }
}
