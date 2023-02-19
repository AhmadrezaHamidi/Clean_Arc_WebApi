using System;
using AhFramWork.Domain.AggregatesModel.FeatureAggregate;
using AhFramWork.Domain.Common;
using AhFramWork.Infrastructures.Context;
using Microsoft.EntityFrameworkCore;

namespace AhFramWork.Persistances.Repositories
{
    public class FeatureRepositoy : IFeatureRepository
    {
        private readonly CatalogContext context;
        public IUnitOfWork UnitOfWork => context;

        public FeatureRepositoy(CatalogContext catalogContext)
        {
            this.context = catalogContext;
        }

        public async Task<Feature> Add(Feature feature)
        {
            await context.Features.AddAsync(feature);
            return feature;
        }

        public async Task<Feature> FindByIdAsync(Guid featureId)
        {
            //for test 
            var query = context.Features.Where(q => q.SortOrder > 10 && q.SortOrder < 20)
                .OrderBy(q => q.Title);

            //get sql query
            var sqlQuery = query.ToQueryString();



            var feature = await context.Features.FindAsync(featureId);
            return feature;
        }

        public Feature Update(Feature feature)
        {
            return context.Features
                .Update(feature)
                .Entity;
        }
    }
}

