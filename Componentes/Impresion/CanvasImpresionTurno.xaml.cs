using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vt_Controles_WPF_NetFr.Utilitario;
using SACSA.Model;

namespace SACSA.Componentes.Impresion
{
    /// <summary>
    /// Lógica de interacción para CanvasImpresionTurno.xaml
    /// </summary>
    public partial class CanvasImpresionTurno : Window
    {
        public TicketeImpreso ticket { get; set; }
        private int AnchoMaximo = 300;
        public CanvasImpresionTurno()
        {
            InitializeComponent();
        }

        public void PintarInformacion() 
        {
            BitmapImage bi = Util.EstablecerImagen("./IMG/LogoTicket.png");

            Ima_Logo.Source = bi;
            lb_TextoBienvenida.lb_Mensaje_Normal_.Text = ("Bienvenido al aeropuero de cartagena de indias ");
            lb_NumTicket.lb_Mensaje_Normal_.Text = ("0000");
            lb_Advert.lb_Mensaje_Normal_.Text = ("POR SU SEGURIDAD UTILICE LOS TAXIS AUTORIZADOS ");
            lb_Advert2.lb_Mensaje_Normal_.Text = ("ESTE TIQUETE NO ES VALIDO COMO FACTURA");
            //lb_Destino.lb_Mensaje_Normal_.Text = ticket.Destino ;
            //Lb_Fecha.lb_Mensaje_Normal_.Text = $"{ticket.FechaHora} - {DateTime.Now.ToString("yyyy/MM/dd")}";
            //lb_ValorAPagar.lb_Mensaje_Normal_.Text = ticket.ValorAPAgar;
            lb_Decreto.lb_Mensaje_Normal_.Text = ("El valor a pagar es el de tarifas oficiales segun el decreto 0482 expedido en 2021");
            lb_ConserveTicket.lb_Mensaje_Normal_.Text = ("CONSERVE SU TIQUETE HASTA LLEGAR A SU DESTINO");
            lb_Advert3.lb_Mensaje_Normal_.Text = ("VERIFIQUE SU EQUIPAJE ANTES DE ABANDONAR ESTE VEHICULO");
            lb_Sugerencias.lb_Mensaje_Normal_.Text = ("Sugerencias y observaciones (5)6931351 EXT 2740-2747");
            lb_PaginaSACSA.lb_Mensaje_Normal_.Text = ("www.aeropuertocartagena.com.co");

           DetailsTurn.Visibility = Visibility.Visible;
        }
    }
}
