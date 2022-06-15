using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vt_Controles_WPF_NetFr.Componentes.CampoTexto;
using Vt_Controles_WPF_NetFr.Componentes.Lista;
using Vt_Controles_WPF_NetFr.Componentes.Teclado.Numerico;
using Vt_Controles_WPF_NetFr.ISevicio;
using Vt_Controles_WPF_NetFr.Utilitario;
using SACSA.Resourses;

namespace SACSA.Views
{
    /// <summary>
    /// Lógica de interacción para SelectedService.xaml
    /// </summary>
    public partial class SelectedService : Page, IServiciosBlackTouch, INotifyPropertyChanged
    {

        DataTable DTObtenerSectores = new DataTable();
        Controller.ConsumoWS Consumo = new Controller.ConsumoWS();
        VentanaModal modal = new VentanaModal();


        public string Nombre { get; set; } = "SelectedService";
        public Action<string> FuntionToRedirect { get; set; }
        private PropiedadesTeclado propiedadesTecladoBase { get; set; } = new PropiedadesTeclado();
        private PropiedadesTeclado PropiedadesTecladoAltoContraste { get; set; } = new PropiedadesTeclado();
        private PropiedadesCampoDeTexto CampoTextoEstadoAltoContraste { get; set; } = new PropiedadesCampoDeTexto();
        private PropiedadesLista PropiedadesListaEstadoBase { get; set; } = new PropiedadesLista();
        public string textoaBuscar { get; set; }
        public TimerPage TimerPage { get; set; }    
        public SelectedService(Action<string> funtionToRedirect)
        {

            InitializeComponent();
            InicializarEstilos();
            int timerTime = Properties.Settings.Default.InactivityTimer;
            this.TimerPage = new TimerPage(this.TimerRedireccionar);
            this.DataContext = this;
            this.FuntionToRedirect = funtionToRedirect;
            this.Background = Util.ObtenerFondo("./IMG/Destino.jpg");
            this.ListaAutocompletar.ListaDestinos.Foreground = new SolidColorBrush(Color.FromRgb(0, 66, 92));
            this.ListaAutocompletar.ListaDestinos.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 66, 92));
            CampoSeleccion.TextChanged += CampoBusqueda_TextChanged;
            Consumo.MostrarDestinos();
            ListaAutocompletar.ListaDestinos.ItemsSource = Consumo.DTobtenerSectores.AsDataView();
            ListaAutocompletar.ListaDestinos.DisplayMemberPath = "NOMBRE";
            ListaAutocompletar.ClickItemChanged += ListaAutocompletar_ClickItemChanged;
        }

        private void ListaAutocompletar_ClickItemChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListaAutocompletar.ListaDestinos.SelectedItem == null)
                    return;

                DataRowView d1 = ListaAutocompletar.ListaDestinos.SelectedItem as DataRowView;

                if (d1 != null)
                {

                    MainWindow.InfoDestino[0] = d1["NOMBRE"].ToString();
                    MainWindow.InfoDestino[1] = d1["COSTO_TARIFA"].ToString();
                    MainWindow.InfoDestino[2] = d1["CODIGO"].ToString();
                    MainWindow.InfoDestino[3] = d1["ZONA"].ToString();
                    this.FuntionToRedirect("ResumeService");
                    Globales.Logger.Debug("Datos del servicio : " + "Nombre Destino: " + " " + MainWindow.InfoDestino[0] + " " + "Costo tarifa: $" + MainWindow.InfoDestino[1] + "  " + "Codigo Lugar: " + " " + MainWindow.InfoDestino[2] + " " + "Zona: " + " " + MainWindow.InfoDestino[3] + ", " + "Capturados. Redireccionando A resumen del servicio");

                }
                else
                {
                    System.Windows.MessageBox.Show("Por favor escriba un lugar de destino");
                    Globales.Logger.Error("Error cargando la informacion y al redirigir pagina ");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CampoBusqueda_TextChanged(object sender, RoutedEventArgs e)
        {

            MostrarLista();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region InicializarEstilos
        public void InicializarEstilos()
        {
            try
            {
                this.propiedadesTecladoBase.AnchoTecla = 80;
                this.propiedadesTecladoBase.AltoTecla = 80;
                this.propiedadesTecladoBase.MargenTecla = 5;
                this.propiedadesTecladoBase.ColorBorde = "#00425C";
                this.propiedadesTecladoBase.ColorFuente = "#00425C";
                this.ListaAutocompletar.ListaDestinos.FontSize = 25;


                Teclado.EstadoBase = this.propiedadesTecladoBase;
                ListaAutocompletar.EstadoBase = this.PropiedadesListaEstadoBase;
                this.ListaAutocompletar.Visibility = Visibility.Hidden;


                /*Propiedades Estado AltoContraste*/
                this.CampoTextoEstadoAltoContraste.ColorBorde = "#00ffff";
                this.CampoTextoEstadoAltoContraste.ColorFondo = "#000000";
                this.CampoTextoEstadoAltoContraste.ColorFuente = "#00ffff";
                this.CampoSeleccion.EstadoAltoContraste = this.CampoTextoEstadoAltoContraste;


                this.PropiedadesTecladoAltoContraste.ColorBorde = "#00ffff";
                this.PropiedadesTecladoAltoContraste.ColorFondo = "#000000";
                this.PropiedadesTecladoAltoContraste.ColorFuente = "#00ffff";
                this.PropiedadesTecladoAltoContraste.AnchoTecla = 80;
                this.PropiedadesTecladoAltoContraste.AltoTecla = 80;


                var estilosBlackTouchCampoTexto = new PropiedadesCampoDeTexto();
                var estilosBlackTouchCampoLista = new PropiedadesLista();

                estilosBlackTouchCampoTexto.ColorBorde = "#AEAAA9";
                estilosBlackTouchCampoTexto.ColorFondo = "#000000";
                estilosBlackTouchCampoTexto.ColorFuente = "#AEAAA9";
                this.CampoSeleccion.EstadoBlackTouch = estilosBlackTouchCampoTexto;

                estilosBlackTouchCampoLista.ColorBorde = "#AEAAA9";
                estilosBlackTouchCampoLista.ColorFondo = "#000000";
                estilosBlackTouchCampoLista.ColorFuente = "#AEAAA9";
                this.ListaAutocompletar.EstadoBlackTouch = estilosBlackTouchCampoLista;

                var estilosBlancoYNegroTeclado = new PropiedadesTeclado();
                estilosBlancoYNegroTeclado.ColorBorde = "#000000";
                estilosBlancoYNegroTeclado.ColorFondo = "#FFFFFF";
                estilosBlancoYNegroTeclado.ColorFuente = "#000000";
                estilosBlancoYNegroTeclado.AnchoTecla = 80;
                estilosBlancoYNegroTeclado.AltoTecla = 80;
                estilosBlancoYNegroTeclado.NoDifuminarColor();
                estilosBlancoYNegroTeclado.SinEstilosDeClic();
                this.Teclado.EstadoBlancoYNegro = estilosBlancoYNegroTeclado;

                var estilosBlackTouch = new PropiedadesTeclado();
                estilosBlackTouch.ColorBorde = "#5D6D7E";
                estilosBlackTouch.ColorFondo = "#000000";
                estilosBlackTouch.ColorFuente = "#5D6D7E";
                estilosBlackTouch.AnchoTecla = 80;
                estilosBlackTouch.AltoTecla = 80;
                estilosBlackTouch.NoDifuminarColor();
                estilosBlackTouch.SinEstilosDeClic();
                this.Teclado.EstadoBlackTouch = estilosBlackTouch;

                this.PropiedadesTecladoAltoContraste.NoDifuminarColor();
                this.PropiedadesTecladoAltoContraste.SinEstilosDeClic();
                this.Teclado.EstadoAltoContraste = this.PropiedadesTecladoAltoContraste;
                this.Teclado.EstablecerEstadoBase();
                this.ListaAutocompletar.EstablecerEstadoBase();
                this.btnAtras.EstablecerEstadoBase();
                /*Fin Propiedades Estado AltoContraste*/
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
        #endregion

        #region Estableces Estados 
        public void EstablecerEstadoBase()
        {
            try
            {
                Globales.Logger.Debug("Estableciendo estado base en seleccion servicio");
                this.Background = Util.ObtenerFondo("./IMG/Destino.jpg");
                this.Teclado.EstablecerEstadoBase();
                this.CampoSeleccion.EstablecerEstadoBase();
                this.btnAtras.EstablecerEstadoBase();
                this.ListaAutocompletar.ListaDestinos.Background = new SolidColorBrush(Colors.White);
                this.ListaAutocompletar.ListaDestinos.Foreground = new SolidColorBrush(Color.FromRgb(0, 66, 92));
                this.ListaAutocompletar.ListaDestinos.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 66, 92));

            }
            catch (Exception ex)
            {

                Globales.Logger.Error(ex.InnerException, "error al establecer estado base en seleccion servicio");
            }
        }

        public void EstablecerAltoContraste()
        {
            try
            {
                Globales.Logger.Debug("Estableciendo AltoContraste en seleccion servicio");
                this.Background = Util.ObtenerFondo("./IMG/Destino-AC.jpg");
                this.Teclado.EstablecerAltoContraste();
                this.CampoSeleccion.EstablecerAltoContraste();
                this.btnAtras.EstablecerAltoContraste();
                this.ListaAutocompletar.ListaDestinos.Background = new SolidColorBrush(Colors.Black);
                this.ListaAutocompletar.ListaDestinos.Foreground = new SolidColorBrush(Colors.Cyan);
                this.ListaAutocompletar.ListaDestinos.BorderBrush = new SolidColorBrush(Colors.Cyan);

            }
            catch (Exception ex)
            {

                Globales.Logger.Error(ex.InnerException, "error al establecer AltoContraste en seleccion servicio");
            }
        }

        public void EstablecerBlancoYNegro()
        {

        }

        public void EstablecerBlackTouch()
        {
            try
            {
                Globales.Logger.Debug("Estableciendo BlackTouch en seleccion servicio");
                this.Background = Util.ObtenerFondo("./IMG/1S_BT.jpg");
                this.Teclado.EstablecerBlackTouch();
                this.CampoSeleccion.EstablecerBlackTouch();
                this.btnAtras.EstablecerBlackTouch();
                this.ListaAutocompletar.ListaDestinos.Background = new SolidColorBrush(Colors.Black);
                this.ListaAutocompletar.ListaDestinos.Foreground = new SolidColorBrush(Color.FromRgb(174, 170, 169));
                this.ListaAutocompletar.ListaDestinos.BorderBrush = new SolidColorBrush(Color.FromRgb(174, 170, 169));
            }
            catch (Exception ex)
            {

                Globales.Logger.Error(ex.InnerException, "error al establecer AltoContraste en seleccion servicio");
            }
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string letra = Properties.Settings.Default.ToString();
            this.Teclado.EscribirEnCampo(this.CampoSeleccion.Txt_Campo);
        }


        private void btnAtras_ComponenteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Globales.Logger.Debug("Se oprimio el boton Atras en seleccion servicio");
                this.FuntionToRedirect("Index");
            }
            catch (Exception ex)
            {

                Globales.Logger.Error(ex, "error al tratar de redirigir a la pagina anterior");
            }
        }
        public void MostrarLista()
        {

            if (this.CampoSeleccion.Txt_Campo.Text.Length >= 1)
            {
                this.ListaAutocompletar.Visibility = Visibility.Visible;
                ListaAutocompletar.ListaDestinos.SelectedIndex = -1;
                ListaAutocompletar.ListaDestinos.ItemsSource = Consumo.FiltrarDestinos(CampoSeleccion.Txt_Campo.Text);
            }
            else
            {
                this.ListaAutocompletar.Visibility = Visibility.Hidden;
            }
            OpcionesDeApagado();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this.CampoSeleccion.Txt_Campo.Clear();
        }

        private void OpcionesDeApagado()
        {
            if (this.CampoSeleccion.Txt_Campo.Text == "SASACSASA")
            {
                Globales.Logger.Debug("Ingresando a apagado del sistema");
                modal.MostrarModal();
            }
        }

        public void TimerRedireccionar(string message = "")
        {
            Globales.Logger.Debug("TimeOut redirigiendo al inicio.");
            this.TimerPage.DetenerTiempo();
            this.FuntionToRedirect("Index");

        }

        public void iniciarTimer()
        {
            this.TimerPage.IniciarTiempo();
        }
    }
}
