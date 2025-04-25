using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Services;

namespace PersonelTakipOtonomSistemi.Views;

public partial class IstenAyrılma : ContentPage
{
	private Personel _personel;
	private readonly IPersonelServices _personelServices;
	public void setPersonel(Personel personel)
	{
		_personel = personel;
	}
	public IstenAyrılma()
	{
		InitializeComponent();
		_personelServices = new PersonelServices();
	}

	private async void ContentPage_Loaded(object sender, EventArgs e)
	{
		kullanıcıAdı.Text = _personel.Ad.ToString();
		kullanıcıSoyadı.Text = _personel.Soyad.ToString();
		kullanıcıTcNo.Text = _personel.TCKimlikNo.ToString();
		kullanıcıPersonelId.Text = _personel.PersonelID.ToString();
		kullanıcıDepertman.Text = _personel.Departman.ToString();
		kullanıcıPozisyon.Text = _personel.Pozisyon.ToString();
		kullanıcıDogumTarihi.Text = _personel.DogumTarihi.ToString();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{

		var personeller = await _personelServices.GetTumPersoneller();
		var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);
		if (personel.aktifMi != "Ayrıldı") { 
		if (onay1.IsChecked && onay2.IsChecked && onay3.IsChecked )
		{
			var ayrıldı = "Ayrıldı";

			var guncellenecek = new PersonelGuncelleDto
			{
				Ad = _personel.Ad,
				Soyad = _personel.Soyad,
				Sifre = _personel.Sifre,
				aktifMi = ayrıldı.ToString().Trim(),
				Departman = _personel.Departman,
				DogumTarihi = _personel.DogumTarihi,
				PersonelID = _personel.PersonelID,
				Eposta = _personel.Eposta,
				IseBaslamaTarihi = _personel.IseBaslamaTarihi,
				Maas = _personel.Maas,
				Pozisyon = _personel.Pozisyon,
				TCKimlikNo = _personel.TCKimlikNo,
				TelefonNo = _personel.TelefonNo,
				yıllıkİzinHakkı = _personel.yıllıkİzinHakkı
			};

			await _personelServices.PersonelGuncelle(guncellenecek);
			await DisplayAlert("Başarılı", "Ayrılma işlemi başarılı bir şekilde gerçekleştirildi.", "Tamam");
            }
			else
			{
			await DisplayAlert("Uyarı", "Lütfen tüm onay kutucuklarını işaretleyin.", "Tamam");
			}
        }
		else
		{
			DisplayAlert("Uyari","İşten Ayrılma Gerçekleştirilemiyor","Tamam");
		}
    }
}