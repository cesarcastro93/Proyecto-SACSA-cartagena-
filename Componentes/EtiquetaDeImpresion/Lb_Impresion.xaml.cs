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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SACSA.Componentes.EtiquetaDeImpresion
{
    /// <summary>
    /// Lógica de interacción para Lb_Impresion.xaml
    /// </summary>
    public partial class Lb_Impresion : UserControl
    {
        public int TamanoFuente { get; set; }
        public int TamanoMaximo { get; set; }
        public Lb_Impresion()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
