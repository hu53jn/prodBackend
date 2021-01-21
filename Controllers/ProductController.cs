using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using productshop.Data;
using productshop.Data.Repo;
using productshop.Dtos;
using productshop.Interfaces;
using productshop.Models;

namespace productshop.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public ProductController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        //GET api/product
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            var products = await uow.ProductRepository.GetProductsAsync();

            var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productsDto);
        }

        //POST api/product/post
        [HttpPost("post")]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            product.LastUpdatedBy = 1;
            product.LastUpdatedOn = DateTime.Now;

            uow.ProductRepository.AddProduct(product);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        //PUT api/product/update/id
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id,ProductDto productDto)
        {
            //if someone manually try to change properties, not from UI
            if (id != productDto.Id)
            {
                return BadRequest("Update not allowed!");
            }

            var prodcutFromDb = await uow.ProductRepository.FindProduct(id);

            if (prodcutFromDb == null)
            {
                return BadRequest("Update not allowed!");
            }

            prodcutFromDb.LastUpdatedBy = 1;
            prodcutFromDb.LastUpdatedOn = DateTime.Now;

            mapper.Map(productDto, prodcutFromDb);

            //throw new Exception("Some unknown error occured");

            await uow.SaveAsync();
            return StatusCode(200);

        }

        //PUT api/product/update/id
        [HttpPut("updateProductName/{id}")] 
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productDto)
        {
            var prodcutFromDb = await uow.ProductRepository.FindProduct(id);
            prodcutFromDb.LastUpdatedBy = 1;
            prodcutFromDb.LastUpdatedOn = DateTime.Now;

            mapper.Map(productDto, prodcutFromDb);

            await uow.SaveAsync();
            return StatusCode(200);
        }

        //probably not going to use at all
        //PATCH api/product/update/id
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateProductPatch(int id, JsonPatchDocument<Product> productToPatch)
        {
            var prodcutFromDb = await uow.ProductRepository.FindProduct(id);
            prodcutFromDb.LastUpdatedBy = 1;
            prodcutFromDb.LastUpdatedOn = DateTime.Now;

            productToPatch.ApplyTo(prodcutFromDb, ModelState);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        //DELETE api/product/delete/id
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            uow.ProductRepository.DeleteProduct(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
