using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vt_Controles_WPF_NetFr.Componentes.Botones;
using Vt_Controles_WPF_NetFr.Componentes.Cargando;
using Vt_Controles_WPF_NetFr.ISevicio;
using Vt_Controles_WPF_NetFr.Modelo;
using Vt_Controles_WPF_NetFr.Utilitario;
using SACSA.Resourses;
using SACSA.Views;
using SACSA.Impresion;
using System.Runtime.CompilerServices;
using System.Data;
using SACSA.Controller;
using System.Globalization;

namespace SACSA.Views
{
    /// <summary>
    /// Lógica de interacción para ResumeService.xaml
    /// </summary>
    public partial class ResumeService : Page, IServiciosBlackTouch, INotifyPropertyChanged
    {

        public string Nombre { get; set; } = "ResumeService";
        public Action<string> FuntionToRedirect { get; set; }
        public TimerPage timer { get; set; }
        public SelectedService SelectedService { get; set; }
        DataTable DTObtenerSectores = new DataTable();
        Controller.ConsumoWS Consumo = new Controller.ConsumoWS(); 

        public ResumeService(Action<string> funtionToRedirect)
        {
            InitializeComponent();
            InicializarEstilos();
            int timerTime = Properties.Settings.Default.InactivityTimer;
            this.timer = new TimerPage(this.TimerRedireccionar);
            this.FuntionToRedirect = funtionToRedirect;
            this.DataContext = this;
            this.Background = Util.ObtenerFondo("./IMG/Resumen.jpg");
            this.btnImprimir.EstablecerEstadoBase();
            this.btnVolver.EstablecerEstadoBase();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InicializarEstilos()
        {
            this.Background = Util.ObtenerFondo("./IMG/Resumen.jpg");
            this.btnImprimir.EstablecerEstadoBase();
            this.btnVolver.EstablecerEstadoBase();
 
        }

        public void EstablecerEstadoBase()
        {
            try
            {
                Globales.Logger.Debug("Estableciendo Estado base en Resumen del servicio");
                this.Background = Util.ObtenerFondo("./IMG/Resumen.jpg");
                this.btnImprimir.EstablecerEstadoBase();
                this.btnVolver.EstablecerEstadoBase();
                this.lbl_destino.Foreground = new SolidColorBrush(Colors.White);
                this.lbl_Fecha.Foreground = new SolidColorBrush(Colors.White);
                this.lbl_hora.Foreground = new SolidColorBrush(Colors.White);
                this.lbl_ValorAPagar.Foreground = new SolidColorBrush(Colors.White);
            }
            catch (Exception ex )
            {
                Globales.Logger.Error(ex.InnerException,"error Estableciendo Estado base en Resumen del servicio");
            }
        }

        public void EstablecerAltoContraste()
        {
            try
            {
                Globales.Logger.Debug("Estableciendo AltoContraste en Resumen del servicio");
                this.Background = Util.ObtenerFondo("./IMG/Resumen-AC.jpg");
                this.btnImprimir.EstablecerAltoContraste();
                this.btnVolver.EstablecerAltoContraste();
                this.lbl_destino.Foreground = new SolidColorBrush(Colors.Cyan);
                this.lbl_Fecha.Foreground = new SolidColorBrush(Colors.Cyan);
                this.lbl_hora.Foreground = new SolidColorBrush(Colors.Cyan);
                this.lbl_ValorAPagar.Foreground = new SolidColorBrush(Colors.Cyan);
            }
            catch (Exception ex)
            {
                Globales.Logger.Error(ex.InnerException, "error Estableciendo AltoContraste en Resumen del servicio");
            }
        }

        public void EstablecerBlancoYNegro()
        {
            
        }

        public void EstablecerBlackTouch()
        {
            try
            {
                Globales.Logger.Debug("Estableciendo BlackTouch en Resumen del servicio");
                this.Background = Util.ObtenerFondo("./IMG/1S_BT.jpg");
                this.btnImprimir.EstablecerBlackTouch();
                this.btnVolver.EstablecerBlackTouch();
                this.lbl_destino.Foreground = new SolidColorBrush(Color.FromRgb(174, 170, 169));
                this.lbl_Fecha.Foreground = new SolidColorBrush(Color.FromRgb(174, 170, 169));
                this.lbl_hora.Foreground = new SolidColorBrush(Color.FromRgb(174, 170, 169));
                this.lbl_ValorAPagar.Foreground = new SolidColorBrush(Color.FromRgb(174, 170, 169));
            }
            catch (Exception ex)
            {
                Globales.Logger.Error(ex.InnerException, "error Estableciendo BlackTouch en Resumen del servicio");
            }
        }

        private void btnImprimir_ComponenteClick(object sender, RoutedEventArgs e)
        {
            RegistrarTicket rt = new RegistrarTicket();
            canvasDeImpresion recibo = new canvasDeImpresion();
           

            try
            {
                Globales.Logger.Debug("Armando impresion,enviando a impresora y registrando en base de datos");
                recibo.Left = 3000;
                recibo.ShowInTaskbar = false;
                recibo.ArmaImpresion(MainWindow.Idioma);
                recibo.Show();
                recibo.GuardarReciboPdf();
                PrintDialog print = new PrintDialog();
                print.PrintVisual(recibo.CanvasImpresion, "Impresion");
            }
            catch (RuntimeWrappedException ex)
            {
                Console.WriteLine("error " + ex.Message);  
            }
            catch (Exception ex)
            {
                recibo.Close();
                Globales.Logger.Error(ex.InnerException,"error al realizar el proceso de impresion");
            }
           
            this.FuntionToRedirect("Index");
            rt.RegistrarTicketEnBD();

        }

        public void capturarDatos() 
        {
            try
            {
                Globales.Logger.Debug("Capturando datos del servicio y mostrando en pantalla");
                CultureInfo InfoPais = CultureInfo.CreateSpecificCulture("es-CO");
                InfoPais.NumberFormat.CurrencyDecimalDigits = 0;
                lbl_destino.Content = MainWindow.InfoDestino[0];
                lbl_ValorAPagar.Content = Convert.ToInt32(MainWindow.InfoDestino[1]).ToString("C", MainWindow.InfoPais);
                lbl_Fecha.Content = DateTime.Now.ToString("dd/MM/yyyy");
                lbl_hora.Content = DateTime.Now.ToString("hh:mm:ss tt");
            }
            catch (Exception ex)
            {
                Globales.Logger.Error(ex.InnerException, "Error Capturando datos del servicio y mostrando en pantalla ");
            }
        }

        private void btnVolver_ComponenteClick(object sender, RoutedEventArgs e)
        {
            Globales.Logger.Debug("Se oprimio el boton atras en resumen servicio, redireccionando al inicio");
            this.FuntionToRedirect("Index");    
        }

        public void TimerRedireccionar(string message = "")
        {
            Globales.Logger.Debug("TimeOut redirigiendo al inicio.");
            this.timer.DetenerTiempo();
            this.FuntionToRedirect("Index");
           
        }

        public void iniciarTimer()
        {
            this.timer.IniciarTiempo(); 
        }
       
    }
}
