using Aspose.Words;
using Aspose.Words.Saving;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Acciona.Credito
{
    public partial class Contratos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string fileName = "CONTRATO DE LEASING.docx";
            string filePath = Server.MapPath("~/CONTRATO DE LEASING.docx");
            //string strFileName = Server.MapPath("Pagos\\" + "CONTRATO DE LEASING.docx");

            ////Loading word document to HTML editor
            LoadDoc(filePath);
        }

        //Function to convert word document to HTML document after that loading into HTML editor
        private void LoadDoc(string strFileName)
        {
            //Loading  doc file using Document class of Aspose DLL
            Document doc = new Document(strFileName);

            //SaveOptions for image which is present in Word document
            HtmlSaveOptions options = new HtmlSaveOptions(SaveFormat.Html);
            string strImagePath = Server.MapPath("Pagos\\");

            //Location to save images which is included in word document
            options.ImagesFolder = strImagePath;
            options.ImagesFolderAlias = "Pagos\\";

            //Setting SaveFormat to save as HTML document
            options.SaveFormat = SaveFormat.Html;

            //Saving  file as HTML document
            doc.Save(strFileName + ".html", options);

            //Reading converted HTML file in Editor 
            StreamReader sr = new StreamReader(strFileName + ".html");
            string strValue = sr.ReadToEnd();
            docArea.Value = strValue;
            sr.Close();
            sr.Dispose();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Getting Text of HTML editor and writing into memorystream class object
            MemoryStream storeStream = new MemoryStream();
            StreamWriter sw = new StreamWriter(storeStream);
            sw.Write(docArea.Value);
            sw.Flush();

            //Again saving edited document with same name
            string strFileName = Server.MapPath("Pagos\\" + "CONTRATO DE LEASING.docx");
            Document doc = new Document(storeStream);
            doc.Save(strFileName, SaveFormat.Doc);
            storeStream.Close();
            storeStream.Dispose();
            sw.Close();
            sw.Dispose();
        }
    }
}