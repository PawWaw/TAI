using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DelivererRatesController : ControllerBase
    {
        private readonly DragorantContext _context;

        public DelivererRatesController(DragorantContext context)
        {
            _context = context;
        }

        // GET: api/DelivererRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DelivererRate>>> GetDelivererRates()
        {
            return await _context.DelivererRates.ToListAsync();
        }

        // GET: api/DelivererRates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DelivererRate>> GetDelivererRate(long id)
        {
            var delivererRate = await _context.DelivererRates.FindAsync(id);

            if (delivererRate == null)
            {
                return NotFound();
            }

            return delivererRate;
        }

        // PUT: api/DelivererRates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivererRate(long id, DelivererRate delivererRate)
        {
            if (id != delivererRate.Id)
            {
                return BadRequest();
            }

            _context.Entry(delivererRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DelivererRateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DelivererRates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DelivererRate>> PostDelivererRate(DelivererRate delivererRate)
        {
            _context.DelivererRates.Add(delivererRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDelivererRate", new { id = delivererRate.Id }, delivererRate);
        }

        // DELETE: api/DelivererRates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DelivererRate>> DeleteDelivererRate(long id)
        {
            var delivererRate = await _context.DelivererRates.FindAsync(id);
            if (delivererRate == null)
            {
                return NotFound();
            }

            _context.DelivererRates.Remove(delivererRate);
            await _context.SaveChangesAsync();

            return delivererRate;
        }

        private bool DelivererRateExists(long id)
        {
            return _context.DelivererRates.Any(e => e.Id == id);
        }
    }
}
