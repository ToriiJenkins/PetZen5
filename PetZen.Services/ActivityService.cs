using PetZen.Data;
using PetZen.Models.ActivityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetZen.Services
{
    public class ActivityService
    {
        private readonly Guid _userId;

        public ActivityService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateActivity(ActivityCreate model)
        {
            var entity =
                new Activity()
                {
                    OwnerId = _userId,
                    ActType = model.ActType,
                    PetId = model.PetId,
                    Date = model.Date,
                    Notes = model.Notes

                };

            using (var ctx = new ApplicationDbContext())
            {
                Activity activity = ctx.Activities.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ActivityListItem> GetActivities()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Activities
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ActivityListItem
                                {
                                    ActivityId = e.ActivityId,
                                    ActType = e.ActType,
                                    PetName = e.Pet.Name,
                                    Date = e.Date,
                                    Notes = e.Notes
                                }
                         );
                return query.ToArray();
            }
        }

        public ActivityDetail GetActivityById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Activities
                        .Single(e => e.ActivityId == id && e.OwnerId == _userId);
                return
                    new ActivityDetail
                    {
                        ActivityId = entity.ActivityId,
                        PetName = entity.Pet.Name,
                        ActType= entity.ActType,
                        Date = entity.Date,
                        Notes = entity.Notes,
                    };
            }
        }


    }
}
