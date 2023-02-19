using System;
using AhFramWork.Application.Conteracts.Common;
using AhFramWork.Application.Conteracts.Dtos;
using AhFramWork.Application.IServices;
using AhFramWork.Domain.AggregatesModel.FeatureAggregate;
using AhFramWork.Infrastructures.Context;
using Microsoft.EntityFrameworkCore;

namespace AhFramWork.Application.Services
{
    public class FeatureServie : IFeatureService
    {
        private readonly IFeatureRepository featureRepository;
        public FeatureServie(IFeatureRepository featureRepository)
        {
            this.featureRepository = featureRepository;
        }

        public async Task<CatalogActionResult<FeatureDto>> Add(FeatureDto model)
        {
            var result = new CatalogActionResult<FeatureDto>();

            var feature = Feature.CreateNew(model.Title, model.Description, model.SortOrder);
            var featureAfterInsert = await featureRepository.Add(feature);
            await featureRepository.UnitOfWork.SaveEntitiesAsync();

            model.Id = featureAfterInsert.Id.Value;

            result.IsSuccess = true;
            result.Data = model;
            return result;
        }

        public async Task<CatalogActionResult<FeatureDto>> Delete(Guid featureId)
        {
            throw new NotImplementedException();
        }

        public async Task<CatalogActionResult<FeatureDto>> GetById(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CatalogActionResult<FeatureDto>> GetList(GridQueryDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<CatalogActionResult<FeatureDto>> Update(FeatureDto feature)
        {
            throw new NotImplementedException();
        }
    }




    public class GenericQueryService<T> : IGenericQueryService<T> where T : class
    {
        private CatalogContext context;
        public GenericQueryService(CatalogContext context)
        {
            this.context = context;
        }

        public async Task<CatalogActionResult<List<T>>> QueryAsync(GridQueryDto args = null,
            IList<string> fields = null, IList<string> includes = null)
        {
            var actionResult = new CatalogActionResult<List<T>>();

            var query = context.Set<T>().AsQueryable();

            //includes
            if (includes != null)
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }

            //filter
            if (args != null && args.Filtered != null && args.Filtered.Length > 0)
            {
                // var filterExpression = QueryUtility.FilterExpression<T>(args.Filtered[0].column, args.Filtered[0].value);
                for (int i = 0; i < args.Filtered.Length; i++)
                {
                    var filterExpression = QueryUtility.FilterExpression<T>(args.Filtered[i].column, args.Filtered[i].value);
                    if (filterExpression != null)
                        query = query.Where(filterExpression);
                }
            }

            //total count
            var total = await query.CountAsync();

            //sort
            if (args != null && args.Sorted != null && args.Sorted.Length > 0)
            {
                for (int i = 0; i < args.Sorted.Length; i++)
                {
                    query = query.SortMeDynamically(args.Sorted[i].column, args.Sorted[i].desc);
                }
            }


            //projection
            if (fields != null && fields.Count > 0)
                query = query.SelectDynamic(fields);

            var result = await query.Skip((args.Page - 1) * args.Size)
                .Take(args.Size)
                .AsNoTracking()
                .ToListAsync();

            actionResult.Data = result;
            actionResult.Total = total;
            actionResult.Page = args.Page;

            return actionResult;
        }
    }
}

