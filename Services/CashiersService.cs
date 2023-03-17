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
    public class CashiersService : ICashiersService
    {
        private readonly IGenericRepository<Cashier> _cashierRepository;

        public CashiersService(IGenericRepository<Cashier> repository)
        {
            _cashierRepository = repository;
        }
        public async Task<Cashier> GetCashierById(int id)
        {
            return await _cashierRepository.GetCashierById(id);
        }

        public async Task<Cashier> SignUpCashier(Cashier newCashier)
        {
            try
            {
                newCashier.Password = EncodeToBase64(newCashier.Password);
                return await _cashierRepository.SignUpCashier(newCashier);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Cashier> GetAllCashiers()
        {
            return _cashierRepository.GetAllCashiers();
        }

        public async Task<List<Cashier>> GetCashiers()
        {
            try
            {
                return await _cashierRepository.GetCashiers();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Cashier> CreateCashier(Cashier newCashier)
        {
            try
            {
                return await _cashierRepository.CreateCashier(newCashier);
            }
            catch
            {
                throw;
            }

        }

        public async Task DeleteCashier(int cashierId)
        {
            await _cashierRepository.DeleteCashier(cashierId);
        }

        public async Task EditCashier(int id, string username, string password)
        {
            var cashier = await _cashierRepository.GetCashierById(id);

            if (cashier == null)
            {
                throw new ArgumentException("Cashier not found");
            }

            cashier.Username = username;
            cashier.Password = password;

            await _cashierRepository.UpdateCashier(cashier);
        }
        
        public static string EncodeToBase64(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public bool SignInCashier(string username, string password)
        {
            var user = _cashierRepository.GetCashierByUsername(username);
            if (user == null)
            {
                return false;
            }

            if (EncodeToBase64(password) != user.Password)
            {
                return false;
            }

            return true;
        }
    }
}
