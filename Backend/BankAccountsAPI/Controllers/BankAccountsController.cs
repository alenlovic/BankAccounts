using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAccountsAPI.Database;
using BankAccountsAPI.Models;

namespace BankAccountsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BankAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/BankAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccounts()
        {
            return await _context.BankAccounts.ToListAsync();
        }


        // GET: single bank account
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetBankAccount")]
        public async Task<ActionResult<BankAccount>> GetBankAccount([FromRoute] Guid id)
        {
            var bankAccount = await _context.BankAccounts.FirstOrDefaultAsync(x => x.ID == id);

            if (bankAccount != null)
            {
                return Ok(bankAccount);
            }

            return NotFound("Card not found");
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBankAccount([FromRoute] Guid id, [FromBody] BankAccount bankAccount)
        {
            var existingBankAccount = await _context.BankAccounts.FirstOrDefaultAsync(x => x.ID == id);

            if (existingBankAccount != null)
            {
                existingBankAccount.CardholderName = bankAccount.CardholderName;
                existingBankAccount.CardNumber = bankAccount.CardNumber;
                existingBankAccount.ExpiryMonth = bankAccount.ExpiryMonth;
                existingBankAccount.ExpiryYear = bankAccount.ExpiryYear;
                existingBankAccount.CVV = bankAccount.CVV;
                await _context.SaveChangesAsync();
                return Ok(existingBankAccount);
            }

            return NotFound("Card not found");
        }

        [HttpPost]
        public async Task<ActionResult<BankAccount>> PostBankAccount([FromBody] BankAccount bankAccount)
        {
            bankAccount.ID = Guid.NewGuid();

            await _context.BankAccounts.AddAsync(bankAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBankAccount), new { id = bankAccount.ID }, bankAccount);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBankAccount([FromRoute] Guid id)
        {
            var existingBankAccount = await _context.BankAccounts.FirstOrDefaultAsync(x => x.ID == id);

            if (existingBankAccount != null)
            {
                _context.Remove(existingBankAccount);
                await _context.SaveChangesAsync();
                return Ok(existingBankAccount);
            }

            return NotFound("Card not found");
        }

        private bool BankAccountExists(Guid id)
        {
            return _context.BankAccounts.Any(e => e.ID == id);
        }
    }
}
