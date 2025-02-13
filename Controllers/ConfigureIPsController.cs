using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResourcesWebApplication.Models.Cisco;
using ResourcesWebApplication.Models.Context;

namespace ResourcesWebApplication.Controllers
{
    [Route("[controller]")]
    public class ConfigureIPsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ConfigureIPsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexLogin()
        {
            var login = await _context.Logins.Take(8).OrderByDescending(d => d.Id).ToListAsync();
            return View(login);
        }
        public async Task<IActionResult> IndexAddress()
        {
            var address = await _context.IPAddresses.Take(8).OrderByDescending(d => d.Id).ToListAsync();
            return View(address);
        }
        public async Task<IActionResult> IndexPort()
        {
            var port = await _context.Ports.Take(8).OrderByDescending(d => d.Id).ToListAsync();
            return View(port);
        }

        public async Task<IActionResult> DetailsLogin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins.FirstOrDefaultAsync(d => d.Id == id);

            if (login == null)
            {
                return NotFound();
            }


            return View(login);
        }
        public async Task<IActionResult> DetailsAddress(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.IPAddresses.FirstOrDefaultAsync(d => d.Id == id);

            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }
        public async Task<IActionResult> DetailsPort(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var port = await _context.Ports.FirstOrDefaultAsync(d => d.Id == id);

            if (port == null)
            {
                return NotFound();
            }
            return View(port);
        }
        public IActionResult CreateLogin()
        {
            return View();
        }
        public IActionResult CreatePort()
        {
            return View();
        }
        public IActionResult CreateAddress()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetIPAddressIDs()
        {
            var address = await _context.IPAddresses.Distinct().OrderByDescending(d => d.Id).ToListAsync();
            List<string> ipAddressesIDs = new List<string>();
            foreach (var item in address)
            {
                ipAddressesIDs.Add(item.Id.ToString());
            }
            return Ok(ipAddressesIDs);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginsIDs()
        {
            var login = await _context.Logins.Distinct().OrderByDescending(d => d.Id).ToListAsync();
            List<string> loginIDs = new List<string>();
            foreach (var item in login)
            {
                loginIDs.Add(item.Id.ToString());
            }
            return Ok(loginIDs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLogin([Bind("Id,Username,Password,CreatedAT")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePort([Bind("Id,IPAdressID,Name,Number,CreatedAT")] Port port)
        {
            if (ModelState.IsValid)
            {
                _context.Add(port);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(port);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAddress([Bind("Id,LoginID,Name,InterfaceName,IP,Subnet,CreatedAT")] IPAddress iPAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iPAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(iPAddress);
        }

        public async Task<IActionResult> EditLogin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLogin(int id, [Bind("Id,Username,Password,CreatedAT")] Login login)
        {
            if (id != login.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }
        public async Task<IActionResult> EditAddress(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.IPAddresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAddress(int id, [Bind("Id,LoginID,Name,InterfaceName,IP,Subnet,CreatedAT")] IPAddress address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }
        public async Task<IActionResult> EditPort(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var port = await _context.Ports.FindAsync(id);
            if (port == null)
            {
                return NotFound();
            }
            return View(port);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPort(int id, [Bind("Id,IPAdressID,Name,Number,CreatedAT")] Port port)
        {
            if (id != port.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(port);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortExists(port.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(port);
        }
        private bool LoginExists(int id)
        {
            return _context.Logins.Any(e => e.Id == id);
        }
        private bool AddressExists(int id)
        {
            return _context.IPAddresses.Any(e => e.Id == id);
        }
        private bool PortExists(int id)
        {
            return _context.Ports.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteLogin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Plaintexts/Delete/5
        [HttpPost, ActionName("DeleteLogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginDeleteConfirmed(int id)
        {
            var login = await _context.Logins.FindAsync(id);
            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAddress(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.IPAddresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Plaintexts/Delete/5
        [HttpPost, ActionName("DeleteAddress")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressDeleteConfirmed(int id)
        {
            var address = await _context.IPAddresses.FindAsync(id);
            _context.IPAddresses.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeletePort(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var port = await _context.Ports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (port == null)
            {
                return NotFound();
            }

            return View(port);
        }

        // POST: Plaintexts/Delete/5
        [HttpPost, ActionName("DeletePort")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PortDeleteConfirmed(int id)
        {
            var port = await _context.Ports.FindAsync(id);
            _context.Ports.Remove(port);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}