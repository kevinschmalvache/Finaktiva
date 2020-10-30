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
    public class ClsMunicipio
    {
        DB_TEST_Entities dbContext = new DB_TEST_Entities();
        List<Municipio> lstMunicipio = new List<Municipio>();
        Municipio objMunicipio = null;

        public async Task<List<Municipio>> GetMunicipios(int intId=0, string strNombre="")
        {
            dbContext = new DB_TEST_Entities();
            await Task.Run(async () =>
            {
                try
                {
                    lstMunicipio = dbContext.Municipio
                    .Where(x => x.id == (intId == 0 ? x.id : intId)
                        && x.nombre == (string.IsNullOrEmpty(strNombre) ? x.nombre : strNombre))
                    .ToList();

                }
                catch (Exception ex)
                {
                    lstMunicipio = new List<Municipio>();
                }
                finally
                {
                    dbContext.Dispose();
                }
            });

            return lstMunicipio;
        }

        //
        public async Task<List<Municipio>> GetMunicipiosxEstado(bool bitEstado)
        {
            dbContext = new DB_TEST_Entities();
            await Task.Run(async () =>
            {
                try
                {
                    lstMunicipio = dbContext.Municipio.Where(x => x.estado == bitEstado).ToList();
                }
                catch (Exception ex)
                {
                    lstMunicipio = new List<Municipio>();
                }
                finally
                {
                    dbContext.Dispose();
                }
            });

            return lstMunicipio;
        }

        public async Task<bool> SetMunicipioAsync(Municipio objMunicipio, short intAccion)
        {
            dbContext = new DB_TEST_Entities();
            try
            {
                switch (intAccion)
                {
                    case 1: //Add
                        objMunicipio.estado = true;
                        objMunicipio.fecha_edicion = DateTime.Now;
                        objMunicipio.fecha_creacion = DateTime.Now;
                        dbContext.Municipio.Add(objMunicipio);
                        break;
                    case 2: //Edit
                        objMunicipio.fecha_edicion = DateTime.Now;
                        dbContext.Entry(objMunicipio).State = System.Data.Entity.EntityState.Modified;
                        break;
                    case 3: //Delete
                        dbContext.Entry(dbContext.Municipio.Find(objMunicipio.id)).State = System.Data.Entity.EntityState.Deleted;
                        break;
                }
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception exc_)
            {
                return false;
            }
            finally
            {
                dbContext.Dispose();
            }
        }

        public bool ExistMunicipioId(int intId)
        {
            dbContext = new DB_TEST_Entities();
            try
            {
                objMunicipio = dbContext.Municipio.Find(intId);
            }
            catch (Exception exc)
            {

            }
            finally
            {
                dbContext.Dispose();
            }
            return objMunicipio != null;
        }

    }
}