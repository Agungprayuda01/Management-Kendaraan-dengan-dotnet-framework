using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kendaraan.Data;
using Kendaraan.Models;
using Microsoft.AspNetCore.Authorization;

namespace Kendaraan.Controllers
{
    [Authorize]
    public class KendaraanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KendaraanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kendaraan
        public async Task<IActionResult> Index(String searchnameString, String searchregisString)
        {
            var filtersearch = from e in _context.kendaraans select e;
            if (!String.IsNullOrEmpty(searchnameString))
            {
                filtersearch = filtersearch.Where(s => s.NamaPemilik.Contains(searchnameString));
            }
            if (!String.IsNullOrEmpty(searchregisString))
            {
                filtersearch = filtersearch.Where(s => s.NoRegistrasi.Contains(searchregisString));
            };
            filtersearch = filtersearch.OrderBy(e => e.No);
            return View(await filtersearch.ToListAsync());
        }

        // GET: Kendaraan/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kendaraanModel = await _context.kendaraans
                .FirstOrDefaultAsync(m => m.NoRegistrasi == id);
            if (kendaraanModel == null)
            {
                return NotFound();
            }

            return View(kendaraanModel);
        }

        // GET: Kendaraan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kendaraan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoRegistrasi,No,NamaPemilik,Alamat,MerkKendaraan,TahunPembuatan,Kapasitas,Warna,BahanBakar")] KendaraanModel kendaraanModel)
        {
            var check = await _context.kendaraans
                .Where(a => a.NoRegistrasi == kendaraanModel.NoRegistrasi)
                .FirstOrDefaultAsync();

            if (check == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(kendaraanModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            } else
            {
                return BadRequest("nomor registrasi sudah ada");
            }
 
            return View(kendaraanModel);
        }

        // GET: Kendaraan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kendaraanModel = await _context.kendaraans.FindAsync(id);
            if (kendaraanModel == null)
            {
                return NotFound();
            }
            return View(kendaraanModel);
        }

        // POST: Kendaraan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NoRegistrasi,No,NamaPemilik,Alamat,MerkKendaraan,TahunPembuatan,Kapasitas,Warna,BahanBakar")] KendaraanModel kendaraanModel)
        {
            if (id != kendaraanModel.NoRegistrasi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kendaraanModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KendaraanModelExists(kendaraanModel.NoRegistrasi))
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
            return View(kendaraanModel);
        }

        // GET: Kendaraan/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kendaraanModel = await _context.kendaraans
                .FirstOrDefaultAsync(m => m.NoRegistrasi == id);
            if (kendaraanModel == null)
            {
                return NotFound();
            }

            return View(kendaraanModel);
        }

        // POST: Kendaraan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var kendaraanModel = await _context.kendaraans.FindAsync(id);
            if (kendaraanModel != null)
            {
                _context.kendaraans.Remove(kendaraanModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KendaraanModelExists(string id)
        {
            return _context.kendaraans.Any(e => e.NoRegistrasi == id);
        }
    }
}
