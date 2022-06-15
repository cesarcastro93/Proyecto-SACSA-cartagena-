using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SACSA.TarifasSacsa;

namespace SACSA.Controller
{
    public class RegistrarTicket
    {
        WSTarifas WStarifas = new WSTarifas();
        public void RegistrarTicketEnBD()
        {
            WStarifas.InsertaTicketImpreso(Convert.ToInt32(MainWindow.InfoDestino[2]), MainWindow.InfoDestino[0], Convert.ToInt32(MainWindow.InfoDestino[1]), 700, 15300);
        }
    }
}
