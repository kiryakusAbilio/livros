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
    public class BookController : ApiController
    {
        private BookStoreDataContext db = new BookStoreDataContext();

        [HttpGet]
        [Route("books")]
        public HttpResponseMessage GetBooks()
        {
            var result = db.Books.Include("Category").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("book/{bookId}")]
        public HttpResponseMessage GetBook(int bookId)
        {
            Book book = db.Books.Find(bookId);
            if (book == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, book);
        }

        [HttpPut]
        [Route("book/{bookId}")]
        public HttpResponseMessage PutCategory(int bookId, Book book)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (bookId != book.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(bookId))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Category updated.");
        }

        [HttpPost]
        [Route("books")]
        public HttpResponseMessage PostBook(Book book)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            if (book == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Books.Add(book);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, book);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error to included new book.");
            }


        }
        [HttpDelete]
        [Route("book/{bookId}")]
        public HttpResponseMessage DeleteBook(int bookId)
        {
            if (bookId <= 0)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            db.Books.Remove(db.Books.Find(bookId));
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Book deleted.");

        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();

        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}