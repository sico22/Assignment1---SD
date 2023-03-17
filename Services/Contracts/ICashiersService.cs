using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment1.DAL.Models;

namespace Assignment1.BLL.Services.Contracts
{
    public interface ICashiersService
    {
        Task<Cashier> SignUpCashier(Cashier newCashier);
        bool SignInCashier(string username, string password);
        Task<Cashier> GetCashierById(int id);
        IEnumerable<Cashier> GetAllCashiers();
        Task<List<Cashier>> GetCashiers();
        Task<Cashier> CreateCashier(Cashier newCashier);
        Task DeleteCashier(int cashierId);
        Task EditCashier(int id, string username, string password);
    }
}
