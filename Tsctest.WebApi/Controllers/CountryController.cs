using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Tsctest.Core.Models;
using Tsctest.Dal;
using Tsctest.Dal.PagingBase;

namespace Tsctest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Country>> GetCountries(int page = 1, int pageSize = 25)
        {

            var data = _context.Countries
                .Include(x => x.SubDivisions)
                .GetPaged(page, pageSize);

            var metadata = new
            {
                data.PageCount,
                data.PageSize,
                data.CurrentPage,
                data.FirstRowOnPage,
                data.LastRowOnPage,
            };

            Response.Headers.Add("X-Pagination-Details", JsonConvert.SerializeObject(metadata));

            return data.Results.ToList();
        }


        // GET: api/Country/5 
        [HttpGet("{countryId}")]
        public async Task<ActionResult<Country>> GetByCountryId(int countryId)
        {
            var country = await _context.Countries.FindAsync(countryId);
            if (country == null)
            {
                return NotFound();
            }
            return country;
        }



        [HttpPut("{countryId}")]
        public async Task<IActionResult> PutCountry(int countryId, Country country)
        {
            if (countryId != country.CountryId)
            {
                return BadRequest();
            }
            _context.Entry(country).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(countryId))
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

        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { countryId = country.CountryId }, country);
        }

        // DELETE: api/Users/5 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteCountry(int countryId)
        {
            var country = await _context.Countries.FindAsync(countryId);
            if (country == null)
            {
                return NotFound();
            }
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return country;
        }

        private bool CountryExists(int countryId)
        {
            return _context.Countries.Any(e => e.CountryId == countryId);
        }
    }
}
