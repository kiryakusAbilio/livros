using BookStore.Domain;
using BookStore.Infra.DataContexts;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.Api.Controllers
{
    [RoutePrefix("api/v1/public")]
    public class CategoryController : ApiController
    {
        private BookStoreDataContext db = new BookStoreDataContext();

        [HttpGet]
        [Route("categories")]
        public HttpResponseMessage GetCategories()
        {
            var result = db.Categories.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public HttpResponseMessage GetCategory(int categoryId)
        {
            Category category = db.Categories.Find(categoryId);
            if (category == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        [HttpPut]
        [Route("category/{categoryId}/{category}")]
        public HttpResponseMessage PutCategory(int categoryId, Category category)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (categoryId != category.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(categoryId))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Category saved.");
        }

        [HttpPost]
        [Route("categories")]
        public HttpResponseMessage PostCategory(Category category)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            if (category == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                
                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            catch (Exception)
            {
                
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error to included new category.");
            }         

            
        }

        [HttpDelete]
        [Route("category/{categoryId}")]
        public HttpResponseMessage DeleteCategory(int categoryId)
        {
            if (categoryId <= 0)
            return Request.CreateResponse(HttpStatusCode.NotFound);        

            db.Categories.Remove(db.Categories.Find(categoryId));
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Category deleted.");

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();

        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}