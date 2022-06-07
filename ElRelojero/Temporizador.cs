using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElRelojero
{
    internal class Temporizador
    {
        public event DelegadoTemporizador TiempoCumplido;
        public delegate void DelegadoTemporizador();
        private CancellationToken cancellation;
        private CancellationTokenSource cancellationTokenSource;
        private Task hilo;
        private int intervalo;

        public bool EstaActivo
        {
           
            get
            {
                return hilo is not null &&
                    (hilo.Status == TaskStatus.Running ||
                    hilo.Status == TaskStatus.WaitingToRun ||
                    hilo.Status == TaskStatus.WaitingForActivation);
            }
        }

        public int Intervalo
        {
            get { return intervalo; }
            set { intervalo = value; }  
            
        }

        public void CorrerTiempo()
        {
            do
            {
                if (TiempoCumplido is not null)
                {
                    TiempoCumplido.Invoke();
                }

                Thread.Sleep(intervalo);
            } while (!cancellation.IsCancellationRequested);
        }

        public void DetenerTemporizador()
        {
            if (EstaActivo && hilo is not null)
            {
                cancellationTokenSource.Cancel();
            }
        }

        public void IniciarTemporizador()
        {
            
            if (hilo is null || hilo.IsCompleted)
            {
                cancellationTokenSource = new CancellationTokenSource();
                cancellation = cancellationTokenSource.Token;

                hilo = new Task(CorrerTiempo, cancellation);
            }

            if (!EstaActivo)
            {
                hilo.Start();
            }
        }
        public Temporizador(int intervalo)
        {
            this.intervalo = intervalo;
        }

       
    }
}
