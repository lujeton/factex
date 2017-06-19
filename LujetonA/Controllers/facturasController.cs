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

namespace LujetonA.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class facturasController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/facturas
        public IQueryable<factura> Getfactura()
        {
            return db.factura;
        }

        // GET: api/facturas/5
        [ResponseType(typeof(factura))]
        public IHttpActionResult Getfactura(int id)
        {
            factura factura = db.factura.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        // PUT: api/facturas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putfactura(int id, factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factura.id)
            {
                return BadRequest();
            }

            db.Entry(factura).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!facturaExists(id))
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

        // POST: api/facturas
        [ResponseType(typeof(factura))]
        public IHttpActionResult Postfactura(Models.aux_object.Factura_completa factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //retorna la factura con el id que se le asignara en el onsave
            var factura_añadida = db.factura.Add(factura.header);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            

            factura.detalles.ForEach((item) => {
                item.idFactura = factura_añadida.id;
            });

            db.detalle_factura.AddRange(factura.detalles);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (facturaExists(factura.header.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = factura.header.id }, factura);
        }

        // DELETE: api/facturas/5
        [ResponseType(typeof(factura))]
        public IHttpActionResult Deletefactura(int id)
        {
            factura factura = db.factura.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            db.factura.Remove(factura);
            db.SaveChanges();

            return Ok(factura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool facturaExists(int id)
        {
            return db.factura.Count(e => e.id == id) > 0;
        }
    }
}