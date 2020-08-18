using System;
using System.Timers;

namespace WPFUI
{
    class Ser
    {
        private readonly Timer serres;

        public Ser()
        {
            serres = new Timer(1000) { AutoReset = true };
            serres.Elapsed += Serres_Elapsed;
        }

        private void Serres_Elapsed(object sender, ElapsedEventArgs e)
        {

        }

        public void Start()
        {
            serres.Start();
        }

        public void Stop()
        {
            serres.Stop();
        }
    }
}
