﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;

namespace Backend.Controllers
{
    public class BodyFoodRates
    {
        public long id { get; set; }
        public long rate { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class FoodRatesController : ControllerBase
    {
        private readonly DragorantContext _context;

        public FoodRatesController(DragorantContext context)
        {
            _context = context;
        }

        // GET: api/FoodRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodRate>>> GetFoodRates()
        {
            return await _context.FoodRates.ToListAsync();
        }

        // GET: api/FoodRates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodRate>> GetFoodRate(long id)
        {
            var foodRate = await _context.FoodRates.FindAsync(id);

            if (foodRate == null)
            {
                return NotFound();
            }

            return foodRate;
        }

        // PUT: api/FoodRates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodRate(long id, FoodRate foodRate)
        {
            if (id != foodRate.Id)
            {
                return BadRequest();
            }

            _context.Entry(foodRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodRateExists(id))
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

        // POST: api/FoodRates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("RateFood")]
        public async Task<ActionResult<FoodRate>> PostFoodRate(BodyFoodRates bodyFoodRate)
        {
            FoodRate foodRate = new FoodRate();
            foodRate.Date = DateTime.Now;
            foodRate.FoodId = bodyFoodRate.id;
            foodRate.Value = bodyFoodRate.rate;
            foodRate.UserId = 1; ////////////////////////////////////////////////////////////////////////////////////        TUTAJ TOKEN BARTEK
            _context.FoodRates.Add(foodRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodRate", new { id = foodRate.Id }, foodRate);
        }

        // DELETE: api/FoodRates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FoodRate>> DeleteFoodRate(long id)
        {
            var foodRate = await _context.FoodRates.FindAsync(id);
            if (foodRate == null)
            {
                return NotFound();
            }

            _context.FoodRates.Remove(foodRate);
            await _context.SaveChangesAsync();

            return foodRate;
        }

        private bool FoodRateExists(long id)
        {
            return _context.FoodRates.Any(e => e.Id == id);
        }
    }
}
