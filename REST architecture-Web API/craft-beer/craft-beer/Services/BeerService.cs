using craft_beer.Data;
using craft_beer.Models;

namespace craft_beer.Services
{
    public class BeerService
    {
        private BeerDbContext _dbContext;
        public BeerService(BeerDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public void AddBeer(beer beer)
        {
            var _beer = new beer()
            {
                BeerTitle = beer.BeerTitle,
                BeerStyleId = beer.BeerStyleId

            };
            _dbContext.Beers.Add(_beer);
            _dbContext.SaveChanges();
        }

        public List<beer> GetAllBeer() => _dbContext.Beers.ToList();

        public List<beer> GetAllBeerWithPaging(PagingParams pageingParams)
        {
            return _dbContext.Beers
                .Skip((pageingParams.PageNumber -1 )* pageingParams.PageSize)
                .Take(pageingParams.PageSize)
                .ToList();

        }

        public beer? GetBeerById(int beerId) => _dbContext?.Beers.FirstOrDefault(x => x.Id == beerId);

        public beer UpdateBeerById(int Id, beer beer)
        {
            var _beer = _dbContext.Beers.FirstOrDefault(x =>x.Id == Id);

            if (_beer != null)
            {
                _beer.BeerTitle = beer.BeerTitle;
                _beer.BeerStyleId = beer.BeerStyleId;

                _dbContext.SaveChanges();
            }

            return _beer!;
        }

        public void DeleteBeerById(int id)
        {
            var _beer = _dbContext.Beers.FirstOrDefault(x => x.Id == id);
            if (_beer != null)
            {
                _dbContext.Beers.Remove(_beer);
                _dbContext.SaveChanges();
            }
        }
    }
}
