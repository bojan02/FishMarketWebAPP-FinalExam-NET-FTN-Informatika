using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZavrsniIspit.Interfaces;
using ZavrsniIspit.Models;

namespace ZavrsniIspit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RibeController : ControllerBase
    {
        private readonly IRibaRepository _ribaRepository;
        private readonly IMapper _mapper;

        public RibeController(IRibaRepository ribaRepository, IMapper mapper)
        {
            _ribaRepository = ribaRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("~/api/info")]
        public IActionResult GetRibarniceGranica(double granica)
        {
            if (granica < 0)
            {
                return BadRequest("Granica ne može biti negativna.");
            }

            return Ok(_ribaRepository.GetInfoRibarnice(granica).ToList());
        }

          [HttpGet]
          [Route("~/api/stanje")]
          public IActionResult GetRibarniceStanje()
          {
              return Ok(_ribaRepository.GetStanjeRibarnice().ToList());
          }


        [HttpGet]
        public IActionResult GetRibe()
        {
            return Ok(_ribaRepository.GetAll().ProjectTo<RibaDetailDTO>(_mapper.ConfigurationProvider).ToList());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public IActionResult GetRiba(int id)
        {
            var riba = _ribaRepository.GetById(id);
            if (riba == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RibaDetailDTO>(riba));
        }

        [HttpGet]
        [Route("~/api/ribe/trazi")]
        public IActionResult GetRibarniceNaziv(string sorta)
        {
            if (string.IsNullOrWhiteSpace(sorta))
            {
                return BadRequest("Sorta ribe ne sme biti prazna.");
            }

            return Ok(_ribaRepository.GetRibeBySorta(sorta).ProjectTo<RibaDetailDTO>(_mapper.ConfigurationProvider).ToList());
        }

        [HttpPost]
        public IActionResult PostRiba(Riba riba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _ribaRepository.Add(riba);
            return CreatedAtAction("GetRiba", new { id = riba.Id }, riba);
        }

        [HttpPut("{id}")]
        public IActionResult PutRiba(int id, Riba riba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != riba.Id)
            {
                return BadRequest();
            }

            try
            {
                _ribaRepository.Update(riba);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(riba);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteRiba(int id)
        {
            var riba = _ribaRepository.GetById(id);
            if (riba == null)
            {
                return NotFound();
            }

            _ribaRepository.Delete(riba);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("~/api/ribe/pretraga")]
        public IActionResult Search(PretragaDTO dto)
        {
            if (dto.Najmanje < 1 || dto.Najvise < 1 || dto.Najvise > 1000 || dto.Najmanje > dto.Najvise)
            {
                return BadRequest();
            }
            return Ok(_ribaRepository.GetAllByParameters(dto.Najmanje, dto.Najvise).ProjectTo<RibaDetailDTO>(_mapper.ConfigurationProvider).ToList());
        }
    }
}
