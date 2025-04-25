using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Services;

namespace PersonelTakipOtonomSistemi.Views;

public partial class PasswordForget : ContentPage
{
	private readonly IPersonelServices _personelServices;
	public PasswordForget()
	{
        InitializeComponent();
		_personelServices = new PersonelServices();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {

		
        
		if (isim.Text != null && soyad.Text != null && personelId.Text != null && tcNo.Text != null && yeniSifre.Text != null && tekradanYeniSifre != null)
		{
			var kullanýcýAdý = isim.Text.Trim();
			var kullanýcýSoyadý = soyad.Text.Trim();
			var tcno = tcNo.Text.Trim();
			var yenisifre = tekradanYeniSifre.Text.Trim();
            var DogumTarihi = dogumTarihi.Date;


            if (int.TryParse(personelId.Text.Trim(), out int personelýd))
			{
			if ( kullanýcýAdý != null && kullanýcýSoyadý != null && yeniSifre.Text == tekradanYeniSifre.Text)
			{
                    var personeller = await _personelServices.GetTumPersoneller();
                    var personel = personeller.FirstOrDefault(x => x.TCKimlikNo == tcno);
					if (personel != null)
					{
						if (personel.Sifre != yenisifre) {
						if (personel.Ad.Trim() == kullanýcýAdý && personel.Soyad.Trim() == kullanýcýSoyadý && personel.PersonelID == personelýd && personel.DogumTarihi == DogumTarihi)
						{

							var guncellencek = new PersonelGuncelleDto
							{
								Ad = personel.Ad,
								Soyad = personel.Soyad,
								aktifMi = personel.aktifMi,
								Departman = personel.Departman,
								DogumTarihi = personel.DogumTarihi,
								Sifre = yenisifre,
								Eposta = personel.Eposta,
								IseBaslamaTarihi = personel.IseBaslamaTarihi,
								Maas = personel.Maas,
								PersonelID = personel.PersonelID,
								Pozisyon = personel.Pozisyon,
								TCKimlikNo = personel.TCKimlikNo,
								TelefonNo = personel.TelefonNo,
								yýllýkÝzinHakký = personel.yýllýkÝzinHakký
							};

							await _personelServices.PersonelGuncelle(guncellencek);
							DisplayAlert("", "Þifre Baþarýlý Þekilde Deðiþtirildi.", "Tamam");
						}
						else
						{

							DisplayAlert("", "Yanlýþ Giriþ Yaptýnýz", "Tamam");

						}
					}
					else
					{

							DisplayAlert("","Ayný Þifreyi Tekrar Kullanamazsýnýz","Tamam");

					}
					}
					else
					{
						DisplayAlert("","Yanlýþ Giriþ Yaptýnýz","Tamam");
					}
			}
			else
			{
				DisplayAlert("", "Eksik Ya da Hatalý Giriþ Yaptýnýz", "Tamam");

			}
			}
            else
            {
                
                await DisplayAlert("Hata", "Personel ID geçerli bir tam sayý deðil.", "Tamam");
            }
        }
		else
		{
			DisplayAlert("", "Eksik Giriþ Yaptýnýz", "Tamam");
		}
    }
}