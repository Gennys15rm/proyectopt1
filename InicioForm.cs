using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoMatematicoWinForms
{
    public class InicioForm : Form
    {
        private TextBox txtJugador1;
        private TextBox txtJugador2;
        private RadioButton rbIndividual;
        private RadioButton rbDosJugadores;
        private Button btnIniciar;

        public InicioForm()
        {
            Text = "Proyecto Matemático - Configuración";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(600, 430);
            MinimumSize = new Size(600, 430);

            CrearInterfaz();
        }

        private void CrearInterfaz()
        {
            Label titulo = new Label();
            titulo.Text = "PROYECTO MATEMÁTICO";
            titulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(145, 35);
            Controls.Add(titulo);

            Label subtitulo = new Label();
            subtitulo.Text = "Evolución del sistema a entorno gráfico";
            subtitulo.Font = new Font("Segoe UI", 11);
            subtitulo.AutoSize = true;
            subtitulo.Location = new Point(175, 78);
            Controls.Add(subtitulo);

            Label lblJugador1 = new Label();
            lblJugador1.Text = "Nombre del jugador 1:";
            lblJugador1.AutoSize = true;
            lblJugador1.Location = new Point(70, 135);
            Controls.Add(lblJugador1);

            txtJugador1 = new TextBox();
            txtJugador1.Location = new Point(240, 130);
            txtJugador1.Width = 260;
            Controls.Add(txtJugador1);

            rbIndividual = new RadioButton();
            rbIndividual.Text = "Modo individual";
            rbIndividual.AutoSize = true;
            rbIndividual.Location = new Point(70, 185);
            rbIndividual.Checked = true;
            Controls.Add(rbIndividual);

            rbDosJugadores = new RadioButton();
            rbDosJugadores.Text = "Modo dos jugadores";
            rbDosJugadores.AutoSize = true;
            rbDosJugadores.Location = new Point(250, 185);
            rbDosJugadores.CheckedChanged += RbDosJugadores_CheckedChanged;
            Controls.Add(rbDosJugadores);

            Label lblJugador2 = new Label();
            lblJugador2.Text = "Nombre del jugador 2:";
            lblJugador2.AutoSize = true;
            lblJugador2.Location = new Point(70, 235);
            Controls.Add(lblJugador2);

            txtJugador2 = new TextBox();
            txtJugador2.Location = new Point(240, 230);
            txtJugador2.Width = 260;
            txtJugador2.Enabled = false;
            Controls.Add(txtJugador2);

            Label informacion = new Label();
            informacion.Text = "Cada jugador comienza con 3 vidas. La dificultad aumenta\n" +
                               "según el progreso y el puntaje obtenido.";
            informacion.AutoSize = true;
            informacion.Location = new Point(70, 280);
            informacion.Font = new Font("Segoe UI", 10);
            Controls.Add(informacion);

            btnIniciar = new Button();
            btnIniciar.Text = "INICIAR JUEGO";
            btnIniciar.Location = new Point(210, 335);
            btnIniciar.Size = new Size(180, 40);
            btnIniciar.Click += BtnIniciar_Click;
            Controls.Add(btnIniciar);

            AcceptButton = btnIniciar;
        }

        private void RbDosJugadores_CheckedChanged(object sender, EventArgs e)
        {
            txtJugador2.Enabled = rbDosJugadores.Checked;
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            string jugador1 = txtJugador1.Text.Trim();
            string jugador2 = txtJugador2.Text.Trim();

            if (string.IsNullOrWhiteSpace(jugador1))
            {
                MessageBox.Show(
                    "Ingrese el nombre del jugador 1.",
                    "Dato requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtJugador1.Focus();
                return;
            }

            if (rbDosJugadores.Checked && string.IsNullOrWhiteSpace(jugador2))
            {
                MessageBox.Show(
                    "Ingrese el nombre del jugador 2.",
                    "Dato requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtJugador2.Focus();
                return;
            }

            bool dosJugadores = rbDosJugadores.Checked;

            Hide();

            using (JuegoForm juego = new JuegoForm(jugador1, jugador2, dosJugadores))
            {
                juego.ShowDialog();
            }

            Close();
        }
    }
}
