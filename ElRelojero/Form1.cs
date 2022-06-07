using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElRelojero
{
    public partial class Form1 : Form
    {
        Temporizador tmp;
        public Form1()
        {
            InitializeComponent();
            //timer.Start();  ya no lo uso, utilizo mi evento
            tmp = new Temporizador(1000);
            tmp.TiempoCumplido += AsignarHora;
        }

        public void AsignarHora()  //manejador
        {
            //this.lbl_hora.Text = $"{DateTime.Now}"; este usaba antes
            if (lbl_hora.InvokeRequired)
            {
                Action delegadoAsignarHora = AsignarHora;
                lbl_hora.Invoke(delegadoAsignarHora);
            }
            else
            {
                lbl_hora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            AsignarHora();  //este es cuando uso el timer
        }

        private void btn_iniciar_Click(object sender, EventArgs e)
        {
            tmp.IniciarTemporizador();
        }

        private void btn_detener_Click(object sender, EventArgs e)
        {
            tmp.DetenerTemporizador();
        }
    }
}
