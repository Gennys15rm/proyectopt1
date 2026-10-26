using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoMatematicoWinForms
{
    public class ResultadosForm : Form
    {
        public ResultadosForm(string[] nombres, int[] puntajes, int cantidad)
        {
            Text = "Proyecto Matemático - Resultados";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(600, 430);

            CrearInterfaz(nombres, puntajes, cantidad);
        }

        private void CrearInterfaz(string[] nombres, int[] puntajes, int cantidad)
        {
            Label titulo = new Label();
            titulo.Text = "RESULTADOS Y RANKING";
            titulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(145, 35);
            Controls.Add(titulo);

            Label explicacion = new Label();
            explicacion.Text = "Ranking de la sesión, ordenado por puntaje:";
            explicacion.AutoSize = true;
            explicacion.Location = new Point(155, 85);
            Controls.Add(explicacion);

            ListBox lista = new ListBox();
            lista.Location = new Point(120, 125);
            lista.Size = new Size(360, 150);
            lista.Font = new Font("Segoe UI", 12);

            for (int i = 0; i < cantidad; i++)
            {
                lista.Items.Add(
                    (i + 1) + ". " +
                    nombres[i] + " - " +
                    puntajes[i] + " puntos");
            }

            Controls.Add(lista);

            if (cantidad == 2)
            {
                string mensaje;

                if (puntajes[0] == puntajes[1])
                {
                    mensaje = "Resultado: EMPATE";
                }
                else
                {
                    mensaje = "Ganador: " + nombres[0];
                }

                Label ganador = new Label();
                ganador.Text = mensaje;
                ganador.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                ganador.AutoSize = true;
                ganador.Location = new Point(210, 295);
                Controls.Add(ganador);
            }

            Button cerrar = new Button();
            cerrar.Text = "Cerrar";
            cerrar.Location = new Point(230, 345);
            cerrar.Size = new Size(140, 38);
            cerrar.Click += delegate
            {
                Close();
            };
            Controls.Add(cerrar);
        }
    }
}
