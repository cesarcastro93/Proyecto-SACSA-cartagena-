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
using SACSA.Resourses;

namespace SACSA.Views
{
    /// <summary>
    /// Lógica de interacción para VentanaModal.xaml
    /// </summary>
    public partial class VentanaModal : Window
    {
        Shutdown OpcApagado = new Shutdown();
       
        public VentanaModal()
        {
            InitializeComponent();

        }

        public void MostrarModal() 
        {
            VentanaModal modal = new VentanaModal();         
            modal.ShowDialog();
        }

        private void ApagarButtonClick(object sender, RoutedEventArgs e) 
        {
            try
            {
                OpcApagado.TurnOff();
                Globales.Logger.Debug("Apagando Sistema");
            }
            catch (Exception ex)
            {
                Globales.Logger.Error(ex, "Error Apagando Sistema");
                Console.WriteLine(ex.ToString());
                
            }
        }

        private void ReiniciarButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpcApagado.ResetSystem();
                Globales.Logger.Debug("Reiniciando Sistema");
            }
            catch (Exception ex)
            {
                Globales.Logger.Error(ex, "error Reiniciando Sistema");
                Console.WriteLine(ex.ToString());
                
            }
        }
    }
}
