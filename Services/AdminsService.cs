using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.DAL.Models;
using Assignment1.DAL.Repositories.Contracts;
using Assignment1.BLL.Services.Contracts;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Assignment1.BLL.Services
{
    public class AdminsService : IAdminsService
    {

        private readonly IGenericRepository<Admin> _adminRepository;

        public AdminsService(IGenericRepository<Admin> repository)
        {
            _adminRepository = repository;
        }
        public async Task<Admin> GetAdminById(int id)
        {
            return await _adminRepository.GetAdminById(id);
        }

        public async Task<Admin> SignUpAdmin(Admin newAdmin)
        {
            try
            {
                newAdmin.Password = EncodeToBase64(newAdmin.Password);
                return await _adminRepository.SignUpAdmin(newAdmin);
            }
            catch
            {
                throw;
            }
        }
        
        public static string EncodeToBase64(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public bool SignInAdmin(string username, string password)
        {
            var user = _adminRepository.GetAdminByUsername(username);
            
            if (user == null)
            {
                
                return false;
            }
            
            if (EncodeToBase64(password) != user.Password)
            {
                Console.WriteLine(password);
                return false;
            }

            return true;
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            return _adminRepository.GetAllAdmins();
        }
    }
}
