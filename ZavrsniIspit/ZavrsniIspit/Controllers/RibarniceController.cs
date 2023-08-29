using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZavrsniIspit.Interfaces;
using ZavrsniIspit.Repository;

namespace ZavrsniIspit.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RibarniceController : ControllerBase
    {
        private readonly IRibraniceRepository _ribarnicaRepository;

        public RibarniceController(IRibraniceRepository ribarnicaRepository)
        {
            _ribarnicaRepository = ribarnicaRepository;
        }

        [HttpGet]
        public IActionResult GetRibarnice()
        {
            return Ok(_ribarnicaRepository.GetAll().ToList());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public IActionResult GetRibarnica(int id)
        {
            var ribarnica = _ribarnicaRepository.GetById(id);
            if (ribarnica == null)
            {
                return NotFound();
            }

            return Ok(ribarnica);
        }

        [HttpGet]
        [Route("~/api/ribarnice/nadji")]
        public IActionResult GetRibarniceNaziv(string naziv)
        {
            if (string.IsNullOrWhiteSpace(naziv))
            {
                return BadRequest("Naziv ribarnice ne sme biti prazan.");
            }

            return Ok(_ribarnicaRepository.GetRibartniceByNaziv(naziv).ToList());
        }



    }
}
