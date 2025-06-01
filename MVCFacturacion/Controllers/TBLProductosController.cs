using MVCFacturacion.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVCFacturacion.Controllers
{
    public class TBLProductosController : Controller
    {
        // GET: TBLProductos
        public ActionResult Index()
        {
            using (var db = new DBFacturacion())
            {
                var productos = db.TBLPRODUCTO
                         .Include(p => p.TBLCATEGORIA_PROD) 
                         .ToList();
                return View(productos);
            }
        }
        public ActionResult Nuevo()
        {
            using (var db = new DBFacturacion())
            {
                ViewBag.IdCategoria = new SelectList(db.TBLCATEGORIA_PROD.ToList(), "IdCategoria", "StrDescripcion");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Nuevo(TBLPRODUCTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new DBFacturacion())
                    {
                        model.DtmFechaModifica = DateTime.Now;
                        model.StrUsuarioModifica = "javier";
                        db.TBLPRODUCTO.Add(model);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

                using (var db = new DBFacturacion())
                {
                    ViewBag.IdCategoria = new SelectList(db.TBLCATEGORIA_PROD.ToList(), "IdCategoria", "StrDescripcion");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int id)
        {
            using (var db = new DBFacturacion())
            {
                var producto = db.TBLPRODUCTO.Find(id);
                ViewBag.IdCategoria = new SelectList(db.TBLCATEGORIA_PROD.ToList(), "IdCategoria", "StrDescripcion");
                return View(producto);
            }
        }

        [HttpPost]
        public ActionResult Editar(TBLPRODUCTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new DBFacturacion())
                    {
                        var producto = db.TBLPRODUCTO.Find(model.IdProducto);
                        producto.StrNombre = model.StrNombre;
                        producto.StrCodigo = model.StrCodigo;
                        producto.NumPrecioCompra = model.NumPrecioCompra;
                        producto.NumPrecioVenta = model.NumPrecioVenta;
                        producto.IdCategoria = model.IdCategoria;
                        producto.StrDetalle = model.StrDetalle;
                        producto.strFoto = model.strFoto;
                        producto.NumStock = model.NumStock;
                        producto.DtmFechaModifica = DateTime.Now;
                        producto.StrUsuarioModifica = "javier";

                        db.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

                using (var db = new DBFacturacion())
                {
                    ViewBag.IdCategoria = new SelectList(db.TBLCATEGORIA_PROD.ToList(), "IdCategoria", "StrNombre", model.IdCategoria);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Borrar(int id)
        {
            try
            {
                using (var db = new DBFacturacion())
                {
                    var producto = db.TBLPRODUCTO.Find(id);
                    db.TBLPRODUCTO.Remove(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}