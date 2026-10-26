using System;

namespace ProyectoMatematicoWinForms
{
    // Esta clase contiene la lógica matemática separada de la interfaz gráfica.
    public class JuegoLogica
    {
        private readonly Random aleatorio = new Random();

        public int Nivel { get; private set; }
        public int Puntaje { get; private set; }
        public int Vidas { get; private set; }

        public string OperacionActual { get; private set; }
        public int Numero1 { get; private set; }
        public int Numero2 { get; private set; }
        public int RespuestaCorrecta { get; private set; }

        public JuegoLogica()
        {
            Reiniciar();
        }

        public void Reiniciar()
        {
            Nivel = 1;
            Puntaje = 0;
            Vidas = 3;
            GenerarPregunta();
        }

        public void GenerarPregunta()
        {
            Nivel = 1 + (Puntaje / 30);

            int maximo = 10 + (Nivel * 10);
            int cantidadOperaciones = Math.Min(4, 1 + (Nivel / 2));
            int tipo = aleatorio.Next(0, cantidadOperaciones);

            if (tipo == 0)
            {
                OperacionActual = "+";
                Numero1 = aleatorio.Next(1, maximo + 1);
                Numero2 = aleatorio.Next(1, maximo + 1);
                RespuestaCorrecta = Numero1 + Numero2;
            }
            else if (tipo == 1)
            {
                OperacionActual = "-";
                Numero1 = aleatorio.Next(1, maximo + 1);
                Numero2 = aleatorio.Next(1, maximo + 1);

                if (Numero2 > Numero1)
                {
                    int temporal = Numero1;
                    Numero1 = Numero2;
                    Numero2 = temporal;
                }

                RespuestaCorrecta = Numero1 - Numero2;
            }
            else if (tipo == 2)
            {
                OperacionActual = "×";
                Numero1 = aleatorio.Next(2, Math.Min(16, 6 + (Nivel * 2)));
                Numero2 = aleatorio.Next(2, Math.Min(16, 6 + (Nivel * 2)));
                RespuestaCorrecta = Numero1 * Numero2;
            }
            else
            {
                OperacionActual = "÷";
                Numero2 = aleatorio.Next(2, Math.Min(12, 5 + Nivel));
                RespuestaCorrecta = aleatorio.Next(2, Math.Min(12, 5 + Nivel));
                Numero1 = Numero2 * RespuestaCorrecta;
            }
        }

        public bool ComprobarRespuesta(int respuesta)
        {
            bool correcta = respuesta == RespuestaCorrecta;

            if (correcta)
            {
                Puntaje += 10;
            }
            else
            {
                Vidas--;
            }

            if (Vidas > 0)
            {
                GenerarPregunta();
            }

            return correcta;
        }
    }
}
