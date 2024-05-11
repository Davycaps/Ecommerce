using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECommerce.Controllers
{
    public class LibriController : Controller
    {
        private readonly dbContext _context;
        private const string SessionCartKey = "Cart";

        public LibriController(dbContext context)
        {
            _context = context;
        }

        // GET: Libri
        public async Task<IActionResult> Index()
        {
            return View(await _context.Libri.ToListAsync());
        }

        // GET: Libri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libri = await _context.Libri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libri == null)
            {
                return NotFound();
            }

            return View(libri);
        }

        // GET: Libri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Libri libri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libri);
        }

        // GET: Libri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libri = await _context.Libri.FindAsync(id);
            if (libri == null)
            {
                return NotFound();
            }
            return View(libri);
        }

        // POST: Libri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Libri libri)
        {
            if (id != libri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibriExists(libri.Id))
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
            return View(libri);
        }

        // GET: Libri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libri = await _context.Libri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libri == null)
            {
                return NotFound();
            }

            return View(libri);
        }

        // POST: Libri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libri = await _context.Libri.FindAsync(id);
            if (libri != null)
            {
                _context.Libri.Remove(libri);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibriExists(int id)
        {
            return _context.Libri.Any(e => e.Id == id);
        }

        public IActionResult Shop()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Carrello(int Id)
        {
            var cart = HttpContext.Session.GetString(SessionCartKey);
            var cartIds = new List<int>();

            if (!string.IsNullOrEmpty(cart))
            {
                cartIds = JsonConvert.DeserializeObject<List<int>>(cart);
            }

            cartIds.Add(Id);

            var updatedCart = JsonConvert.SerializeObject(cartIds);

            HttpContext.Session.SetString(SessionCartKey, updatedCart);

            return View("Carrello");
        }
    }
}

