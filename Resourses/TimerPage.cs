using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SACSA.Resourses
{
    public class TimerPage
    {
        private DispatcherTimer dispatcherTimer;

        public Action<string> FuntionToCallWhenTimerIsZero { get; private set; }

        public TimerPage(Action<string> funtionToCall)
        {
            this.FuntionToCallWhenTimerIsZero = funtionToCall;
            dispatcherTimer = new DispatcherTimer();
        }

        public void InitTimer()
        {
            dispatcherTimer.Tick += new EventHandler(Temporizador_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0,50);
            dispatcherTimer.Start();
        }
        private void Temporizador_Tick(object sender, EventArgs e)
        {
            this.DetenerTiempo();
            this.FuntionToCallWhenTimerIsZero("TimeOut");
        }

        public void IniciarTiempo()
        {
            this.InitTimer();
        }

        public void DetenerTiempo()
        {
            dispatcherTimer.IsEnabled = false;
            dispatcherTimer.Tick -= Temporizador_Tick;
            dispatcherTimer.Stop();
        }

        public void ReiniciarTiempo()
        {
            DetenerTiempo();
            InitTimer();
        }





    }
}
