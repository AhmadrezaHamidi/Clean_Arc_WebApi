using System;
using AhFramWork.Domain.Common;

namespace AhFramWork.Domain.AggregatesModel.FeatureAggregate.Events
{
    public record class AddFeatureSendNotificationEvent : DomainEvent
    {
        public FeatureId FeatureId { get; init; }

        public AddFeatureSendNotificationEvent(FeatureId featureId)
        {
            FeatureId = featureId;
            AggregateId = featureId.Value;
        }
    }
}

