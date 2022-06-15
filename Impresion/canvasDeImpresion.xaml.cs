using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using SACSA.Views;
using SACSA.TarifasSacsa;
using System.Data;
using Vt_Controles_WPF_NetFr.Utilitario;

namespace SACSA.Impresion
{
    /// <summary>
    /// Lógica de interacción para canvasDeImpresion.xaml
    /// </summary>
    public partial class canvasDeImpresion : Window
    {
        WSTarifas wsTarifas = new WSTarifas();
        DataSet DSIdiomas = new DataSet();
        DataTable dtIdioma = new DataTable();
        CultureInfo infoPais = new CultureInfo("es-CO");
        String Consecutivo = "";
        public string NombreArchivo { get; set; }
        
        public canvasDeImpresion()
        {
            InitializeComponent();
            
        }
     
        public void ArmaImpresion(int cualidioma)
        {
            Consecutivo = wsTarifas.ObtenerConsecutivo();
            BitmapImage bi = Util.EstablecerImagen("./IMG/LogoTicket2.png");
            DSIdiomas = wsTarifas.ObtenerLabelIdiomas(cualidioma);
            dtIdioma = DSIdiomas.Tables[0];
            Ima_Logo.Source = bi;
            Lbl_Bienvenido.Content = DSIdiomas.Tables[0].Rows[5][1].ToString();
            Lbl_Consecutivo.Content = DSIdiomas.Tables[0].Rows[6][1].ToString(); 
            Lbl_NumConsecutivo.Content = Consecutivo;
            Lbl_advertencia.Content = DSIdiomas.Tables[0].Rows[7][1].ToString();
            Lbl_Pbx.Content = DSIdiomas.Tables[0].Rows[8][1].ToString();
            Lbl_Nit.Content = DSIdiomas.Tables[0].Rows[9][1].ToString();
            Lbl_Advertencia2.Content = DSIdiomas.Tables[0].Rows[16][1].ToString();
            Lbl_Destino.Content = DSIdiomas.Tables[0].Rows[2][1].ToString();
            Lbl_DestinoServicio.Content = MainWindow.InfoDestino[0];
            Lbl_Fecha.Content = DSIdiomas.Tables[0].Rows[0][1].ToString();
            Lbl_FechaServicio.Content = DateTime.Now.ToString(infoPais); ;
            Lbl_CostoApagar.Content = DSIdiomas.Tables[0].Rows[3][1].ToString();
            Lbl_CostoApagarServicio.Content = Convert.ToInt32(MainWindow.InfoDestino[1]).ToString("C", MainWindow.InfoPais);
            Lbl_Advertencia3.Content = DSIdiomas.Tables[0].Rows[10][1].ToString();
            Lbl_Advertencia4.Content = DSIdiomas.Tables[0].Rows[17][1].ToString();
            Lbl_Advertencia6.Content = DSIdiomas.Tables[0].Rows[18][1].ToString();
            Lbl_Advertencia7.Content = DSIdiomas.Tables[0].Rows[19][1].ToString();
            Lbl_Pagina.Content = DSIdiomas.Tables[0].Rows[20][1].ToString();
            Thread.Sleep(200);
        }

       

        //public void Contador()
        //{

        //    String line = "";
        //    int contador = 0;
        //    String FechaEvaluar = "";
        //    String[] lines;

        //    try
        //    {
        //        StreamReader sr = new StreamReader(@"Contador.txt");
        //        line = sr.ReadLine();
        //        lines = File.ReadAllLines(@"Contador.txt");

        //        contador = Convert.ToInt32(line);
        //        FechaEvaluar = lines[1].ToString();
        //        sr.Close();
        //        if (FechaEvaluar == DateTime.Now.ToString("yyyy/MM/dd"))
        //        {
        //            contador = contador + 1;
        //            StreamWriter sw = new StreamWriter(@"Contador.txt");
        //            sw.WriteLine(contador.ToString());
        //            sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd"));
        //            sw.Close();
        //            LNoCIn.Content = contador.ToString();
        //        }
        //        else
        //        {
        //            contador = 1;
        //            StreamWriter sw = new StreamWriter(@"Contador.txt");
        //            sw.WriteLine(contador.ToString());
        //            sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd"));
        //            sw.Close();
        //            LNoCIn.Content = contador.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Consumo.Logger.Error(ex, "Creando y guardando el consecutivo");
        //    }

        //}


        public void GuardarReciboPdf()
        {
            try
            {
                MemoryStream lMemoryStream = new MemoryStream();
                Package package = Package.Open(lMemoryStream, FileMode.Create);
                XpsDocument doc = new XpsDocument(package);
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
                writer.Write(CanvasImpresion);
                doc.Close();
                package.Close();

            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
      
        }

        //public void GuardarCopiaReciboXps()
        //{
        //    try
        //    {
        //        XpsDocument doc = new XpsDocument(NombreArchivo + ".xps", FileAccess.Write);
        //        XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

        //        writer.Write(CanvasImpresion);
        //        doc.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        //Consumo.Logger.Error(ex, "Guardando Copia Recibo");
                
        //    }
        //}

        #region Otros Metodos
        //public void leeparametrosimpre()
        //{
            
        //    try
        //    {
          
        //         for (int i = 0; i < contexto.getparams().dsparams.tables[j].rows.count; i++)
        //         {
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulocomprobante")
        //                 vlcomprobante = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulototalpago")
        //                 vltotalpago = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulocambio")
        //                 vlcambio = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulorecuado")
        //                 vlrecaudo = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulofactura")
        //                 vlfactura = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulopagofactura")
        //                 vlpago = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulotransaccionexitosa")
        //                 vlestadotransaccion = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulotransacciondeclinada")
        //                 vlestadotransacciond = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "tfoot5")
        //                 vltextf5 = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();

        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulocodigodelbanco")
        //                 vlcodigobancovalor = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulorecibo")
        //                 vlrecibo = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulocodigounico")
        //                 vlcodigounico = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "tituloaprobado")
        //                 vlaprobado = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "tituloterminal")
        //                 vlterminal = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "corresponsal")
        //                 vlcorresponsal = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "nombrealmacen")
        //                 vlnombrealmacen = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulocodigoservicio")
        //                 vlcodservicio = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "direccionalmacen")
        //                 vldireccion = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             //if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "titulorrn")
        //             //    this.vlrrn = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "tfoot1")
        //                 vltextf1 = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "tfoot2")
        //                 vltextf2 = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "tfoot3")
        //                 vltextf3 = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //             if (contexto.getparams().dsparams.tables[j].rows[i]["parametro"].tostring().toupper() == "tfoot4")
        //                 vltextf4 = contexto.getparams().dsparams.tables[j].rows[i]["valor"].tostring();
        //         }
        //     }
        //    catch (Exception ex)
        //    {
        //        //consumo.logger.error(ex, "leyendo parámetros de impresión factura");
                
        //    }
            #endregion

    }
}

