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
using SisSegLT.Negocio;
using SisSegLT.Seguridad;

namespace AsodenicSR.Import
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPass.Text))
            {
                try
                {
                    Usuario usuario = Consulta.ValidarCredenciales(txtUser.Text, txtPass.Text);
                    if (usuario != null)
                    {
                        ItemMenu itemMenu = new ItemMenuBLL().ObtenerPorRuta(AsodenicSR.Import.Properties.Settings.Default.ItemMenuNombre);
                        if (itemMenu != null)
                        {
                            if (itemMenu.ItemRol.Where(r => r.IdRol.Equals(usuario.IdRol) && (r.EsActivo)).Count() > 0)
                            {
                                General.UsuarioAutenticado = usuario;
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            }
                            else
                            {
                                lblMensaje.Text = "No tiene permiso para acceder";
                            }
                        }
                        else
                        {
                            lblMensaje.Text = "La aplicación no tiene permiso";
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "No se ha encontrado el usuario indicado";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
