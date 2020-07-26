using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tsctest.Core.Models;
using Tsctest.Dal;
using Tsctest.Dal.PagingBase;

namespace Tsctest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubDivisionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubDivisionController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get All subDivisions using predefined paging
        /// </summary>
        /// <param name="page">Default value 1</param>
        /// <param name="pageSize">Default value 2</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<SubDivision>> GetSubDivisions(int page = 1, int pageSize = 25)
        {
            return _context.SubDivisions.GetPaged(page, pageSize).Results.ToList();
        }

        /// <summary>
        /// Async method to retrieve the SubDivisions of a Country by CountrId
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [Route("[action]/{countryId}")]
        [HttpGet]
        public async Task<ActionResult<List<SubDivision>>> GetBySubDivisionsByCountryId(int countryId)
        {
            var country = await _context.Countries.FindAsync(countryId);
            if (country == null)
            {
                return NotFound();
            }

            if (country.SubDivisions == null)
            {
                return NoContent();
            }

            return country.SubDivisions.ToList();
        }


        /// <summary>
        /// SubDivision Update method
        /// </summary>
        /// <param name="subDivisionId">PK of the subDivision item to update</param>
        /// <param name="subDivision">changed item to be updated</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutSubDivision(int subDivisionId, SubDivision subDivision)
        {
            if (subDivisionId != subDivision.SubDivisionId)
            {
                return BadRequest();
            }
            _context.Entry(subDivision).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubDivisionExists(subDivisionId))
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
        /// Delete method for SubDivisions
        /// </summary>
        /// <param name="subDivisionId">PK of the subDivision to Delete</param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<ActionResult<SubDivision>> DeleteSubDivision(int subDivisionId)
        {
            var subDivision = await _context.SubDivisions.FindAsync(subDivisionId);
            if (subDivision == null)
            {
                return NotFound();
            }
            _context.SubDivisions.Remove(subDivision);
            await _context.SaveChangesAsync();
            return subDivision;
        }



        /// <summary>
        /// Internal method to check if the SubDivision exists
        /// </summary>
        /// <param name="subDivisionId">PK for the SubDivision</param>
        /// <returns></returns>
        private bool SubDivisionExists(int subDivisionId)
        {
            return _context.SubDivisions.Any(e => e.SubDivisionId == subDivisionId);
        }
    }
}
