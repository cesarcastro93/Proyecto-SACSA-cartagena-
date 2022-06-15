using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SACSA.TarifasSacsa;
using SACSA.Views;
using SACSA.Resourses;

namespace SACSA.Controller
{
    public class ConsumoWS
    {

        WSTarifas tarifas = new WSTarifas();
        DataSet DSobtenerSectores = new DataSet();
        public DataTable DTobtenerSectores = new DataTable();



        public void MostrarDestinos()
        {
            try
            {
                DSobtenerSectores = tarifas.ObtenerSectores();
                DTobtenerSectores = DSobtenerSectores.Tables[0];
                Globales.Logger.Info("Consultando Servicio Web");
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
                Globales.Logger.Error(ex.InnerException, "No se puede conectar con el servidor, verifique conectividad");
            }

        }
        public DataView FiltrarDestinos(string textoaBuscar)
        {
            try
            {
                Globales.Logger.Info("Lista de Destinos cargada con exito");
                DataView dv = new DataView(DTobtenerSectores);
                dv.RowFilter = $"NOMBRE like '%{textoaBuscar}%'";

                return dv;
            }
            catch (Exception ex)
            {
                Globales.Logger.Error(ex.InnerException, "error de conexion con el servidor");
                Console.WriteLine("error " + ex.Message);

                return null;
            }

        }
    }
}
