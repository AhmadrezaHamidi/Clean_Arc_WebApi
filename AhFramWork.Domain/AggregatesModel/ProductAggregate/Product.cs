using System;
using AhFramWork.Domain.AggregatesModel.CategoryAggregate;
using AhFramWork.Domain.AggregatesModel.FeatureAggregate;
using AhFramWork.Domain.AggregatesModel.ProductAggregate.Events;
using AhFramWork.Domain.Common;

namespace AhFramWork.Domain.AggregatesModel.ProductAggregate
{
    public class Product : AggregateRoot<ProductId>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Code { get; private set; }
        public double Price { get; private set; }
        public CategoryId CategoryId { get; private set; }

        private readonly List<ProductFeatureValue> _productFeatureValues = new List<ProductFeatureValue>();
        public IReadOnlyList<ProductFeatureValue> ProductFeatureValues => _productFeatureValues;


        private readonly List<ProductAttachment> _productAttachments = new List<ProductAttachment>();
        public IReadOnlyList<ProductAttachment> ProductAttachments => _productAttachments;

        internal static Product CreateNew(CategoryId categoryI, string title, string description, string code, double price, List<ProductFeatureValue> productFeatures)
        {
            return new Product(categoryI, title, description, code, price, productFeatures);
        }

        private void BuildFeatures(List<ProductFeatureValue> featureData)
        {
            featureData.ForEach(feature =>
            {
                var newFeature = ProductFeatureValue.CreateNew(Id, feature.FeatureId, feature.Value);
                _productFeatureValues.Add(newFeature);
            });
        }

        private Product(CategoryId categoryId, string title, string description, string code, double price, List<ProductFeatureValue> productFeatures)
        {
            if (price < 0) throw new BusinessRuleException("invalid price value");
            CategoryId = categoryId;
            Title = title;
            Code = code;
            Description = description;
            Price = price;
            BuildFeatures(productFeatures);
            AddDomainEvent(new AddProductSendNotificationEvent(Id));
        }

        private Product() { }
    }
    public class ProductAttachment : Entity<Guid>
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        public ProductAttachmentFileType FileType { get; set; }
    }

    public enum ProductAttachmentFileType
    {
        Thumbnail = 10,
        Gallery = 20,
        Video = 30,
        Catalog = 40,
    }
    public class ProductFeatureValue : Entity<Guid>
    {
        public ProductId ProductId { get; private set; }
        public FeatureId FeatureId { get; private set; }
        public string Value { get; private set; }

        internal static ProductFeatureValue CreateNew(ProductId productId, FeatureId featureId, string value)
        {
            return new ProductFeatureValue(productId, featureId, value);
        }

        private ProductFeatureValue(ProductId productId, FeatureId featureId, string value)
        {
            ProductId = productId;
            FeatureId = featureId;
            Value = value;
        }

        private ProductFeatureValue()
        {

        }
    }
    public sealed class ProductId : StronglyTypedId<ProductId>
    {
        public ProductId(Guid value) : base(value)
        {

        }
    }
}

