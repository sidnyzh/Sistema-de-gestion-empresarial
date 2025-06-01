using MVCFacturacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFacturacion.Controllers
{
    public class TBLCLientesController : Controller
    {
        // GET: TBLCLientes
        public ActionResult Index()
        {
            using (DBFacturacion db = new DBFacturacion())
            {
                var clientes = db.TBLCLIENTES.ToList();
                return View(clientes);
            }
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DBFacturacion db = new DBFacturacion())
                    {
                        var nuevoCliente = new TBLCLIENTES
                        {
                            StrNombre = collection["StrNombre"],
                            NumDocumento = long.Parse(collection["NumDocumento"]),
                            StrDireccion = collection["StrDireccion"],
                            StrTelefono = collection["StrTelefono"],
                            StrEmail = collection["StrEmail"],
                            DtmFechaModifica = DateTime.Now,
                            StrUsuarioModifica = "javier"
                        };

                        db.TBLCLIENTES.Add(nuevoCliente);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

                return View(collection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int id)
        {
            using (DBFacturacion db = new DBFacturacion())
            {
                var cliente = db.TBLCLIENTES.Find(id);
                return View(cliente);
            }
        }

        [HttpPost]
        public ActionResult Editar(TBLCLIENTES model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DBFacturacion db = new DBFacturacion())
                    {
                        var cliente = db.TBLCLIENTES.Find(model.IdCliente);
                        cliente.StrNombre = model.StrNombre;
                        cliente.NumDocumento = model.NumDocumento;
                        cliente.StrDireccion = model.StrDireccion;
                        cliente.StrTelefono = model.StrTelefono;
                        cliente.StrEmail = model.StrEmail;
                        cliente.DtmFechaModifica = DateTime.Now;
                        cliente.StrUsuarioModifica = "javier";

                        db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Borrar(int id)
        {
            try
            {
                using (DBFacturacion db = new DBFacturacion())
                {
                    var cliente = db.TBLCLIENTES.Find(id);
                    db.TBLCLIENTES.Remove(cliente);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}   