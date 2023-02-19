using System;
using AhFramWork.Application.Conteracts.Common;
using AhFramWork.Application.Conteracts.Dtos;

namespace AhFramWork.Application.IServices
{
    public interface IFeatureService
    {
        Task<CatalogActionResult<FeatureDto>> GetById(Guid id, Guid userId);
        Task<CatalogActionResult<FeatureDto>> GetList(GridQueryDto model = null);
        Task<CatalogActionResult<FeatureDto>> Add(FeatureDto feature);
        Task<CatalogActionResult<FeatureDto>> Update(FeatureDto feature);
        Task<CatalogActionResult<FeatureDto>> Delete(Guid featureId);

    }
    public interface IGenericQueryService<T> where T : class
    {
        Task<CatalogActionResult<List<T>>> QueryAsync(GridQueryDto args, IList<string> fields = null, IList<string> includes = null);
    }
}

