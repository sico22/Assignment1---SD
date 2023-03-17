using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Assignment1.BLL.Services.Contracts;
using Assignment1.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Assignment1.PL.Controllers
{
    public class HomeController : Controller
    {
        public List<Performance> performancesPersistent;
        public List<Ticket> ticketPersistent;
        public List<Admin> adminPersistent;
        public List<Cashier> cashierPersistent;

        private readonly IPerformancesService _performancesService;
        private readonly ITicketsService _ticketsService;
        private readonly IAdminsService _adminsService;
        private readonly ICashiersService _cashiersService;
        bool isCashierLoggedIn = false;
        bool isAdminLoggedIn = false;
        public HomeController(IPerformancesService performancesService, ITicketsService ticketsService, IAdminsService adminsService, ICashiersService cashiersService)
        {
            _performancesService = performancesService;
            _ticketsService = ticketsService;
            _adminsService = adminsService;
            _cashiersService = cashiersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
          return View();
        }

        public IActionResult LogOut()
        {
            isCashierLoggedIn = false;
            isAdminLoggedIn = false;
            return RedirectToAction("SignInAdmin");
        }

        public async Task<IActionResult> ShowPerformances()
        {
            List<Performance> performances = await _performancesService.GetPerformances();
            performancesPersistent = performances;
            return View(performances);
        }

        public async Task<IActionResult> ShowTickets()
        {
            List<Ticket> tickets = await _ticketsService.GetTickets();
            ticketPersistent = tickets;
            return View(tickets);
        }

        public async Task<IActionResult> ShowCashiers()
        {
            List<Cashier> cashiers = await _cashiersService.GetCashiers();
            cashierPersistent = cashiers;
            return View(cashiers);
        }

        public async Task<ActionResult> form1(string txtArtist, string txtGenre, string txtTitle, DateTime txtDate, int txtNoTickets)
        {
            Performance performance = new Performance
            {
                Artist = txtArtist,
                Genre = txtGenre,
                Title = txtTitle,
                Date = txtDate,
                NrOfTickets = txtNoTickets
            };

            Console.WriteLine(performance.PerformanceId);

            
            await _performancesService.CreatePerformance(performance);

            return RedirectToAction("ShowPerformances");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var performance = await _performancesService.GetPerformanceById(id);

            if (performance == null)
            {
                return NotFound();
            }

            
            await _performancesService.DeletePerformance(performance.PerformanceId);

            return RedirectToAction("ShowPerformances");
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var performance = await _performancesService.GetPerformanceById(id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int performanceId, string artist, string genre, string title, DateTime date, int nrOfTickets)
        {

            
            await _performancesService.EditPerformance(performanceId, 
                artist, genre, title, 
                date, nrOfTickets);
            return RedirectToAction("ShowPerformances");
        }

        [HttpGet]
        public async Task<ActionResult> NewTicket(int id)
        {
            var performance = await _performancesService.GetPerformanceById(id);
            if (performance == null)
            {
                return NotFound();
            }

            return View();
        }

        
        [HttpPost]
        public async Task<ActionResult> NewTicket(int performanceId, int nrOfPlaces)
        {
            var performance = await _performancesService.GetPerformanceById(performanceId);

            Ticket newTicket = new Ticket
            {
                PerformanceId = performanceId,
                NrOfPlaces = nrOfPlaces
            };

            if(nrOfPlaces > performance.NrOfTickets)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not enough tickets" });
            }

            await _performancesService.EditPerformance(performance.PerformanceId,
                performance.Artist, performance.Genre, performance.Title,
                performance.Date, performance.NrOfTickets - nrOfPlaces);
            await _ticketsService.CreateTicket(newTicket);
            return RedirectToAction("ShowTickets");
        }

        public async Task<ActionResult> DeleteTicket(int id)
        {
            var ticket = await _ticketsService.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }


            await _ticketsService.DeleteTicket(ticket.TicketId);

            return RedirectToAction("ShowTickets");
        }

        
        [HttpGet]
        public async Task<ActionResult> EditTicket(int id)
        {
            var ticket = await _ticketsService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost]
        public async Task<ActionResult> EditTicket(int ticketId, int performanceId, int nrOfPlaces)
        {
            await _ticketsService.EditTicket(ticketId, performanceId, nrOfPlaces);
            return RedirectToAction("ShowTickets");
        }

        
        public async Task<ActionResult> ExportTickets(int id)
        {
            List<Ticket> tickets = await _ticketsService.GetTickets();

            try
            {
                var builder = new StringBuilder();
                builder.Append("TicketId,PerformanceId,NrOfPlaces");
                foreach (var ticket in tickets)
                {
                    if (ticket.TicketId == id)
                    {
                        builder.AppendLine($"{ticket.TicketId }, {ticket.PerformanceId}, {ticket.NrOfPlaces}");
                    }
                }
                return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "tickets.csv");
            }
            catch
            {
                throw;
            }
          
        }

        private ActionResult File(object p)
        {
            throw new NotImplementedException();
        }

        private Task<bool> PerformanceExists(int performanceId)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        public async Task<ActionResult> SignUpAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SignUpAdmin(string username, string password)
        {
            int id = _adminsService.GetAllAdmins().Count() + 1;
            Admin newAdmin = new Admin
            {
                AdminId = id,
                Username = username,
                Password = password
            };


            await _adminsService.SignUpAdmin(newAdmin);
            isAdminLoggedIn = true;
            return RedirectToAction("ShowPerformances");
        }

        [HttpGet]
        public async Task<ActionResult> SignUpCashier()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SignUpCashier(string username, string password)
        {
            int id = _cashiersService.GetAllCashiers().Count() + 1;

            Cashier newCashier = new Cashier
            {
                CashierId = id,
                Username = username,
                Password = password
            };


            await _cashiersService.SignUpCashier(newCashier);
            isCashierLoggedIn = true;
            return RedirectToAction("ShowPerformances");
        }

        [HttpGet]
        public async Task<ActionResult> NewCashier()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> NewCashier(string username, string password)
        {
            int id = _adminsService.GetAllAdmins().Count() + 1;
            Cashier newCashier = new Cashier
            {
                CashierId = id,
                Username = username,
                Password = password
            };

            await _cashiersService.CreateCashier(newCashier);
            return RedirectToAction("ShowCashiers");
        }

        public async Task<ActionResult> DeleteCashier(int id)
        {
            var cashier = await _cashiersService.GetCashierById(id);

            if (cashier == null)
            {
                return NotFound();
            }


            await _cashiersService.DeleteCashier(cashier.CashierId);

            return RedirectToAction("ShowCashiers");
        }


        [HttpGet]
        public async Task<ActionResult> EditCashier(int id)
        {
            var cashier = await _cashiersService.GetCashierById(id);
            if (cashier == null)
            {
                return NotFound();
            }

            return View(cashier);
        }

        [HttpPost]
        public async Task<ActionResult> EditCashier(int cashierId, string username, string password)
        {
            await _cashiersService.EditCashier(cashierId, username, password);
            return RedirectToAction("ShowCashiers");
        }

        [HttpGet]
        public async Task<ActionResult> SignInCashier()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignInCashier(string username, string password)
        {
            try
            {
                // Call the SignInCashier method from the CashierBLL class
                isCashierLoggedIn = _cashiersService.SignInCashier(username, password);

            }
            catch (Exception ex)
            {
                throw;
            }

            ViewBag.IsCashierLoggedIn = isCashierLoggedIn;
            if(isCashierLoggedIn)
            {
                return RedirectToAction("ShowCashiers");
            }
            else
            {
                return RedirectToAction("SignInCashier");
            }
                


        }

        public async Task<ActionResult> SignInAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignInAdmin(string username, string password)
        {
            try
            {
                // Call the SignInCashier method from the CashierBLL class
                isAdminLoggedIn = _adminsService.SignInAdmin(username, password);
            }
            catch (Exception ex)
            {
                throw;
            }

            ViewBag.isAdminLoggedIn = isAdminLoggedIn;
            if (isAdminLoggedIn)
            {
                return RedirectToAction("ShowPerformances");
            }
            else
            {
                return RedirectToAction("SignInAdmin");
            }    
                


        }

        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}