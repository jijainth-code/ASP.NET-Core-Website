﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using basicweb.Data;
using basicweb.Models;

namespace basicweb.Controllers
{
    public class customersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public customersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: customers
        public async Task<IActionResult> Index()
        {
              return View(await _context.customer.ToListAsync());
        }

        // GET: customers/search

        public async Task<IActionResult> Search()
        {
            return View();
        }

        // GET: customers/ShowSearch
        public async Task<IActionResult> ShowSearch(String srch)
        {
            return View("Index" , await _context.customer.Where(j => j.Name.Contains(srch)).ToListAsync()) ;
        }



        // GET: customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.customer == null)
            {
                return NotFound();
            }

            var customer = await _context.customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,amount")] customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.customer == null)
            {
                return NotFound();
            }

            var customer = await _context.customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,amount")] customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!customerExists(customer.Id))
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
            return View(customer);
        }

        // GET: customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.customer == null)
            {
                return NotFound();
            }

            var customer = await _context.customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.customer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.customer'  is null.");
            }
            var customer = await _context.customer.FindAsync(id);
            if (customer != null)
            {
                _context.customer.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool customerExists(int id)
        {
          return _context.customer.Any(e => e.Id == id);
        }
    }
}
