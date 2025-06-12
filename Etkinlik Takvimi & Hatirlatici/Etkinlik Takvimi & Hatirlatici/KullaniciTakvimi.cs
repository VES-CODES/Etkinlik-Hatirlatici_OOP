using Etkinlik_Takvimi___Hatirlatici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;            //  FirstOrDefault için
using System.Media;
using System.Timers;

namespace Etkinlik_Takvimi___Hatirlatici
{
    public class KullaniciTakvimi : IReminder, IZamanlayici
    {
        private readonly Timer _timer;

        public List<Etkinlik> Etkinlikler { get; } = new List<Etkinlik>();

        public event Action<Etkinlik> Hatirlatma;

        public KullaniciTakvimi(ISynchronizeInvoke syncObj)
        {
            _timer = new Timer(10_000);  // 10 saniyede bir kontrol
            _timer.SynchronizingObject = syncObj;
            _timer.AutoReset = true;
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           
            // zamanı gelmiş ilk etkinliği bulduk
            var due = Etkinlikler.FirstOrDefault(ev => ev.Tarih <= DateTime.Now);
            if (due == null) return;

            
            SystemSounds.Exclamation.Play();

            // event’i tetikle 
            Hatirlatma?.Invoke(due);

           
            Etkinlikler.Remove(due);
            
        }

        public void Baslat()
        {
            Timer_Elapsed(this, null);
            _timer.Start();
        }

        public void Durdur()
        {
            _timer.Stop();
        }
        
        public void Ekle(Etkinlik e)
        {
            Etkinlikler.Add(e);
            if (!_timer.Enabled) _timer.Start();
            if (e.Tarih <= DateTime.Now)
            {
                System.Media.SystemSounds.Exclamation.Play();
                Hatirlatma?.Invoke(e);
                Etkinlikler.Remove(e);
            }
        }

        public void Sil(Guid id)
        {
            Etkinlikler.RemoveAll(x => x.Id == id);
        }

        public List<Etkinlik> Listele()
        {
            return Etkinlikler;
        }
    }
}