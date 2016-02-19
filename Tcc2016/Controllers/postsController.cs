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
    public class postsController : ApiController
    {
        private tcc_newEntities db = new tcc_newEntities();

        // GET: api/posts
        public IQueryable<posts> Getposts()
        {
            return db.posts;
        }

        // GET: api/posts/5
        [ResponseType(typeof(posts))]
        public IHttpActionResult Getposts(decimal id)
        {
            posts posts = db.posts.Find(id);
            if (posts == null)
            {
                return NotFound();
            }

            return Ok(posts);
        }


        [ResponseType(typeof(posts))]
        public IHttpActionResult GetpostsByCategory(decimal category_id)
        {
            var posts = db.posts.Where(c => c.category_id == category_id);
            if (posts == null)
            {
                return NotFound();
            }

            return Ok(posts);
        }
        // PUT: api/posts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putposts(decimal id, posts posts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != posts.id)
            {
                return BadRequest();
            }

            db.Entry(posts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!postsExists(id))
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

        // POST: api/posts
        [ResponseType(typeof(posts))]
        public IHttpActionResult Postposts(posts posts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.posts.Add(posts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = posts.id }, posts);
        }

        // DELETE: api/posts/5
        [ResponseType(typeof(posts))]
        public IHttpActionResult Deleteposts(decimal id)
        {
            posts posts = db.posts.Find(id);
            if (posts == null)
            {
                return NotFound();
            }

            db.posts.Remove(posts);
            db.SaveChanges();

            return Ok(posts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool postsExists(decimal id)
        {
            return db.posts.Count(e => e.id == id) > 0;
        }
    }
}