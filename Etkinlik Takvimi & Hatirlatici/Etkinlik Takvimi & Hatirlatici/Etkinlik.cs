using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etkinlik_Takvimi___Hatirlatici
{
    public class Etkinlik
    {
        public Guid Id { get; } = Guid.NewGuid(); //Globally Unique Identifier
        public string Baslik { get; set; }
        public DateTime Tarih { get; set; }
        public string Aciklama { get; set; }
    }
}
