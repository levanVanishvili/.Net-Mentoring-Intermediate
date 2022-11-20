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
        [Route("add-beer")]
        public IActionResult AddBeer([FromBody]beer beer) 
        {
            _beerService.AddBeer(beer);

            return Ok();
        }

        [HttpGet]
        [Route("get-all-beer")]
        public IActionResult GetAllBeer() 
        {
            var allBeer = _beerService.GetAllBeer();
            return Ok(allBeer); 
        }

        [HttpGet]
        [Route("get-beer/{id}")]
        public IActionResult GetBeerById(int id)
        {
            var beer = _beerService.GetBeerById(id);

            return Ok(beer);
        }

        [HttpPut]
        [Route("update-beer/{id}")]
        public IActionResult UpdateBeerById(int id, [FromBody]beer beer)
        {
            var updatedBeer = _beerService.UpdateBeerById(id, beer);

            return Ok(updatedBeer);
        }

        [HttpDelete]
        [Route("delete-beer/{id}")]
        public IActionResult DeleteBeerById (int id)
        {
            _beerService.DeleteBeerById(id) ;

            return Ok();
        }

        [HttpGet]
        [Route("get-beerByStyleId")]
        public ActionResult<IEnumerable<beer>> GetBeerByStyleId([FromQuery] beer beer)
        {
            return _beerService.GetAllBeer().Where(b => b.BeerStyleId == beer.BeerStyleId).ToList();
        }

        [HttpGet]
        [Route("get-beerByPaging")]
        public IActionResult GetAllBeerByPaging([FromQuery] PagingParams pagingParams)
        {
            var filteredBeer = _beerService.GetAllBeerWithPaging(pagingParams);
            return Ok(filteredBeer);
        }

    }
}
