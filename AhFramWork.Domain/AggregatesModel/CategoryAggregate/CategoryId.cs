using AhFramWork.Domain.Common;

namespace AhFramWork.Domain.AggregatesModel.CategoryAggregate
{
    public sealed class CategoryId : StronglyTypedId<CategoryId>
    {
        public CategoryId(Guid value) : base(value)
        {
        }
    }

}

