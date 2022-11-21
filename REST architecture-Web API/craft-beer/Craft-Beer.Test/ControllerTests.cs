using craft_beer.Controllers;
using craft_beer.Data;
using craft_beer.Models;
using craft_beer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Craft_Beer.Test
{
    public class Tests
    {
        private static DbContextOptions<BeerDbContext> dbContextOptions = new DbContextOptionsBuilder<BeerDbContext>()
            .UseInMemoryDatabase(databaseName: "beerDBTest")
            .Options;

        BeerDbContext dbContext;
        BeerService beerService;
        BeerStyleService beerStyleService;

        [OneTimeSetUp]
        public void Setup()
        {
            dbContext = new BeerDbContext(dbContextOptions);
            dbContext.Database.EnsureCreated();

            SeedDatabase();
            beerService = new BeerService(dbContext);
            beerStyleService = new BeerStyleService(dbContext);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            dbContext.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var beerStyles = new List<BeerStyle>()
            {
                new BeerStyle
                {
                    Id = 1,
                    Style = "Pilsner"
                },
                new BeerStyle
                {
                    Id = 2,
                    Style = "Lager"
                },
                new BeerStyle
                {
                    Id = 3,
                    Style = "APA"
                },
                new BeerStyle
                {
                    Id = 4,
                    Style = "IPA"
                }
            };
            dbContext.BeerStyles.AddRange(beerStyles);

            var _beer = new List<beer>()
            {
                new beer
                {
                    Id = 1,
                    BeerTitle = "Pilsner Urquell",
                    BeerStyleId= 1,
                },
                new beer
                {
                    Id = 2,
                    BeerTitle = "Hacker Pschorr Munich Gold",
                    BeerStyleId= 2,
                },
                new beer
                {
                    Id = 3,
                    BeerTitle = "Budweiser",
                    BeerStyleId= 1,
                },
                new beer
                {
                    Id = 4,
                    BeerTitle = "Kozel",
                    BeerStyleId= 2,
                },
                new beer
                {
                    Id = 5,
                    BeerTitle = "Paulaner Original",
                    BeerStyleId= 2,
                },
                new beer
                {
                    Id = 6,
                    BeerTitle = "Spaten Premium",
                    BeerStyleId= 1,
                }
            };
            dbContext.Beers.AddRange(_beer);

            dbContext.SaveChanges();
        }

        [Test]
        public void Get_All_beer()
        {
            var beerController = new BeerController(beerService);

            var actionResult = beerController.GetAllBeer();

            OkObjectResult? okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            Assert.That(okResult.Value, Is.EqualTo(dbContext.Beers.ToList()));
        }


        [Test]
        [TestCase(2)]
        public void Get_beer_byID(int id)
        {
            var beerController = new BeerController(beerService);
            
            var actionResult = beerController.GetBeerById(id);

            OkObjectResult? okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var beer = okResult.Value as beer;

            Assert.That(beer?.BeerTitle, Is.EqualTo("Hacker Pschorr Munich Gold"));
        }

        [Test]
        [TestCase(4, "Spaten Premium 2", 2)]
        public void Add_beer(int id, string beerTitle, int Styleid)
        {
            var beerController = new BeerController(beerService);

            var beer = new beer
            {
                Id = id,
                BeerTitle = beerTitle,
                BeerStyleId = Styleid,
            };
            var actionResult = beerController.AddBeer(beer);

            OkResult? okResult = actionResult as OkResult;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        [TestCase(5, "Paulaner Original updated", 1)]
        public void update_beer_by_Id(int id, string beerTitle, int Styleid)
        {
            var beerController = new BeerController(beerService);

            var beer = new beer
            {
                Id = id,
                BeerTitle = beerTitle,
                BeerStyleId = Styleid,
            };

            var actionResult = beerController.UpdateBeerById(id, beer);

            OkObjectResult? okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var beerUpdated = okResult.Value as beer;

            Assert.That(beerUpdated?.BeerTitle, Is.EqualTo("Paulaner Original updated"));
        }

        [Test]
        [TestCase(6)]
        public void Delete_Beer_byID(int id)
        {
            var beerController = new BeerController(beerService);

            var actionResult = beerController.DeleteBeerById(id);

            OkResult? okResult = actionResult as OkResult;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        [TestCase(2, "Spaten Premium updated", 1)]
        public void Get_Beer_byBeerStyleID(int id, string beerTitle, int Styleid)
        {
            var beerController = new BeerController(beerService);

            var beer = new beer
            {
                Id = id,
                BeerTitle = beerTitle,
                BeerStyleId = Styleid,
            };

            var result = beerController.GetBeerByStyleId(beer);

            Assert.That(result?.Value?.Count(), Is.EqualTo(2));
        }

        [Test]
        [TestCase(3,2)]
        public void Get_beer_ByPaging(int page, int pageSize)
        {
            var beerController = new BeerController(beerService);

            var pageParm = new PagingParams
            {
                PageSize = pageSize,
                PageNumber = page
            };

            var actionResult = beerController.GetAllBeerByPaging(pageParm);

            OkObjectResult? okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var result = okResult.Value as List<beer>;

            Assert.That(result?.Count(), Is.EqualTo(2));
        }

        //BeerStylesController-Tests

        [Test]
        public void Get_All_BeerStyle()
        {
            var beerStylesController = new BeerStylesController(beerStyleService);

            var actionResult = beerStylesController.GetAllBeerStyles();

            OkObjectResult? okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            Assert.That(okResult.Value, Is.EqualTo(dbContext.BeerStyles.ToList()));
        }

        [Test]
        [TestCase(5, "Stout")]
        public void Add_beerStyle(int id, string style)
        {
            var beerStylesController = new BeerStylesController(beerStyleService);

            var beerStyle = new BeerStyle
            {
                Id = id,
                Style = style
            };
            var actionResult = beerStylesController.AddBeerStyle(beerStyle);

            OkResult? okResult = actionResult as OkResult;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        [TestCase(4)]
        public void Get_beerStyle_byID(int id)
        {
            var beerStylesController = new BeerStylesController(beerStyleService);

            var actionResult = beerStylesController.GetBeerStyleById(id);

            OkObjectResult? okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var beerStyle = okResult.Value as BeerStyle;

            Assert.That(beerStyle?.Style, Is.EqualTo("IPA"));
        }

        [Test]
        [TestCase(1, "APA")]
        public void update_beerStyle_byId(int id, string style)
        {
            var beerStylesController = new BeerStylesController(beerStyleService);

            var beerStyle = new BeerStyle
            {
                Id = id,
                Style = style
            };

            var actionResult = beerStylesController.UpdateBeerStyleById(id, beerStyle);

            OkObjectResult? okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var beerStyleUpdated = okResult.Value as BeerStyle;

            Assert.That(beerStyleUpdated?.Id, Is.EqualTo(1));

        }

        [Test]
        [TestCase(6)]
        public void Delete_BeerStyle_byID(int id)
        {
            var beerStylesController = new BeerStylesController(beerStyleService);

            var actionResult = beerStylesController.DeleteBeerStyleById(id);

            OkResult? okResult = actionResult as OkResult;

            Assert.That(okResult?.StatusCode, Is.EqualTo(200));
        }
    }
}