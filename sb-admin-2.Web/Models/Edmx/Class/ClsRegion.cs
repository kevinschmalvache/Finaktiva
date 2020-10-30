using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections;

namespace sb_admin_2.Web.Models.Edmx.Class
{
    public class ClsRegion
    {
        DB_TEST_Entities dbContext = new DB_TEST_Entities();
        List<Region> lstRegion = new List<Region>();
        Region objRegion = null;
        bool Resultado = false;
        ClsRegionMunicipio objClsRegionMunicipio = new ClsRegionMunicipio();


        public async Task<List<Region>> GetRegiones(int intId = 0, string strNombre = "")
        {
            dbContext = new DB_TEST_Entities();
            await Task.Run(async () =>
            {
                try
                {
                    lstRegion = dbContext.Region
                    .Where(x => x.id == (intId == 0 ? x.id : intId)
                        && x.nombre == (string.IsNullOrEmpty(strNombre) ? x.nombre : strNombre))
                    .ToList();

                }
                catch (Exception ex)
                {
                    lstRegion = new List<Region>();
                }
                finally
                {
                    dbContext.Dispose();
                }
            });

            return lstRegion;
        }

        public async Task<bool> SetRegionAsync(Region objRegion, short intAccion, int[] Municipios = null)
        {
            dbContext = new DB_TEST_Entities();
            try
            {
                switch (intAccion)
                {
                    case 1: //Add
                        objRegion.fecha_edicion = DateTime.Now;
                        objRegion.fecha_creacion = DateTime.Now;
                        dbContext.Region.Add(objRegion);
                        break;
                    case 2: //Edit
                        objRegion.fecha_edicion = DateTime.Now;
                        dbContext.Entry(objRegion).State = System.Data.Entity.EntityState.Modified;
                        if (Municipios != null)
                        {
                            if (await objClsRegionMunicipio.SetMunicipiosRegiones(objRegion.id, Municipios) == false)
                            {
                                Resultado = false;
                            }
                        }
                       
                        break;
                    case 3: //Delete
                        dbContext.Entry(dbContext.Region.Find(objRegion.id)).State = System.Data.Entity.EntityState.Deleted;
                        break;
                }
                dbContext.SaveChanges();
                Resultado = true;
            }
            catch (Exception exc_)
            {
                Resultado = false;
            }
            finally
            {
                dbContext.Dispose();
            }

            return Resultado;
        }

        public bool ExistRegionId(int intId)
        {
            dbContext = new DB_TEST_Entities();
            try
            {
                objRegion = dbContext.Region.Find(intId);
            }
            catch (Exception exc)
            {
            }
            finally
            {
                dbContext.Dispose();
            }
            return objRegion != null;
        }
    }
}