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
    public class ClsRegionMunicipio
    {
        DB_TEST_Entities dbContext = new DB_TEST_Entities();
        bool Resultado = false;
        List<RegionMunicipio> lstRegionMunicipio = new List<RegionMunicipio>();

        public async Task<bool> SetMunicipiosRegiones(int intIdRegion, int[] ArrIdsMunicipios)
        {
            //dbContext = new DB_TEST_Entities();
            await Task.Run(async () =>
            {
                try
                {
                    string query = "DELETE FROM [dbo].[Region_Municipio] WHERE id_region = " + intIdRegion + ";";

                    foreach (int item in ArrIdsMunicipios)
                    {
                        query += String.Format(System.Environment.NewLine + "INSERT INTO [dbo].[Region_Municipio] ([id_region] ,[id_municipio]) VALUES ({0} ,{1});", intIdRegion, item);
                    }
                    dbContext.Database.ExecuteSqlCommand(query);
                    Resultado = true;
                }
                catch (Exception ex)
                {
                    Resultado = false;
                }
                finally
                {
                    //dbContext.Dispose();
                }
            });

            return Resultado;
        }

        public async Task<List<RegionMunicipio>> GetRegionesMunicipios(int intIdRegion = 0)
        {
            dbContext = new DB_TEST_Entities();
            await Task.Run(async () =>
            {
                try
                {
                    string query = "SELECT [id_region] ,[id_municipio] FROM [dbo].[Region_Municipio] ";

                    if (intIdRegion > 0)
                    {
                        query += "Where [id_region]=" + intIdRegion;
                    }
                    lstRegionMunicipio = dbContext.Database.SqlQuery<RegionMunicipio>(query).ToList();
                }
                catch (Exception ex)
                {
                    lstRegionMunicipio = new List<RegionMunicipio>();
                }
                finally
                {
                    dbContext.Dispose();
                }
            });

            return lstRegionMunicipio;
        }

    }
}