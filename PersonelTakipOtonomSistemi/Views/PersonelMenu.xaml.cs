using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Services;

namespace PersonelTakipOtonomSistemi.Views;

public partial class PersonelMenu : ContentPage
{

    private Personel _personel;
    private readonly IPersonelServices _personelServices;

    public void SetPersonel(Personel personel)
    {
        _personel = personel;
    }
    public PersonelMenu()
    {
        InitializeComponent();
        _personelServices = new PersonelServices();
    }

    private async void getir_Clicked(object sender, EventArgs e)
    {

        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);
        _personel = personel;

        Ad.Text = _personel.Ad.ToString();
        Soyad.Text = _personel.Soyad.ToString();
        TcNo.Text = _personel.TCKimlikNo.ToString();
        DogumTarihi.Text = _personel.DogumTarihi.ToString();
        Eposta.Text = _personel.Eposta.ToString();
        IseBaslamaTarihi.Text = _personel.IseBaslamaTarihi.ToString();
        TelefonNo.Text = _personel.TelefonNo.ToString();
        Pozisyon.Text = _personel.Pozisyon.ToString();
        Depertman.Text = _personel.Departman.ToString();
        maas.Text = _personel.Maas.ToString();
        aktif.Text = _personel.aktifMi.ToString();
        izin.Text = _personel.yýllýkÝzinHakký.ToString();
    }

    private async void SifreDegistir_Clicked(object sender, EventArgs e)
    {
        var EskiSifre = eskiSifre.Text;
        var YeniSifre = yeniSifre.Text;
        var TekrardanYeniSifre = yeniSifre.Text;

        var Select = await DisplayAlert("", "Deðiþtirmek Ýstediðine Emin misin.", "Evet", "Hayýr");
        if (Select == true)
        {
            if (EskiSifre != null && YeniSifre != null && TekrardanYeniSifre != null && YeniSifre == TekrardanYeniSifre && _personel.Sifre.ToString() == EskiSifre)
            {
                var personelGuncelleDto = new PersonelGuncelleDto
                {
                    PersonelID = _personel.PersonelID,
                    Ad = _personel.Ad,
                    Soyad = _personel.Soyad,
                    TCKimlikNo = _personel.TCKimlikNo,
                    DogumTarihi = _personel.DogumTarihi,
                    Eposta = _personel.Eposta,
                    TelefonNo = _personel.TelefonNo,
                    Pozisyon = _personel.Pozisyon,
                    Departman = _personel.Departman,
                    IseBaslamaTarihi = _personel.IseBaslamaTarihi,
                    Sifre = YeniSifre,
                    aktifMi = _personel.aktifMi,
                    Maas = _personel.Maas,
                    yýllýkÝzinHakký = _personel.yýllýkÝzinHakký,

                };

                await _personelServices.PersonelGuncelle(personelGuncelleDto);
                DisplayAlert("", "Baþarýlý Þifre Deðiþtirme", "Tamam");
            }
            else
            {
                DisplayAlert("Uyarý", "Hatalý Þifre Giriþimi", "Tamam");
            }

        }

    }

    private async void talepEt_Clicked(object sender, EventArgs e)
    {
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == x.PersonelID);

        var gunceldurum = "Ýzinli";
        if (istenilenMiktar.Text != null && personel.aktifMi != "Ayrýldý") { 
            var istenilenGunMÝktari = int.Parse(istenilenMiktar.Text.Trim());

        var guncelDurum = _personel.yýllýkÝzinHakký - istenilenGunMÝktari;
        if (_personel.yýllýkÝzinHakký > 0 && istenilenGunMÝktari > 0)
        {
            if (_personel.yýllýkÝzinHakký >= istenilenGunMÝktari)
            {
                var guncellenecek = new PersonelGuncelleDto
                {
                    PersonelID = _personel.PersonelID,
                    Eposta = _personel.Eposta,
                    Pozisyon = _personel.Pozisyon,
                    Ad = _personel.Ad,
                    Sifre = _personel.Sifre,
                    Soyad = _personel.Soyad,
                    aktifMi = gunceldurum,
                    Departman = _personel.Departman,
                    DogumTarihi = _personel.DogumTarihi,
                    IseBaslamaTarihi = _personel.IseBaslamaTarihi,
                    Maas = _personel.Maas,
                    TCKimlikNo = _personel.TCKimlikNo,
                    TelefonNo = _personel.TelefonNo,
                    yýllýkÝzinHakký = guncelDurum,
                };

                await _personelServices.PersonelGuncelle(guncellenecek);
                DisplayAlert("Ýþlem Baþarýlý", $"{istenilenGunMÝktari} Gün Ýzin Alýnmýþtýr. Geriye Kalan Gün Miktarý {guncelDurum}", "Tamam");
                izin.Text = guncelDurum.ToString();
                aktif.Text = gunceldurum.ToString();
            }
            else
            {
                await DisplayAlert("", $"Ýstenilen Miktar {_personel.yýllýkÝzinHakký} Üstünde Olduðundan Ýþlem Saðlanamýyor.", "Tamam");
            }
        }
        else
        {
            await DisplayAlert("", "Yýllýk Ýzin Hakkýnýz Bulunmamaktadýr", "Tamam");
        }
     }
        else
        {
            DisplayAlert("", "Herhangi Bir Deðer Girilmemiþtir ya da Ýþten Ayrýlma Durumu Vardýr", "Tamam");
        }
       
    }

    private async void aktifOl_Clicked(object sender, EventArgs e)
    {
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);
        bool select = await DisplayAlert("UYARI","Ýzinden Dönüþ Yapmak Ýstediðinze Emin Misiniz","Evet","Hayýr");

        if ( select == true && personel.aktifMi.Trim() != "Ayrýldý")
        {
            var GuncelDurum = "Aktif";
            var guncellenecek = new PersonelGuncelleDto
            {
                PersonelID = _personel.PersonelID,
                Eposta = _personel.Eposta,
                Pozisyon = _personel.Pozisyon,
                Ad = _personel.Ad,
                Sifre = _personel.Sifre,
                Soyad = _personel.Soyad,
                aktifMi = GuncelDurum,
                Departman = _personel.Departman,
                DogumTarihi = _personel.DogumTarihi,
                IseBaslamaTarihi = _personel.IseBaslamaTarihi,
                Maas = _personel.Maas,
                TCKimlikNo = _personel.TCKimlikNo,
                TelefonNo = _personel.TelefonNo,
                yýllýkÝzinHakký = _personel.yýllýkÝzinHakký,
            };
            aktif.Text = GuncelDurum.ToString();

            await _personelServices.PersonelGuncelle(guncellenecek);
            await DisplayAlert("","Baþarýyla Ýþe Dönüþ Gerçekleþtirdiniz","Tamam");
        }
        else
        {
            DisplayAlert("","Ýþten Ayrýldýðýnýz Ýçin Gerçekleþtiremiyoruz","Tamam");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);

        IstenAyrýlma ýstenAyrýlma = new IstenAyrýlma();
        if(personel != null) { 
        ýstenAyrýlma.setPersonel(personel);
        await Navigation.PushAsync(ýstenAyrýlma);
        }
        else
        {
            DisplayAlert("","Bilgiler Aktarýlamadý","Tamam");
        }
    }
}
