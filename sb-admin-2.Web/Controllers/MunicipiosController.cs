using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sb_admin_2.Web.Models.Edmx;
using sb_admin_2.Web.Models.Edmx.Class;

namespace sb_admin_2.Web.Controllers
{
    public class MunicipiosController : Controller
    {
        ClsMunicipio objClsMunicipio = new ClsMunicipio();
        List<Municipio> lstMunicipio = new List<Municipio>();
        Municipio objMunicipio = new Municipio();
        string strMsj = "";

        // GET: Municipios
        public async Task<ActionResult> Index()
        {
            return View(await objClsMunicipio.GetMunicipios());
        }

        // GET: Municipios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Municipios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Municipio municipio)
        {
            if (objClsMunicipio.ExistMunicipioId(municipio.id))
            {
                //ya existe
                strMsj = "toastr[`warning`](`Ya se encuentra registrado un Municipio con ese ID #" + municipio.id + " `, `Municipio`);";
            }
            else
            {
                if (await objClsMunicipio.SetMunicipioAsync(municipio, 1))
                {
                    //msj si guardo
                    strMsj = "toastr[`success`](`Municipio guardado exitosamente!`, `Municipio`);";
                }
                else
                {
                    //msj no guardo
                    strMsj = "toastr[`error`](`Error durante el proceso de guardado!`, `Municipio`);";
                }
            }
            TempData["MsjGuardado"] = strMsj;
            return RedirectToAction("Index");
        }

        // GET: Municipios/Edit/5
        public async Task<ActionResult> Edit(int id = 0)
        {
            lstMunicipio = await objClsMunicipio.GetMunicipios(id);
            return View(lstMunicipio.FirstOrDefault());
        }

        // POST: Municipios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Municipio municipio)
        {
            if (await objClsMunicipio.SetMunicipioAsync(municipio, 2))
            {
                //msj si guardo
                strMsj = "toastr[`success`](`Municipio editado exitosamente!`, `Municipio`);";
            }
            else
            {
                //msj no guardo
                strMsj = "toastr[`error`](`Error durante el proceso de editado!`, `Municipio`);";
            }
            TempData["MsjGuardado"] = strMsj;
            return RedirectToAction("Index");
        }


        // POST: Municipios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(int id)
        {
            objMunicipio.id = id;
            return Json(new
            {
                resultado = await objClsMunicipio.SetMunicipioAsync(objMunicipio, 3)
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
