using SACSA.Resourses;
using SACSA.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using SACSA.Resourses;

namespace SACSA
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int Idioma = 1;
        public static string[] InfoDestino = new string[4];
        public static CultureInfo InfoPais = CultureInfo.CreateSpecificCulture("es-CO");
          

        public List<IServiciosBlackTouch> Paginas { get; set; }
        public Index index { get; set; }
        public ResumeService resumeService { get; set; }
        public SelectedService selectedService { get; set; }
        public Instructions instructions { get; set; }




        public MainWindow()
        {
            InfoPais.NumberFormat.CurrencyDecimalDigits = 0;

            InitializeComponent();
            this.InicializePages();
            Globales.InicializeLog();
            this.FramePrincipal.Content = Paginas.First();
            this.PanelApariencia.Background = Util.ObtenerFondo("./IMG/home-2.jpg");
            this.PanelDesarrollado.Background = Util.ObtenerFondo("./IMG/home-3.jpg");

        }

        private void InicializePages() {
            this.Paginas = new List<IServiciosBlackTouch>();
            this.Paginas.Add(new Index(this.RedirectPage));
            this.Paginas.Add(new SelectedService(this.RedirectPage));
            this.Paginas.Add(new ResumeService(this.RedirectPage));
            this.Paginas.Add(new Instructions(this.RedirectPage));
        }

        private void RedirectPage(string pageName) {
 
            switch (pageName)
            {
                case "Index":
                    this.FramePrincipal.Content = this.Paginas.First();
                    break;

                case "Instructions":
                    var Instructions = (Instructions)this.Paginas.Where(item => item.Nombre.Equals(pageName)).First();
                    this.FramePrincipal.Content = Instructions;
                    break;
                case "SelectedService":
                    var SelectedService = (SelectedService)this.Paginas.Where(item => item.Nombre.Equals(pageName)).First();
                    this.FramePrincipal.Content = SelectedService;
                    SelectedService.iniciarTimer();
                    break;
                case "ResumeService":
                    var resumeService =(ResumeService)this.Paginas.Where(item=>item.Nombre.Equals(pageName)).First();
                    resumeService.capturarDatos();
                    resumeService.iniciarTimer();
                    this.FramePrincipal.Content = resumeService;
                    break;

            }
        }

        private void BarraControl_VisualizacionNormal(object sender, RoutedEventArgs e) 
        {
            try
            {
                Globales.Logger.Debug("Aplicando apariencia base");
                this.PanelApariencia.Background = Util.ObtenerFondo("./IMG/home-2.jpg");
                this.PanelDesarrollado.Background = Util.ObtenerFondo("./IMG/home-3.jpg");
                foreach (var pagina in this.Paginas)
                {
                    var item = pagina.GetType().GetMethod("EstablecerEstadoBase");
                    item.Invoke(pagina, new object[] { });
                }
            }
            catch (Exception ex )
            {
                Globales.Logger.Error(ex.InnerException,"error  Aplicando apariencia base");
            }
        }

        private void BarraControl_VisualizacionAltoContraste(object sender, RoutedEventArgs e)
        {
            try
            {
                Globales.Logger.Debug("Aplicando apariencia AltoContraste");
                this.PanelApariencia.Background = Util.ObtenerFondo("./IMG/HOME-2-AC.jpg");
                this.PanelDesarrollado.Background = Util.ObtenerFondo("./IMG/HOME-3-AC.jpg");
                foreach (var pagina in this.Paginas)
                {
                    var item = pagina.GetType().GetMethod("EstablecerAltoContraste");
                    item.Invoke(pagina, new object[] { });
                    
                }
            }
            catch (Exception ex )
            {

                Globales.Logger.Error( ex.InnerException, " error Aplicando apariencia base");
            }
        }
        private void BarraControl_VisualizacionBlackTouch(object sender, RoutedEventArgs e)
        {
            try
            {
                Globales.Logger.Debug("Aplicando apariencia BlackTouch");
                this.PanelApariencia.Background = Util.ObtenerFondo("./IMG/1-INTER-BT.jpg");
                this.PanelDesarrollado.Background = Util.ObtenerFondo("./IMG/1-INFERIOR_BT.jpg");
                foreach (var pagina in this.Paginas)
                {
                    var item = pagina.GetType().GetMethod("EstablecerBlackTouch");
                    item.Invoke(pagina, new object[] { });
                }
            }
            catch (Exception ex )
            {

                Globales.Logger.Error(ex.InnerException, "Aplicando apariencia base");
            }
        }

       
    }
}
