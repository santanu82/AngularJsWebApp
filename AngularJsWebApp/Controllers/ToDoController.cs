using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AngularJsWebApp.Models;

namespace AngularJsWebApp.Controllers
{
    public class ToDoController : ApiController
    {
        private ToDoContext db = new ToDoContext();

        // GET api/ToDo
        public IEnumerable<ToDo> GetToDoes()
        {
            return db.ToDoes.AsEnumerable();
        }

        // GET api/ToDo/5
        public ToDo GetToDo(int id)
        {
            ToDo todo = db.ToDoes.Find(id);
            if (todo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return todo;
        }

        // PUT api/ToDo/5
        public HttpResponseMessage PutToDo(int id, ToDo todo)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != todo.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(todo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/ToDo
        public HttpResponseMessage PostToDo(ToDo todo)
        {
            if (ModelState.IsValid)
            {
                db.ToDoes.Add(todo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, todo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = todo.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ToDo/5
        public HttpResponseMessage DeleteToDo(int id)
        {
            ToDo todo = db.ToDoes.Find(id);
            if (todo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ToDoes.Remove(todo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, todo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}