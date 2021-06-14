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
    public sealed class ProductAuditTrailService : IProductAuditTrailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductFactory _productFactory;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public ProductAuditTrailService
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

            //var auth = await _authService.AddAsync(model.User);

            //if (auth.Failed) return auth.Fail<long>();
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

        //public async Task<IResult> InactivateAsync(long id)
        //{
        //    var user = new User(id);

        //    user.Inactivate();

        //    await _userRepository.UpdateStatusAsync(user);

        //    await _unitOfWork.SaveChangesAsync();

        //    return Result.Success();
        //}

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
