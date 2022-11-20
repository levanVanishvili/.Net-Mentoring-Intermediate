using craft_beer.Models;
using craft_beer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace craft_beer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerStylesController : ControllerBase
    {
        private BeerStyleService _beerStyleService;
        public BeerStylesController(BeerStyleService beerStyleService)
        {
            _beerStyleService= beerStyleService;
        }

        [HttpPost]
        [Route("add-BeerStyle")]
        public IActionResult AddBeerStyle([FromBody] BeerStyle style)
        {
            _beerStyleService.AddBeerStyle(style);

            return Ok();
        }

        [HttpGet]
        [Route("get-all-BeerStyles")]
        public IActionResult GetAllBeerStyles()
        {
            var allBeerStyles = _beerStyleService.GetAllBeerStyles();
            return Ok(allBeerStyles);
        }

        [HttpGet]
        [Route("get-BeerStyle/{id}")]
        public IActionResult GetBeerStyleById(int id)
        {
            var beer = _beerStyleService.GetBeerStyleById(id);

            return Ok(beer);
        }

        [HttpPut]
        [Route("update-BeerStyle/{id}")]
        public IActionResult UpdateBeerStyleById(int id, [FromBody] BeerStyle style)
        {
            var updatedBeerStyles = _beerStyleService.UpdateBeerStyleById(id, style);

            return Ok(updatedBeerStyles);
        }

        [HttpDelete]
        [Route("delete-BeerStyle/{id}")]
        public IActionResult DeleteBeerStyleById(int id)
        {
            _beerStyleService.DeleteBeerStyleById(id);

            return Ok();
        }
    }
}
