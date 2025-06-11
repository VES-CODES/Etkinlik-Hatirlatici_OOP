using Etkinlik_Takvimi___Hatirlatici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;            // ← FirstOrDefault için
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
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // 1. zamanı gelmiş ilk etkinliği bul
            var due = Etkinlikler.FirstOrDefault(ev => ev.Tarih <= DateTime.Now);
            if (due == null) return;

            // 2. bir kereye mahsus ses çal
            SystemSounds.Exclamation.Play();

            // 3. event’i tetikle (Form1’deki mesaj kutusunu açacak)
            Hatirlatma?.Invoke(due);

            // 4. listeden çıkar (bir daha tetiklenmesin)
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