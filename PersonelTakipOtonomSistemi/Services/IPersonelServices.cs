using PersonelTakipOtonomSistemi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelTakipOtonomSistemi.Services
{
    internal interface IPersonelServices
    {
        Task<List<Personel>> GetTumPersoneller();

        Task PersonelEkle(PersonelOlusturDto personel);
        Task PersonelGuncelle(PersonelGuncelleDto personel);
        Task PersonelSil(int id);

    }
}
