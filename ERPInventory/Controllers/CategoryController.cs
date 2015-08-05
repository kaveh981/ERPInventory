﻿using ERPInventory.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPInventory.Controllers
{
    public class CategoryController : ApiController
    {
        // GET api/<controller>

        private readonly ICategory _Category;


          
        public CategoryController(ICategory category)
        {
            _Category = category;
        }

   

        // GET api/<controller>/5
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



        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}