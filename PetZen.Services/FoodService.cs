using PetZen.Data;
using PetZen.Models;
using PetZen.Models.FoodModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Services
{
    public class FoodService
    {
        private readonly Guid _userId;

        public FoodService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateFood(FoodCreate model)
        {
            var entity =
                new Food()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Species = model.Species,
                    ServingSize = model.ServingSize,
                    PurchaseLink = model.PurchaseLink
                };

            using (var ctx = new ApplicationDbContext())
            {
                Food food = ctx.Foods.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FoodListItem> GetFoods()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Foods
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new FoodListItem
                                {
                                    FoodId = e.FoodId,
                                    Name = e.Name,
                                    Species = e.Species,
                                    ServingSize = e.ServingSize,
                                    PurchaseLink = e.PurchaseLink
                                }
                         );
                return query.ToArray();
            }
        }
    }
}
