using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etkinlik_Takvimi___Hatirlatici
{
    public interface IReminder
    {
        event Action<Etkinlik> Hatirlatma;
    }

    public interface IZamanlayici
    {
        void Baslat();
        void Durdur();
    }
}
