using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VillaController : ControllerBase
    {
        //private readonly ILogger<VillaController> _logger;

        //public VillaController( ILogger<VillaController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly Ilogging _logger;
        public VillaController(Ilogging logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDTO))]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("getting all villas", "info"); // Added the 'type' argument as required by the Ilogging.Log method
            return Ok(VillaStore.VillaList);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(VillaDTO))]
        public ActionResult GetVillaById(int id)
        {
            if (id == 0)
            {
                _logger.Log("Invalid villa id: " + id, "error"); // Added the 'type' argument as required by the Ilogging.Log method
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

         
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(VillaDTO))]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (VillaStore.VillaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "villa already exists");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDto.Id = VillaStore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.VillaList.Add(villaDto);

            // Fix for CS1501: Use the overload of Created that takes two arguments
            return CreatedAtAction(nameof(GetVillaById), new { id = villaDto.Id }, villaDto);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(VillaDTO))]

        public IActionResult DeleteVilla(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            VillaStore.VillaList.Remove(villa);
            return NoContent();



        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(VillaDTO))]

        public IActionResult UpdateVilla(int id, VillaDTO villaDto)
        {

            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDto.Name;
            villa.Occupancy = villaDto.Occupancy;
            villa.Sqft = villaDto.Sqft;
            return NoContent();


        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(VillaDTO))]

        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return BadRequest(); 
            }
            patchDto.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }



    }
}
