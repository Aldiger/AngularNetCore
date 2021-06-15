using System;
using Architecture.Database;
using Architecture.Domain;
using Architecture.Model;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using DotNetCore.Results;
using DotNetCore.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Architecture.Application
{
    public sealed class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductFactory _productFactory;
        private readonly IProductAuditTrailFactory _productAuditTrailFactory;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductAuditTrailRepository _productAuditTrailRepository;

        public ProductService
        (
            IUnitOfWork unitOfWork,
            IProductFactory productFactory,
            IProductAuditTrailFactory productAuditTrailFactory,
            IUserRepository userRepository,
            IProductRepository productRepository,
            IProductAuditTrailRepository productAuditTrailRepository
        )
        {
            _unitOfWork = unitOfWork;
            _productFactory = productFactory;
            _productAuditTrailFactory = productAuditTrailFactory;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _productAuditTrailRepository = productAuditTrailRepository;

        }

        public async Task<IResult<long>> AddAsync(ProductModel model)
        {
            var validation = new AddProductModelValidator().Validation(model);

            if (validation.Failed) return validation.Fail<long>();

            var user = await _userRepository.GetAsync(model.UserId);
            
            var product = _productFactory.Create(model, user);
            var productAuditTrail = _productAuditTrailFactory.Create(product, AuditRow.Before, AuditAction.Insert, DateTime.UtcNow);

            product.ProductAuditTrails= new List<ProductAuditTrail>{productAuditTrail};
            
            await _productRepository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return product.Id.Success();
        }

        public async Task<IResult> DeleteAsync(long id)
        {
            await _productRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public Task<ProductModel> GetAsync(long id)
        {
            return _productRepository.GetModelAsync(id);
        }

        public async Task<Grid<ProductModel>> GridAsync(GridParameters parameters, ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor)?.Value;
            var role = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            long? userIdlong = null;
            if (role == Roles.Worker.ToString())
            {
                long.TryParse(userId, out long userIdlongparse);
                userIdlong = userIdlongparse;
            }
            
            var results=await _productRepository.GridAsync(parameters, userIdlong);
            return results;
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

            var date = DateTime.UtcNow;

            if (product is null) return Result.Success();

            var productAuditTrailBefore = _productAuditTrailFactory.Create(product, AuditRow.Before, AuditAction.Update, date);

            product.Update(model.Name, model.Description, model.Price);

            var productAuditTrailAfter = _productAuditTrailFactory.Create(product, AuditRow.After, AuditAction.Update, date);

            product.ProductAuditTrails = new List<ProductAuditTrail> {productAuditTrailBefore, productAuditTrailAfter};

            await _productRepository.UpdateAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }


    }
}
