using System.Linq;
using System.Security.Claims;
using Architecture.Application;
using Architecture.Domain;
using Architecture.Model;
using DotNetCore.AspNetCore;
using DotNetCore.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Web
{
    [ApiController]
    [Route("products")]
    public sealed class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductAuditTrailService _productAuditTrailService;

        public ProductController(IProductService productService,
            IProductAuditTrailService productAuditTrailService
            )
        {
            _productService = productService;
            _productAuditTrailService = productAuditTrailService;
        }

        [HttpPost]
        [Authorize(Roles = "Worker")]
        public IActionResult Add(ProductModel model) => _productService.AddAsync(model).ApiResult(); 

        [HttpDelete("{id}")]
        [Authorize(Roles = "Worker")]
        public IActionResult Delete(long id) => _productService.DeleteAsync(id).ApiResult();

        [HttpGet("{id}")]
        public IActionResult Get(long id) => _productService.GetAsync(id).ApiResult();

        [HttpGet("audit/{id}")]
        public IActionResult GetAudit(long id) => _productAuditTrailService.GetAsync(id).ApiResult();

        [HttpGet("grid")]
        [Authorize(Roles = "Worker, Admin")]
        public IActionResult Grid([FromQuery] GridParameters parameters)=>_productService.GridAsync(parameters, User).ApiResult();
        

        [HttpGet]
        [Authorize(Roles = "Worker, Admin")]
        public IActionResult List() => _productService.ListAsync().ApiResult();

        [HttpPut("{id}")]
        [Authorize(Roles = "Worker")]
        public IActionResult Update(ProductModel model) => _productService.UpdateAsync(model).ApiResult();
    }
}
