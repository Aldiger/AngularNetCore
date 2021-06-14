using Architecture.Application;
using Architecture.Model;
using DotNetCore.AspNetCore;
using DotNetCore.Objects;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Web
{
    [ApiController]
    [Route("products")]
    public sealed class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        [HttpPost]
        public IActionResult Add(ProductModel model) => _productService.AddAsync(model).ApiResult();

        //[HttpPost]
        //public IActionResult Add(ProductModel model)
        //{
        //    var user = User;
        //    return _productService.AddAsync(model).ApiResult();
        //} 

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) => _productService.DeleteAsync(id).ApiResult();

        [HttpGet("{id}")]
        public IActionResult Get(long id) => _productService.GetAsync(id).ApiResult();

        [HttpGet("grid")]
        public IActionResult Grid([FromQuery] GridParameters parameters) => _productService.GridAsync(parameters).ApiResult();

        //[HttpPatch("{id}/inactivate")]
        //public IActionResult Inactivate(long id) => _productService.InactivateAsync(id).ApiResult();

        [HttpGet]
        public IActionResult List() => _productService.ListAsync().ApiResult();

        [HttpPut("{id}")]
        public IActionResult Update(ProductModel model) => _productService.UpdateAsync(model).ApiResult();
    }
}
