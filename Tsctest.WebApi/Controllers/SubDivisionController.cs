using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tsctest.Core.Models;
using Tsctest.Dal;

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

        public async Task<ActionResult<IEnumerable<SubDivision>>> GetSubDivisions()
        {
            return await _context.SubDivisions.ToListAsync();
        }

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
    }
}
