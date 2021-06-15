using Architecture.Model;
using DotNetCore.Objects;
using DotNetCore.Results;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Architecture.Application
{
    public interface IProductService
    {
        Task<IResult<long>> AddAsync(ProductModel model);

        Task<IResult> DeleteAsync(long id);

        Task<ProductModel> GetAsync(long id);

        Task<Grid<ProductModel>> GridAsync(GridParameters parameters, ClaimsPrincipal user);

        Task<IEnumerable<ProductModel>> ListAsync();

        Task<IResult> UpdateAsync(ProductModel model);
    }
}
