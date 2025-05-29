using Etkinlik_Takvimi___Hatirlatici;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Etkinlik_Takvimi___Hatirlatici
{
    public class KullaniciTakvimi : IReminder, IZamanlayici
    {
        private readonly Timer _timer;
        public List<Etkinlik> Etkinlikler { get; } = new List<Etkinlik>();

        public event Action<Etkinlik> Hatirlatma;

        public KullaniciTakvimi()
        {
            _timer = new Timer(10_000);           // 10 saniyede bir kontrol
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime now = DateTime.Now;
            for (int i = Etkinlikler.Count - 1; i >= 0; i--)
            {
                if (Etkinlikler[i].Tarih <= now)
                {
                    Hatirlatma?.Invoke(Etkinlikler[i]);
                    Etkinlikler.RemoveAt(i);
                }
            }
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