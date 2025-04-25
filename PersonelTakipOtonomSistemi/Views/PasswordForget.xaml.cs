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
			var kullan�c�Ad� = isim.Text.Trim();
			var kullan�c�Soyad� = soyad.Text.Trim();
			var tcno = tcNo.Text.Trim();
			var yenisifre = tekradanYeniSifre.Text.Trim();
            var DogumTarihi = dogumTarihi.Date;


            if (int.TryParse(personelId.Text.Trim(), out int personel�d))
			{
			if ( kullan�c�Ad� != null && kullan�c�Soyad� != null && yeniSifre.Text == tekradanYeniSifre.Text)
			{
                    var personeller = await _personelServices.GetTumPersoneller();
                    var personel = personeller.FirstOrDefault(x => x.TCKimlikNo == tcno);
					if (personel != null)
					{
						if (personel.Sifre != yenisifre) {
						if (personel.Ad.Trim() == kullan�c�Ad� && personel.Soyad.Trim() == kullan�c�Soyad� && personel.PersonelID == personel�d && personel.DogumTarihi == DogumTarihi)
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
								y�ll�k�zinHakk� = personel.y�ll�k�zinHakk�
							};

							await _personelServices.PersonelGuncelle(guncellencek);
							DisplayAlert("", "�ifre Ba�ar�l� �ekilde De�i�tirildi.", "Tamam");
						}
						else
						{

							DisplayAlert("", "Yanl�� Giri� Yapt�n�z", "Tamam");

						}
					}
					else
					{

							DisplayAlert("","Ayn� �ifreyi Tekrar Kullanamazs�n�z","Tamam");

					}
					}
					else
					{
						DisplayAlert("","Yanl�� Giri� Yapt�n�z","Tamam");
					}
			}
			else
			{
				DisplayAlert("", "Eksik Ya da Hatal� Giri� Yapt�n�z", "Tamam");

			}
			}
            else
            {
                
                await DisplayAlert("Hata", "Personel ID ge�erli bir tam say� de�il.", "Tamam");
            }
        }
		else
		{
			DisplayAlert("", "Eksik Giri� Yapt�n�z", "Tamam");
		}
    }
}