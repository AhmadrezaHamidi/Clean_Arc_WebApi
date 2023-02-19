using AhFramWork.Domain.AggregatesModel.FeatureAggregate;
using AhFramWork.Domain.Common;

namespace AhFramWork.Domain.AggregatesModel.CategoryAggregate
{
    public class CategoryFeature : Entity<Guid>
    {
        public CategoryId CategoryId { get; private set; }
        public FeatureId FeatureId { get; private set; }

        internal static CategoryFeature CreateNew(CategoryId categoryId, FeatureId featureId)
        {
            return new CategoryFeature(categoryId, featureId);
        }

        private CategoryFeature(CategoryId categoryId, FeatureId featureId)
        {
            CategoryId = categoryId;
            FeatureId = featureId;
        }

        private CategoryFeature()
        {

        }
    }

}

