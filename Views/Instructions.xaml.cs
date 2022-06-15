using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Vt_Controles_WPF_NetFr.ISevicio;
using Vt_Controles_WPF_NetFr.Utilitario;
using Vt_Controles_WPF_NetFr.Componentes.Botones;
using Vt_Controles_WPF_NetFr.Componentes.Cargando;
using SACSA.Resourses;
using System.Threading;

namespace SACSA.Views
{
    /// <summary>
    /// Lógica de interacción para Instructions.xaml
    /// </summary>
    public partial class Instructions : Page, IServiciosBlackTouch, INotifyPropertyChanged
    {
        public string Nombre { get; set; } = "Instructions";
        public Action<string> FuntionToRedirect { get; set; }

        public Instructions(Action<string> funtionToRedirect)
        {
            InitializeComponent();
            this.Background = Util.ObtenerFondo("./IMG/2-S.jpg");
            this.FuntionToRedirect = funtionToRedirect;
            InicializarEstilos();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InicializarEstilos()
        {
            this.btnBack.EstablecerEstadoBase();
            this.btnNext.EstablecerEstadoBase();
        }
        public void EstablecerEstadoBase()
        {
            try
            {
                this.Background = Util.ObtenerFondo("./IMG/2-S.jpg");
                this.btnBack.EstablecerEstadoBase();
                this.btnNext.EstablecerEstadoBase();

            }
            catch (Exception e )
            {
                Console.WriteLine(e);
            }    
        }

        public void EstablecerAltoContraste()
        {
            try
            {
                this.Background = Util.ObtenerFondo("./IMG/2-S-AC.jpg");
                this.btnBack.EstablecerAltoContraste();
                this.btnNext.EstablecerAltoContraste();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void EstablecerBlackTouch()
        {
            try
            {  
                this.Background = Util.ObtenerFondo("./IMG/1S_BT.jpg");
                this.btnBack.EstablecerBlackTouch();
                this.btnNext.EstablecerBlackTouch();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void EstablecerBlancoYNegro()
        {
            
        }

        private void btn_nextInstructions_Click(object sender, RoutedEventArgs e)
        {
            this.FuntionToRedirect("SelectedService");
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.FuntionToRedirect("Index");
        }
    }
}
