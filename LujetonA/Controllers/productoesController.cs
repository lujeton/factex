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
using LujetonA.Models;
using System.Web.Http.Cors;
using LujetonA.logicBuiness;
using System.Web;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LujetonA.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class productoesController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        [Route("api/productoes/imageManager/")]
        public async Task<HttpResponseMessage> PostFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Images");
            var provider = new CustomMultipartFormDataStreamProvider(root);


            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        // GET: api/productoes
        public IQueryable<v_producto_real_stock> Getproducto()
        {
            return db.v_producto_real_stock;
        }

        // GET: api/productoes/5
        [ResponseType(typeof(producto))]
        public IHttpActionResult Getproducto(int id)
        {
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/productoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproducto(int id, producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.id)
            {
                return BadRequest();
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productoExists(id))
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

        // POST: api/productoes
        [ResponseType(typeof(producto))]
        public IHttpActionResult Postproducto(producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("error de modelo");
            }


            db.producto.Add(producto);
            db.SaveChanges();

            return Ok(producto);
        }

        // DELETE: api/productoes/5
        [ResponseType(typeof(producto))]
        public IHttpActionResult Deleteproducto(int id)
        {
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.producto.Remove(producto);
            db.SaveChanges();

            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productoExists(int id)
        {
            return db.producto.Count(e => e.id == id) > 0;
        }
    }
}