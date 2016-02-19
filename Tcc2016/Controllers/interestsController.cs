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
    public class interestsController : ApiController
    {
        private tcc_newEntities db = new tcc_newEntities();

        // GET: api/interests
        public IQueryable<interests> Getinterests()
        {
            return db.interests;
        }

        // GET: api/interests/5
        [ResponseType(typeof(interests))]
        public IHttpActionResult Getinterests(decimal id)
        {
            interests interests = db.interests.Find(id);
            if (interests == null)
            {
                return NotFound();
            }

            return Ok(interests);
        }

        // PUT: api/interests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putinterests(decimal id, interests interests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != interests.id)
            {
                return BadRequest();
            }

            db.Entry(interests).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!interestsExists(id))
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

        // POST: api/interests
        [ResponseType(typeof(interests))]
        public IHttpActionResult Postinterests(interests interests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.interests.Add(interests);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = interests.id }, interests);
        }

        // DELETE: api/interests/5
        [ResponseType(typeof(interests))]
        public IHttpActionResult Deleteinterests(decimal id)
        {
            interests interests = db.interests.Find(id);
            if (interests == null)
            {
                return NotFound();
            }

            db.interests.Remove(interests);
            db.SaveChanges();

            return Ok(interests);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool interestsExists(decimal id)
        {
            return db.interests.Count(e => e.id == id) > 0;
        }
    }
}