using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsodenicSR.Import
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Principal());
        }

        ///// <summary>
        ///// Punto de entrada con parametros para la aplicación.
        ///// </summary>
        //static void Main(string[] args)
        //{
        //    if (args.Count() == 2)
        //    {
        //        switch (args[0].ToLower())
        //        {
        //            case "itemmenunombre":
        //                AsodenicSR.Import.Properties.Settings.Default.ItemMenuNombre = args[1];
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}
    }
}

