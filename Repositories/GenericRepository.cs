using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment1.DAL.DataContext;
using Assignment1.DAL.Models;
using Assignment1.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;


namespace Assignment1.DAL.Repositories
{
    public class GenericRepository<Tmodel> : IGenericRepository<Tmodel> where Tmodel : class
    {
        private readonly Assignment1Context _context;

        public GenericRepository(Assignment1Context context)
        {
            _context = context;
        }

        public async Task<Performance> CreatePerformance(Performance newPerformance)
        {
            await _context.Performances.AddAsync(newPerformance);
            await _context.SaveChangesAsync();
            return newPerformance;
        }

        public async Task<List<Tmodel>> GetPerformances()
        {
            try
            {
                return await _context.Set<Tmodel>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Performance> GetPerformanceById(int id)
        {
            return await _context.Performances.FindAsync(id);
        }

        public async Task DeletePerformance(int performanceId)
        {
            var performance = await _context.Performances.FindAsync(performanceId);

            if (performance == null)
            {
                throw new ArgumentException($"Performance with id {performanceId} does not exist.");
            }

            _context.Performances.Remove(performance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePerformance(Performance performance)
        {
            _context.Performances.Update(performance);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Tmodel>> GetTickets()
        {
            try
            {
                return await _context.Set<Tmodel>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<Ticket> CreateTicket(Ticket newTicket)
        {
            await _context.Tickets.AddAsync(newTicket);
            await _context.SaveChangesAsync();
            return newTicket;
        }

        public async Task DeleteTicket(int ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);

            if (ticket == null)
            {
                throw new ArgumentException($"Ticket with id {ticketId} does not exist.");
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }


        public async Task<Admin> SignUpAdmin(Admin newAdmin)
        {
            await _context.Admins.AddAsync(newAdmin);
            await _context.SaveChangesAsync();
            return newAdmin;
        }

        public async Task<Admin> GetAdminById(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task<Cashier> SignUpCashier(Cashier newCashier)
        {
            await _context.Cashiers.AddAsync(newCashier);
            await _context.SaveChangesAsync();
            return newCashier;
        }

        public async Task<Cashier> GetCashierById(int id)
        {
            return await _context.Cashiers.FindAsync(id);
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            return _context.Admins.ToList();
        }

        public IEnumerable<Cashier> GetAllCashiers()
        {
            return _context.Cashiers.ToList();
        }

        public async Task<Cashier> CreateCashier(Cashier newCashier)
        {
            await _context.Cashiers.AddAsync(newCashier);
            await _context.SaveChangesAsync();
            return newCashier;
        }

        public async Task<List<Tmodel>> GetCashiers()
        {
            try
            {
                return await _context.Set<Tmodel>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }


        public async Task DeleteCashier(int cashierId)
        {
            var cashier = await _context.Cashiers.FindAsync(cashierId);

            if (cashier == null)
            {
                throw new ArgumentException($"Performance with id {cashierId} does not exist.");
            }

            _context.Cashiers.Remove(cashier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCashier(Cashier cashier)
        {
            _context.Cashiers.Update(cashier);
            await _context.SaveChangesAsync();
        }

        public Admin GetAdminByUsername(string username)
        {
            return _context.Admins.FirstOrDefault(u => u.Username == username);
        }

        public Cashier GetCashierByUsername(string username)
        {
            
            return _context.Cashiers.FirstOrDefault(u => u.Username == username);
        }

    }
}
