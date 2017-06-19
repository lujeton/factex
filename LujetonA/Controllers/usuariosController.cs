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
using System.Web;
using LujetonA.logicBuiness;
using System.Web.Http.Cors;

namespace LujetonA.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class usuariosController : ApiController
    {
        private bd_proyectoEntities1 db = new bd_proyectoEntities1();

        // GET: api/usuarios
        public IQueryable<usuario> Getusuario()
        {
            return db.usuario;
        }

        // GET: api/usuarios/5
        [ResponseType(typeof(usuario))]
        public IHttpActionResult Getusuario(int id)
        {
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putusuario(int id, usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.idusuario)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuarioExists(id))
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

        // POST: api/usuarios
        [ResponseType(typeof(usuario))]
        public IHttpActionResult Postusuario(usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.usuario.Add(usuario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (usuarioExists(usuario.idusuario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = usuario.idusuario }, usuario);
        }

        // DELETE: api/usuarios/5
        [ResponseType(typeof(usuario))]
        public IHttpActionResult Deleteusuario(int id)
        {
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.usuario.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usuarioExists(int id)
        {
            return db.usuario.Count(e => e.idusuario == id) > 0;
        }

        [HttpPost]
        [Route("usuario/login")]
        public HttpResponseMessage login(usuario request_user) {
            if(request_user == null) return Request.CreateResponse(HttpStatusCode.OK, "bad");

            var store_user = db.usuario.Where(x => x.user == request_user.user && x.pass == request_user.pass).FirstOrDefault();
            if (store_user != null) {
                string token = logicBuiness.tokenProcessor.GetToken(store_user.idusuario, DateTime.UtcNow);
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "bad");
            }                     
        }

        [HttpPost]
        [RestAuthorize]
        [Route("usuario/getmenu/")]
        public HttpResponseMessage getMenu() {
            //seg * min
            var token = ActionContext.Request.Headers.Authorization;
            var usuario = logicBuiness.tokenProcessor.ValidateToken(token.Parameter, 60 * 60);
            if (usuario == null) return Request.CreateResponse(HttpStatusCode.OK,"bad");       
            var menus = db.menu_perfil.Where(x => x.id_perfil == usuario.idperfil).Select(y => y.menu).ToList();
            var records = from entity in menus
                          select new
                          {
                              url = entity.url,
                              descripcion = entity.descripcion
                          };                        
            return Request.CreateResponse(HttpStatusCode.OK, records);
        }        
    }
}