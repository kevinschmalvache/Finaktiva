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
using sb_admin_2.Web.Models;

namespace sb_admin_2.Web.Controllers
{
    public class RegionesController : Controller
    {
        private DB_TEST_Entities db = new DB_TEST_Entities();

        Region objRegion = new Region();
        List<Region> lstRegion = new List<Region>();
        ClsRegion objClsRegion = new ClsRegion();

        ClsMunicipio objClsMunicipio = new ClsMunicipio();
        List<Municipio> lstMunicipios = new List<Municipio>();
        List<RegionMunicipio> lstRegionMunicipio = new List<RegionMunicipio>();

        ClsRegionMunicipio clsRegionMunicipio = new ClsRegionMunicipio();

        string strMsj = "";

        // GET: Regiones
        public async Task<ActionResult> Index()
        {
            return View(await objClsRegion.GetRegiones());
        }

        // GET: Regiones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Regiones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Region region)
        {
            if (objClsRegion.ExistRegionId(region.id))
            {
                //ya existe
                strMsj = "toastr[`warning`](`Ya se encuentra registrado una Región con ese ID #" + region.id + " `, `Región`);";
            }
            else
            {
                if (await objClsRegion.SetRegionAsync(region, 1))
                {
                    //msj si guardo
                    strMsj = "toastr[`success`](`Región guardada exitosamente!`, `Región`);";
                }
                else
                {
                    //msj no guardo
                    strMsj = "toastr[`error`](`Error durante el proceso de guardado!`, `Región`);";
                }
            }
            TempData["MsjGuardado"] = strMsj;
            return RedirectToAction("Index");
        }

        // GET: Regiones/Edit/5
        public async Task<ActionResult> Edit(int id = 0)
        {
            lstMunicipios = await objClsMunicipio.GetMunicipiosxEstado(true);

            lstRegion = await objClsRegion.GetRegiones(id);
            ViewBag.LstMunicipiosActs = lstMunicipios;

            lstRegionMunicipio = await clsRegionMunicipio.GetRegionesMunicipios(id);
            ViewBag.LstRegionesMunicipios = lstRegionMunicipio;

            return View(lstRegion.FirstOrDefault());
        }

        // POST: Regiones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Region region, int[] selectMunicipios)
        {
            if (await objClsRegion.SetRegionAsync(region, 2, selectMunicipios))
            {
                //msj si guardo
                strMsj = "toastr[`success`](`Región editada exitosamente!`, `Región`);";
            }
            else
            {
                //msj no guardo
                strMsj = "toastr[`error`](`Error durante el proceso de editado!`, `Región`);";
            }
            TempData["MsjGuardado"] = strMsj;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(int id)
        {
            objRegion.id = id;
            return Json(new
            {
                resultado = await objClsRegion.SetRegionAsync(objRegion, 3)
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
