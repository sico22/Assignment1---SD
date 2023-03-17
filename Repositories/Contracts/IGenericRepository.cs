using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment1.DAL.Models;

namespace Assignment1.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<List<TModel>> GetPerformances();
        Task<Performance> CreatePerformance(Performance newPerformance);
        Task<Performance> GetPerformanceById(int id);
        Task DeletePerformance(int performanceId);
        Task UpdatePerformance(Performance performance);
        Task<List<TModel>> GetTickets();
        Task<Ticket> CreateTicket(Ticket newTicket);
        Task<Ticket> GetTicketById(int id);
        Task DeleteTicket(int ticketId);
        Task UpdateTicket(Ticket ticket);
        Task<Admin> SignUpAdmin(Admin newAdmin);
        Task<Admin> GetAdminById(int id);
        Task<Cashier> SignUpCashier(Cashier newCashier);
        Task<Cashier> GetCashierById(int id);
        IEnumerable<Admin> GetAllAdmins();
        IEnumerable<Cashier> GetAllCashiers();
        Task<List<TModel>> GetCashiers();
        Task<Cashier> CreateCashier(Cashier newCashier);
        Task DeleteCashier(int cashierId);
        Task UpdateCashier(Cashier cashier);
        Admin GetAdminByUsername(string username);
        Cashier GetCashierByUsername(string username);

    }
}
