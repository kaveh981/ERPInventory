using ERPInventory.BusinessLayer;
using ERPInventory.Model.BindingModels;
using ERPInventory.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPInventory.Controllers
{
    [RoutePrefix("api/category")]
    public class CategoryController : BaseApiController
    {

        private readonly ICategory _Category;

        public CategoryController(ICategory category)
        {
            _Category = category;
        }

        [HttpGet]
        public HttpResponseMessage GetCategories()
        {
            try
            {
                int c = _Category.GetChildsByCategoryId(Guid.Parse("e87f784e-b93a-e511-ae8c-002433726434")).ToList().Count();
                return Request.CreateResponse(HttpStatusCode.OK, _Category.GetChildsByCategoryId(Guid.Parse("e87f784e-b93a-e511-ae8c-002433726434")).ToList());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }
        }

        [HttpGet]
        public HttpResponseMessage GetChildsByCategoryId(Guid? id)
        {
            //Guid? ids = id == null ? null : (Guid?)Guid.Parse(id);
            try
            {
                int c = _Category.GetChildsByCategoryId(id).ToList().Count();
                return Request.CreateResponse(HttpStatusCode.OK, _Category.GetChildsByCategoryId(id).ToList());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }
        }

        [Route("PostCategory")]
        [Authorize]
        public HttpResponseMessage PostCategory([FromBody]inv_Category category)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _Category.PostCategory(category);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, category);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [Route("PutCategory")]
        [Authorize]
        [HttpPut]
        public HttpResponseMessage PutCategory([FromBody]inv_Category category)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _Category.UpdateCategory(category);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, category);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        
        [Route("MoveAndSortCategory")]
        [HttpPut]
        [Authorize]
        public HttpResponseMessage MoveAndSortCategory(CategoryDataSorting data)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _Category.MoveAndSortCategory(data);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, data);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [HttpDelete]
        [Authorize]
        public HttpResponseMessage Delete(Guid id)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _Category.DeleteCategory(id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, id);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _Category.Dispose();
            base.Dispose(disposing);
        }
    }
}