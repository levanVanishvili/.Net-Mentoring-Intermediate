using craft_beer.Data;
using craft_beer.Models;

namespace craft_beer.Services
{
    public class BeerStyleService
    {
        private BeerDbContext _dbContext;
        public BeerStyleService(BeerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBeerStyle(BeerStyle style)
        {
            var _style = new BeerStyle()
            {
                Style = style.Style

            };
            _dbContext.BeerStyles.Add(_style);
            _dbContext.SaveChanges();
        }

        public List<BeerStyle> GetAllBeerStyles() => _dbContext.BeerStyles.ToList();

        public BeerStyle GetBeerStyleById(int StyleId) => _dbContext.BeerStyles.FirstOrDefault(x => x.Id == StyleId);

        public BeerStyle UpdateBeerStyleById(int Id, BeerStyle style)
        {
            var _style = _dbContext.BeerStyles.FirstOrDefault(x => x.Id == Id);

            if (_style != null)
            {
                _style.Style = style.Style;

                _dbContext.SaveChanges();
            }

            return _style;
        }

        public void DeleteBeerStyleById(int id)
        {
            var _style = _dbContext.BeerStyles.FirstOrDefault(x => x.Id == id);
            if (_style != null)
            {
                _dbContext.BeerStyles.Remove(_style);
                _dbContext.SaveChanges();
            }
        }
    }
}
