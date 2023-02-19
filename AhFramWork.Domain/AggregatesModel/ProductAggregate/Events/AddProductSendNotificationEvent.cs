using System;
using AhFramWork.Domain.Common;

namespace AhFramWork.Domain.AggregatesModel.ProductAggregate.Events
{
    public record class AddProductSendNotificationEvent : DomainEvent
    {
        public ProductId ProductId { get; init; }

        public AddProductSendNotificationEvent(ProductId productId)
        {
            ProductId = productId;
            AggregateId = productId.Value;
        }
    }
}

