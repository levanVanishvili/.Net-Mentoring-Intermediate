using craft_beer.Models;
using craft_beer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace craft_beer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        public BeerService _beerService;

        public BeerController(BeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult AddBeer([FromBody]beer beer) 
        {
            _beerService.AddBeer(beer);

            return Ok();
        }

        [HttpGet]
        [Route("beers")]
        public IActionResult GetAllBeer() 
        {
            var allBeer = _beerService.GetAllBeer();
            return Ok(allBeer); 
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBeerById(int id)
        {
            var beer = _beerService.GetBeerById(id);

            return beer is not null
                ? Ok(beer)
                : BadRequest();
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateBeerById(int id, [FromBody]beer beer)
        {
            var updatedBeer = _beerService.UpdateBeerById(id, beer);

            return updatedBeer is not null
                ? Ok(updatedBeer)
                : BadRequest();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteBeerById (int id)
        {
            _beerService.DeleteBeerById(id) ;

            return Ok();
        }

        [HttpGet]
        [Route("find/beerByStyleId")]
        public ActionResult<IEnumerable<beer>> GetBeerByStyleId([FromQuery] beer beer)
        {
            return _beerService.GetAllBeer().Where(b => b.BeerStyleId == beer.BeerStyleId).ToList();
        }

        [HttpGet]
        [Route("filter/beerByPaging")]
        public IActionResult GetAllBeerByPaging([FromQuery] PagingParams pagingParams)
        {
            var filteredBeer = _beerService.GetAllBeerWithPaging(pagingParams);
            return filteredBeer is not null
                ? Ok(filteredBeer)
                : BadRequest();
        }

    }
}
