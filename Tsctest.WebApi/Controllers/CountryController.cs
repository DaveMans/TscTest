using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that retrieves all the countries using paging 
        /// </summary>
        /// <param name="page">1</param>
        /// <param name="pageSize">25</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get country by Primary Key
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method to update the complete country entity
        /// </summary>
        /// <param name="countryId">PK of the country</param>
        /// <param name="country">Country entity</param>
        /// <returns></returns>
        [HttpPut]
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

        /// <summary>
        /// Method to create a new country
        /// </summary>
        /// <param name="country">Country entity</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { countryId = country.CountryId }, country);
        }

        /// <summary>
        /// Deletes the country by id (PK)
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
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
