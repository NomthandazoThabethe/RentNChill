using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentNChillMovies.Data;
using RentNChillMovies.Interfaces;
using RentNChillMovies.Models;
using RentNChillMovies.ViewModels;

namespace RentNChillMovies.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly ITransactionRepository _transaction;


        public TransactionsController(ApplicationDbContext context, UserManager<User> userManager, ITransactionRepository _transaction)
        {
            _context = context;
            this.userManager = userManager;
            this._transaction = _transaction;

        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.Account).Include(t => t.Rental);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Rental)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountHolderName");
            ViewData["RentId"] = new SelectList(_context.Rentals, "RentalId", "RentalId");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,AccountId,RentId,TransactionDate,TransactionAmount,IsCompleted")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountHolderName", transaction.AccountId);
            ViewData["RentId"] = new SelectList(_context.Rentals, "RentalId", "RentalId", transaction.RentId);
            return View(transaction);
        }

        // GET: Transacion
        [HttpGet]
        [Authorize(Policy = "readonlypolicy")]
        public IActionResult Transaction(int id)
        {
            var startDate = DateTime.Today.ToShortDateString();
            var endDate = DateTime.Today.AddDays(30).ToShortDateString();
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            var movie = _context.Movies.FirstOrDefault(b => b.MovieId == id);
            return View(new TransactionViewModel
            {
                Movie = movie,
                MovieId = id
            });
      
        }

        // POST: Transaction
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "readonlypolicy")]
        public async Task <IActionResult> Transaction(int id, TransactionViewModel trans)
        {
            var user = userManager.GetUserId(User);
            await _transaction.PostNewTransaction(id, user);
            TempData["Success"] = "Hoooray, you rented a movie!";
            return RedirectToAction("Index", "Movies");

        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountHolderName", transaction.AccountId);
            ViewData["RentId"] = new SelectList(_context.Rentals, "RentalId", "RentalId", transaction.RentId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,AccountId,RentId,TransactionDate,TransactionAmount,IsCompleted")] Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionId))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountHolderName", transaction.AccountId);
            ViewData["RentId"] = new SelectList(_context.Rentals, "RentalId", "RentalId", transaction.RentId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Rental)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
