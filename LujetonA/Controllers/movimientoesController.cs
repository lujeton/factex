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
using LujetonA.Models.aux_object;

namespace LujetonA.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class movimientoesController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/movimientoes
        public IQueryable<object> Getmovimiento()
        {
            return db.movimiento.Select(x => new {
                id = x.id,
                x.fecha,
                producto_nombre = x.producto.descripcion,
                x.cantidad,
                bodega_nombre = x.bodega.nombre,
                x.valor,
                tmovimiento_nombre = x.tipomovimiento.descripcion,
                proveedor_descripcion = x.proveedor.representante
            });

        }

        // GET: api/movimientoes/5
        [ResponseType(typeof(movimiento))]
        public IHttpActionResult Getmovimiento(int id)
        {
            movimiento movimiento = db.movimiento.Find(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return Ok(movimiento);
        }

        // PUT: api/movimientoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmovimiento(int id, movimiento movimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movimiento.id)
            {
                return BadRequest();
            }

            db.Entry(movimiento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!movimientoExists(id))
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

        // POST: api/movimientoes
        [ResponseType(typeof(movimiento))]
        public IHttpActionResult Postmovimiento(movimiento movimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var store_movimiento = db.movimiento.Add(movimiento);
            db.SaveChanges();
            var store_producto = db.producto.Find(store_movimiento.idProducto);

            if (store_producto.stock == null) {
                store_producto.stock = 0;
            }

            if (movimiento.idTMovimiento == "1") {
                store_producto.stock+= movimiento.cantidad;
            }
            else
            {
                if (store_producto.stock > movimiento.cantidad)
                    store_producto.stock -= movimiento.cantidad;
                else
                {
                    return BadRequest("cantidad no funciona");
                }
            }

            var k = store_producto.id;
            var pbd = new producto_bodega() {
                idBodega = movimiento.idBodega,
                idProducto = k,
                idMovimiento = store_movimiento.id,
                cantidad = store_movimiento.cantidad
            };            
            db.producto_bodega.Add(pbd);
            try
            {
                db.SaveChanges();                
            }
            catch (DbUpdateException ex)
            {
                string str = ex.Message;
                if (movimientoExists(movimiento.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = movimiento.id }, movimiento);
        }

        [Route("api/movimientoes/traslado")]
        public string do_traslado(traslado _traslado) {
            var _mov_salida = new producto_bodega();
            var _mov_entrada = new producto_bodega();

            #region preparando salida

            var bodegaInicial = db.producto_bodega.Where(x => x.idProducto == _traslado.IdProducto && x.idBodega == _traslado.IdBodegaSalida).ToList();
            if (bodegaInicial.Count() < 1) {
                return "no hay stock en la bodega que ha solicitado";
            }

            var bodegaFuente = bodegaInicial.First();

            if (bodegaFuente.cantidad < _traslado.Cantidad) {
                return "no hay stock suficiento en la bodega que ha solicitado";
            }

            bodegaFuente.cantidad -= _traslado.Cantidad;
            db.Entry(bodegaFuente).State = EntityState.Modified;

            var bodegaDestina = db.producto_bodega.Where(x => x.idProducto == _traslado.IdProducto && x.idBodega == _traslado.IdBodegaDestino).FirstOrDefault();

            if (bodegaDestina == null) {
                bodegaDestina = new producto_bodega();
                bodegaDestina.idBodega = _traslado.IdBodegaDestino;
                bodegaDestina.cantidad = _traslado.Cantidad;
                bodegaDestina.idProducto = _traslado.IdProducto;
                db.producto_bodega.Add(bodegaDestina);
            }
            else
            {
                bodegaDestina.cantidad += _traslado.Cantidad;
                db.Entry(bodegaDestina).State = EntityState.Modified;
            }

            try
            {
                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "ocurrio un error:" + ex.Message;
            }

            #endregion
        }

        // DELETE: api/movimientoes/5
        [ResponseType(typeof(movimiento))]
        public IHttpActionResult Deletemovimiento(int id)
        {
            movimiento movimiento = db.movimiento.Find(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            db.movimiento.Remove(movimiento);
            db.SaveChanges();

            return Ok(movimiento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool movimientoExists(int id)
        {
            return db.movimiento.Count(e => e.id == id) > 0;
        }
    }
}