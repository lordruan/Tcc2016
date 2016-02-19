using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Tcc2016.Models;

namespace Tcc2016.Controllers
{
    public class categoriesController : ApiController
    {
        private tcc_newEntities db = new tcc_newEntities();

        // GET: api/categories
        public IQueryable<categories> Getcategories()
        {
            return db.categories;
        }

        // GET: api/categories/5
        [ResponseType(typeof(categories))]
        public IHttpActionResult Getcategories(decimal id)
        {
            categories categories = db.categories.Find(id);
            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        // PUT: api/categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcategories(decimal id, categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categories.id)
            {
                return BadRequest();
            }

            db.Entry(categories).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/categories
        [ResponseType(typeof(categories))]
        public IHttpActionResult Postcategories(categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.categories.Add(categories);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categories.id }, categories);
        }

        // DELETE: api/categories/5
        [ResponseType(typeof(categories))]
        public IHttpActionResult Deletecategories(decimal id)
        {
            categories categories = db.categories.Find(id);
            if (categories == null)
            {
                return NotFound();
            }

            db.categories.Remove(categories);
            db.SaveChanges();

            return Ok(categories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool categoriesExists(decimal id)
        {
            return db.categories.Count(e => e.id == id) > 0;
        }
    }
}