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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BankAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of bank accounts
        /// </summary>
        /// <returns> A list of bank accounts</returns>
        /// <remarks>
        /// 
        /// Sample request
        /// GET /api/bankaccounts
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of bank accounts</response>
        // GET: api/BankAccounts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccounts()
        {
            return await _context.BankAccounts.ToListAsync();
        }


        /// <summary>
        /// Returns a single bank account
        /// </summary>
        /// 
        /// <param name="id">Returns a single bank account</param>
        /// <returns>One bank account</returns>
        /// // GET: single bank account
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Update a bank account
        /// </summary>
        /// <param name="id">Update bank account</param>
        /// <param name="bankAccount">Update bank account</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Create a new bank account
        /// </summary>
        /// <param name="bankAccount">Request's bank account</param>
        /// <returns></returns>
        /// <response code="201">Bank account created successfully</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BankAccount>> PostBankAccount([FromBody] BankAccount bankAccount)
        {
            bankAccount.ID = Guid.NewGuid();

            await _context.BankAccounts.AddAsync(bankAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBankAccount), new { id = bankAccount.ID }, bankAccount);


        }

        /// <summary>
        /// Delete a bank account
        /// </summary>
        /// <param name="id">Delete a bank account</param>
        /// <returns></returns>
        /// <response code="200">Bank account deleted successfully</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
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
