using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Security;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo, IMapper mapper, IGenericRepository<ProductType> TypeRepo, IGenericRepository<ProductBrand> BrandRepo)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
            _typeRepo = TypeRepo;
            _brandRepo = BrandRepo;
        }

        //Get All Products
        [HttpGet]

        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams Params)

        {
            var Spec = new ProductWithTypeAndBrandSpecifications(Params);
            var Products = await _productRepo.GetAllWIthSpecAsync(Spec);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);

            var CountSpec = new ProductWithFilterationForCountAsync(Params);

            var Count = await _productRepo.GetCountForAllProductAsync(CountSpec);


            //var ReturnedPaginationData = new Pagination<ProductToReturnDto>()
            //{
            //    PageSize = Params.PageSize,   a
            //    PageIndex = Params.PageIndex,
            //    Data = MappedProducts
            //};
            //return Ok(MappedProducts);
            return Ok(new Pagination<ProductToReturnDto>(Params.PageSize, Params.PageIndex, MappedProducts, Count));
        }

        //Get Product By Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)] //= 404

        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var Spec = new ProductWithTypeAndBrandSpecifications(id);

            var Product = await _productRepo.GetByIdWithSpecAsync(Spec);
            if (Product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(Product);

            return Ok(MappedProduct);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await _typeRepo.GetAllAsync();
            return Ok(Types);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Types = await _brandRepo.GetAllAsync();
            return Ok(Types);
        }

    }
}
