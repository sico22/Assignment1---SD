using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment1.DAL.Models;

namespace Assignment1.BLL.Services.Contracts
{
    public interface IAdminsService
    {
        Task<Admin> SignUpAdmin(Admin newAdmin);
        bool SignInAdmin(string username, string password);
        Task<Admin> GetAdminById(int id);

        IEnumerable<Admin> GetAllAdmins();
    }
}
