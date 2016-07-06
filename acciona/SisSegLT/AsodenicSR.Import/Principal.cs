using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SisSegLT.Datos;

namespace AsodenicSR.Import
{
    public partial class Principal : Form
    {
        private const string TITULO_FORM = "Importador CSV []";

        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            if (new Login().ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Text = TITULO_FORM.Replace("[]","[" + General.UsuarioAutenticado.Login + "]");
            }
            else
            {
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog()
            {
                Title = "Abrir archivo delimitado por comas",
                Multiselect = false,
                Filter = "Archivo delimitado por comas (*.txt, *.csv, *.prn)|*.txt;*.csv;*.prn|Todos los archivos (*.*)|*.*",
                DefaultExt = "*.csv"
            };
            if (abrir.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] filas = System.IO.File.ReadAllLines(abrir.FileName, Encoding.Default);
                string[] separador = { "," };
                long cacheMax = 5000;
                string cacheTexto = "";
                int tipo = 0; //0 - nulo; 1 - DatosClienteImp; 2 - DetallePrestamosClienteImp
                string nombreArchivo = Application.StartupPath; //1 - DatosClienteImp; 2 - DetallePrestamosClienteImp
                string rutaXML = Application.StartupPath;

                //CORREGIR FORMATO DE FECHAS
                for (long i = 1; i < filas.Length; i++)
                {
                    string[] campos = filas[i].Split(separador, StringSplitOptions.None);
                    if (tipo == 0)
                    {
                        if (campos.Length == 7)
                        {
                            tipo = 2;
                            nombreArchivo = System.IO.Path.Combine(nombreArchivo, "ImportDetallePrestamosClienteImp.afv");
                            rutaXML = System.IO.Path.Combine(rutaXML, "FormatDetallePrestamosClienteImp.xml");
                        }
                        else
                        {
                            tipo = 1;
                            nombreArchivo = System.IO.Path.Combine(nombreArchivo, "ImportDatosClienteImp.afv");
                            rutaXML = System.IO.Path.Combine(rutaXML, "FormatDatosClienteImp.xml");
                        }

                        //Preparar el archivo
                        System.IO.File.WriteAllText(nombreArchivo, "", Encoding.Default);
                    }
                    //switch (tipo)
                    //{
                    //    case 1: //DatosClienteImp
                    //        campos[3] = cambiarFormatoFecha(campos[3]);
                    //        campos[5] = cambiarFormatoFecha(campos[5]);
                    //        campos[26] = cambiarFormatoFecha(campos[26]);
                    //        break;
                    //    case 2: //DetallePrestamosClienteImp
                    //        campos[4] = cambiarFormatoFecha(campos[4]);
                    //        campos[5] = cambiarFormatoFecha(campos[5]);
                    //        break;
                    //}
                    string filaCorregida = "";
                    for (int j = 0; j < campos.Length; j++)
                    {
                        filaCorregida += campos[j].Trim() + ",";
                    }
                    //Recorrer todas las filas en busca de duplicados
                    bool esDuplicado = false;
                    for (long f = 0; f < filas.Length; f++)
                    {
                        if (f != i)
                            if (filas[f].Contains(filaCorregida.Substring(0, filaCorregida.Length - 1)))
                            {
                                esDuplicado = true;
                                break;
                            }
                    }
                    if (!esDuplicado)
                        filas[i] = Guid.NewGuid() + "," + filaCorregida.Substring(0, filaCorregida.Length - 1);
                    else
                        filas[i] = "";
                }
                //ESCRIBIR ARCHIVO CORREGIDO
                for (long i = 1; i < filas.Length; i++)
                {
                    if (filas[i].Trim().Length > 0)
                    {
                        cacheTexto += filas[i] + "\n";
                    }

                    if ((cacheTexto.Length > cacheMax) || ((i + 1) == filas.Length))
                    {
                        //Escribir el contenido de la cache en el archivo de salida
                        System.IO.File.AppendAllText(nombreArchivo, cacheTexto, Encoding.Default);
                        //Limpiar a cache
                        cacheTexto = "";
                    }
                }
                //EJECTUAR SCRIPT PARA INSERTAR MASIVAMENTE LOS DATOS
                try
                {
                    new SisSegDB().SP_ImportacionMasiva(tipo.ToString(), nombreArchivo, rutaXML);
                    MessageBox.Show("Los registros se han insertado satisfactoriamente, además se han eliminado cualquier duplicado en las tablas existentes", "Hecho...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Se ha producido el siguiente error:\r\n" + ex.InnerException.Message, "Error al insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string cambiarFormatoFecha(string texto)    
        {
            return Convert.ToDateTime(texto.Substring(0, 4) + "-" + texto.Substring(4, 2) + "-" + texto.Substring(6, 2)).ToString("yyyy-MM-dd");
        }
    }
}
