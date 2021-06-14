using Architecture.Database;
using Architecture.Domain;
using Architecture.Model;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using DotNetCore.Results;
using DotNetCore.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Architecture.Application
{
    public sealed class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductFactory _productFactory;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public ProductService
        (
            IUnitOfWork unitOfWork,
            IProductFactory productFactory,
            IUserRepository userRepository,
            IProductRepository productRepository
        )
        {
            _unitOfWork = unitOfWork;
            _productFactory = productFactory;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<IResult<long>> AddAsync(ProductModel model)
        {
            var validation = new AddProductModelValidator().Validation(model);

            if (validation.Failed) return validation.Fail<long>();

            var user = await _userRepository.GetAsync(model.UserId);
            
            var product = _productFactory.Create(model, user);

            await _productRepository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return product.Id.Success();
        }

        public async Task<IResult> DeleteAsync(long id)
        {
            //var authId = await _userRepository.GetAuthIdByUserIdAsync(id);

            await _productRepository.DeleteAsync(id);

            //await _authService.DeleteAsync(authId);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public Task<ProductModel> GetAsync(long id)
        {
            return _productRepository.GetModelAsync(id);
        }

        public Task<Grid<ProductModel>> GridAsync(GridParameters parameters)
        {
            return _productRepository.GridAsync(parameters);
        }


        public async Task<IEnumerable<ProductModel>> ListAsync()
        {
            return await _productRepository.ListModelAsync();
        }

        public async Task<IResult> UpdateAsync(ProductModel model)
        {
            var validation = new UpdateProductModelValidator().Validation(model);

            if (validation.Failed) return validation;

            var product = await _productRepository.GetAsync(model.Id);

            if (product is null) return Result.Success();

            product.Update(model.Name, model.Description, model.Price);

            await _productRepository.UpdateAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
