using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerce.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;


namespace ECommerce.Controllers
{
    public class UtentiController : Controller
    {
        
        private readonly dbContext _context;

        public UtentiController(dbContext context)
        {
            _context = context;
           

        }

        // GET: Utenti
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utente.ToListAsync());
        }

        // GET: Utenti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // GET: Utenti/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Registrati()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        // POST: Utenti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cognome,Username,Password,Tipo")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utente);
        }

        // GET: Utenti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }
            return View(utente);
        }

        // POST: Utenti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cognome,Username,Password,Tipo")] Utente utente)
        {
            if (id != utente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtenteExists(utente.Id))
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
            return View(utente);
        }

        // GET: Utenti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // POST: Utenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utente = await _context.Utente.FindAsync(id);
            if (utente != null)
            {
                _context.Utente.Remove(utente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtenteExists(int id)
        {
            return _context.Utente.Any(e => e.Id == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrati([Bind("Id,Nome,Cognome,Username,Password,Tipo")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Login( Utente user)
        {
            foreach(var i in _context.Utente)
            {    
                if (i.Username == user.Username && i.Password==user.Password)
                {
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("Login", "si");
                    return Task.FromResult<IActionResult>(RedirectToAction("Index","Home"));
                }
            
            }

                ModelState.AddModelError("", "Credenziali non valide");
                return Task.FromResult<IActionResult>(View());
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("Username","");
            HttpContext.Session.SetString("Login", "no");
            return RedirectToAction("Index","Home");
        }


        [HttpPost]
        public IActionResult RegRiuscito(Utente u)
        {
            dbContext db = new dbContext();
            db.Utente.Add(u);
            db.SaveChanges();
            HttpContext.Session.SetString("NomeUtente",u.Username);
            return View(u);
        }

    }
}
