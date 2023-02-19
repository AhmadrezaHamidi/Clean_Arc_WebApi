using System;
using AhFramWork.Domain.AggregatesModel.FeatureAggregate.Events;
using AhFramWork.Domain.Common;

namespace AhFramWork.Domain.AggregatesModel.FeatureAggregate
{
    public class Feature : AggregateRoot<FeatureId>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int SortOrder { get; set; }

        public static Feature CreateNew(string title, string description, int sortOrder)
        {
            var featureId = new FeatureId(Guid.NewGuid());
            return new Feature(featureId, title, description, sortOrder);
        }

        public static Feature CreateNewForUpdate(Guid id, string title, string description, int sortOrder)
        {
            var featureId = new FeatureId(id);
            return new Feature(featureId, title, description, sortOrder);
        }

        public static Feature CreateNewForDelete(FeatureId id)
        {
            return new Feature(id);
        }

        public void Update(Feature newValue)
        {
            Title = newValue.Title;
            Description = newValue.Description;
            SortOrder = newValue.SortOrder;
        }

        private Feature(FeatureId id, string title, string description, int sortOrder)
        {
            //validation
            Id = id;
            Title = title;
            Description = description;
            SortOrder = sortOrder;
            //call event ???
            AddDomainEvent(new AddFeatureSendNotificationEvent(Id));
        }

        private Feature(FeatureId featureId)
        {
            Id = featureId;
        }

        private Feature() { }
    }
    public sealed class FeatureId : StronglyTypedId<FeatureId>
    {
        public FeatureId(Guid value) : base(value)
        {

        }
    }
    public interface IFeatureRepository : IRepository<Feature>
    {
        Task<Feature> Add(Feature feature);
        Feature Update(Feature feature);
        Task<Feature> FindByIdAsync(Guid featureId);
    }
}

