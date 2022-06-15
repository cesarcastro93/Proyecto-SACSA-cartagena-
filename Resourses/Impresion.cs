using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using SACSA.Componentes.Impresion;

namespace SACSA.Resourses
{
    public class Impresion
    {
        public void ImpresionTurno()
        {
            CanvasImpresionTurno win2 = new CanvasImpresionTurno();
            PrintDialog dialog = new PrintDialog();
            win2.PintarInformacion();
            win2.Show();
            win2.Left = 3000;
            dialog.PrintVisual(win2.PanelImpresion, "");
            win2.Close();
            
        }
    }
}
