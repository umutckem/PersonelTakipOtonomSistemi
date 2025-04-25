using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelTakipOtonomSistemi.Dtos
{
    internal class PersonelGuncelleDto
    {
        public int PersonelID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TCKimlikNo { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Eposta { get; set; }
        public string TelefonNo { get; set; }
        public double Maas { get; set; }
        public string aktifMi { get; set; }
        public int yıllıkİzinHakkı { get; set; }
        public string Pozisyon { get; set; }
        public string Departman { get; set; }
        public string Sifre { get; set; }
        public DateTime IseBaslamaTarihi { get; set; }

    }
}
