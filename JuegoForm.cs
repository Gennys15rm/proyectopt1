using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoMatematicoWinForms
{
    public class JuegoForm : Form
    {
        // Arreglos paralelos para almacenar nombres y puntajes de la sesión.
        private string[] nombres = new string[10];
        private int[] puntajes = new int[10];
        private int cantidadRegistros = 0;

        private readonly bool dosJugadores;
        private readonly string[] jugadores = new string[2];
        private readonly JuegoLogica[] partidas = new JuegoLogica[2];
        private int turno = 0;

        private Label lblJugador;
        private Label lblNivel;
        private Label lblPuntaje;
        private Label lblVidas;
        private Label lblPregunta;
        private TextBox txtRespuesta;
        private Button btnResponder;
        private Button btnSalir;

        public JuegoForm(string jugador1, string jugador2, bool dosJugadores)
        {
            this.dosJugadores = dosJugadores;

            jugadores[0] = jugador1;
            jugadores[1] = jugador2;

            partidas[0] = new JuegoLogica();
            partidas[1] = new JuegoLogica();

            Text = "Proyecto Matemático - Juego";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(650, 500);
            MinimumSize = new Size(650, 500);

            CrearInterfaz();
            ActualizarPantalla();
        }

        private void CrearInterfaz()
        {
            Label titulo = new Label();
            titulo.Text = "JUEGO MATEMÁTICO";
            titulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(200, 25);
            Controls.Add(titulo);

            lblJugador = new Label();
            lblJugador.AutoSize = true;
            lblJugador.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblJugador.Location = new Point(55, 90);
            Controls.Add(lblJugador);

            lblNivel = new Label();
            lblNivel.AutoSize = true;
            lblNivel.Location = new Point(55, 135);
            Controls.Add(lblNivel);

            lblPuntaje = new Label();
            lblPuntaje.AutoSize = true;
            lblPuntaje.Location = new Point(240, 135);
            Controls.Add(lblPuntaje);

            lblVidas = new Label();
            lblVidas.AutoSize = true;
            lblVidas.Location = new Point(400, 135);
            Controls.Add(lblVidas);

            lblPregunta = new Label();
            lblPregunta.AutoSize = false;
            lblPregunta.TextAlign = ContentAlignment.MiddleCenter;
            lblPregunta.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblPregunta.Location = new Point(100, 185);
            lblPregunta.Size = new Size(450, 65);
            Controls.Add(lblPregunta);

            Label ayuda = new Label();
            ayuda.Text = "Escriba una respuesta numérica:";
            ayuda.AutoSize = true;
            ayuda.Location = new Point(190, 270);
            Controls.Add(ayuda);

            txtRespuesta = new TextBox();
            txtRespuesta.Location = new Point(200, 300);
            txtRespuesta.Width = 250;
            txtRespuesta.Font = new Font("Segoe UI", 14);
            txtRespuesta.TextAlign = HorizontalAlignment.Center;
            txtRespuesta.KeyPress += TxtRespuesta_KeyPress;
            Controls.Add(txtRespuesta);

            btnResponder = new Button();
            btnResponder.Text = "RESPONDER";
            btnResponder.Location = new Point(220, 350);
            btnResponder.Size = new Size(210, 42);
            btnResponder.Click += BtnResponder_Click;
            Controls.Add(btnResponder);

            btnSalir = new Button();
            btnSalir.Text = "Finalizar y ver resultados";
            btnSalir.Location = new Point(200, 410);
            btnSalir.Size = new Size(250, 32);
            btnSalir.Click += BtnSalir_Click;
            Controls.Add(btnSalir);

            AcceptButton = btnResponder;
        }

        private void TxtRespuesta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void ActualizarPantalla()
        {
            JuegoLogica juego = partidas[turno];

            if (dosJugadores)
            {
                lblJugador.Text = "Turno de: " + jugadores[turno];
            }
            else
            {
                lblJugador.Text = "Jugador: " + jugadores[0];
            }

            lblNivel.Text = "Nivel: " + juego.Nivel;
            lblPuntaje.Text = "Puntaje: " + juego.Puntaje;
            lblVidas.Text = "Vidas: " + juego.Vidas;

            lblPregunta.Text = juego.Numero1 + " " +
                               juego.OperacionActual + " " +
                               juego.Numero2 + " = ?";

            txtRespuesta.Clear();
            txtRespuesta.Focus();
        }

        private void BtnResponder_Click(object sender, EventArgs e)
        {
            int respuesta;

            if (!int.TryParse(txtRespuesta.Text.Trim(), out respuesta))
            {
                MessageBox.Show(
                    "Ingrese un número válido.",
                    "Entrada inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtRespuesta.Focus();
                return;
            }

            JuegoLogica juego = partidas[turno];
            bool correcta = juego.ComprobarRespuesta(respuesta);

            if (correcta)
            {
                MessageBox.Show(
                    "¡Respuesta correcta! +10 puntos.",
                    "¡Bien!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Respuesta incorrecta. Se pierde una vida.",
                    "Incorrecto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            if (juego.Vidas <= 0)
            {
                if (dosJugadores)
                {
                    MessageBox.Show(
                        jugadores[turno] +
                        " se quedó sin vidas. Se muestran los resultados.",
                        "Fin del jugador",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Se terminaron las vidas.",
                        "Fin del juego",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                FinalizarJuego();
                return;
            }

            if (dosJugadores)
            {
                turno = (turno + 1) % 2;
            }

            ActualizarPantalla();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            FinalizarJuego();
        }

        private void RegistrarPuntajes()
        {
            cantidadRegistros = 0;

            int cantidadJugadores = dosJugadores ? 2 : 1;

            for (int i = 0; i < cantidadJugadores; i++)
            {
                if (cantidadRegistros < nombres.Length)
                {
                    nombres[cantidadRegistros] = jugadores[i];
                    puntajes[cantidadRegistros] = partidas[i].Puntaje;
                    cantidadRegistros++;
                }
            }

            OrdenarRanking();
        }

        private void OrdenarRanking()
        {
            // Ordenamiento descendente mediante arreglos paralelos.
            for (int i = 0; i < cantidadRegistros - 1; i++)
            {
                for (int j = i + 1; j < cantidadRegistros; j++)
                {
                    if (puntajes[j] > puntajes[i])
                    {
                        int temporalPuntaje = puntajes[i];
                        puntajes[i] = puntajes[j];
                        puntajes[j] = temporalPuntaje;

                        string temporalNombre = nombres[i];
                        nombres[i] = nombres[j];
                        nombres[j] = temporalNombre;
                    }
                }
            }
        }

        private void FinalizarJuego()
        {
            RegistrarPuntajes();

            Hide();

            using (ResultadosForm resultados =
                   new ResultadosForm(nombres, puntajes, cantidadRegistros))
            {
                resultados.ShowDialog();
            }

            Close();
        }
    }
}
