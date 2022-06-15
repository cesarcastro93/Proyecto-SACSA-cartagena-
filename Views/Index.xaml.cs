using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vt_Controles_WPF_NetFr.Componentes.Botones;
using Vt_Controles_WPF_NetFr.Componentes.Cargando;
using Vt_Controles_WPF_NetFr.ISevicio;
using Vt_Controles_WPF_NetFr.Modelo;
using Vt_Controles_WPF_NetFr.Utilitario;
using SACSA.Resourses;
using System.Threading;
using System.Data;
using SACSA.TarifasSacsa;
using SACSA.Impresion;

namespace SACSA.Views
{
    /// <summary>
    /// Lógica de interacción para Index.xaml
    /// </summary>
    public partial class Index : Page, IServiciosBlackTouch,INotifyPropertyChanged
    {

        WSTarifas wsTarifas = new WSTarifas();
        DataSet DSIdiomas = new DataSet();
        DataTable dtIdioma = new DataTable();
        canvasDeImpresion CI = new canvasDeImpresion();
        TextToSpeech tx = new TextToSpeech();   

        public string Nombre { get; set; } = "Index";
        public Action<string> FuntionToRedirect { get; set; }
        public Index(Action<string>funtionToRedirect)
        {
            InitializeComponent();
            this.Background = Util.ObtenerFondo("./IMG/home.jpg");
            this.DataContext = this;
            InicializarEstilos();
            this.FuntionToRedirect = funtionToRedirect;
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public void InicializarEstilos()
        {
           this.btnEspañol.EstablecerEstadoBase();
           this.btnIngles.EstablecerEstadoBase();
        }

        public void EstablecerEstadoBase()
        {
            try
            {
                Globales.Logger.Debug("Estableciendo Estado base en el index");
                this.Background = Util.ObtenerFondo("./IMG/home.jpg");
                this.btnEspañol.EstablecerEstadoBase();
                this.btnIngles.EstablecerEstadoBase();
            }
            catch (Exception e)
            {
                Globales.Logger.Error(e.InnerException,"error al establecer Estado base en el index");
            }
        }
         
        public void EstablecerAltoContraste()
        {
            try
            {
                Globales.Logger.Debug("Estableciendo AltoContraste en el index");
                this.Background = Util.ObtenerFondo("./IMG/Home-AC.jpg");
                this.btnEspañol.EstablecerAltoContraste();
                this.btnIngles.EstablecerAltoContraste();
            }
            catch (Exception e )
            {
                Globales.Logger.Error(e.InnerException, "error al establecer AltoContraste en el index");
            }
        }

        public void EstablecerBlancoYNegro()
        {

        }

        public void EstablecerBlackTouch()
        {
            try
            {
                Globales.Logger.Debug("Estableciendo BlackTouch en el index");
                TextToSpeech tx = new TextToSpeech();
                this.Background = Util.ObtenerFondo("./IMG/1S_BT.jpg");
                this.btnEspañol.EstablecerBlackTouch();
                this.btnIngles.EstablecerBlackTouch();
                //Thread work = new Thread(new ParameterizedThreadStart(tx.TextSpeech));
                //work.Start(this.Rtexto.Text);
            }
            catch (Exception e )
            {

                Globales.Logger.Error(e.InnerException, "error al establecer BlackTouch en el index");
            }
            

        }

        private void btnEspañol_ComponenteClick(object sender, RoutedEventArgs e)
        {

            try
            {
                Globales.Logger.Debug("Se oprimio el boton con el idioma español");
                MainWindow.Idioma = 1;
                this.FuntionToRedirect("SelectedService");
            }
            catch (Exception ex)
            {
                Globales.Logger.Error(ex.InnerException,"Se ha producido un error al tratar de establecer el idioma español");   
            }
            
        }

        private void btnIngles_ComponenteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Globales.Logger.Debug("Se oprimio el boton con el idioma ingles");
                MainWindow.Idioma = 0;
                this.FuntionToRedirect("SelectedService");
                
            }
            catch (Exception ex)
            {

                Globales.Logger.Error(ex, "Se ha producido un error al tratar de establecer el idioma ingles");
            }
            
        }
    }
}
