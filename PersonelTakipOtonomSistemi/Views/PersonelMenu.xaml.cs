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
        izin.Text = _personel.yıllıkİzinHakkı.ToString();
    }

    private async void SifreDegistir_Clicked(object sender, EventArgs e)
    {
        var EskiSifre = eskiSifre.Text;
        var YeniSifre = yeniSifre.Text;
        var TekrardanYeniSifre = yeniSifre.Text;

        var Select = await DisplayAlert("", "Değiştirmek İstediğine Emin misin.", "Evet", "Hayır");
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
                    yıllıkİzinHakkı = _personel.yıllıkİzinHakkı,

                };

                await _personelServices.PersonelGuncelle(personelGuncelleDto);
                DisplayAlert("", "Başarılı Şifre Değiştirme", "Tamam");
            }
            else
            {
                DisplayAlert("Uyarı", "Hatalı Şifre Girişimi", "Tamam");
            }

        }

    }

    private async void talepEt_Clicked(object sender, EventArgs e)
    {
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == x.PersonelID);

        var gunceldurum = "İzinli";
        if (istenilenMiktar.Text != null && personel.aktifMi != "Ayrıldı") { 
            var istenilenGunMİktari = int.Parse(istenilenMiktar.Text.Trim());

        var guncelDurum = _personel.yıllıkİzinHakkı - istenilenGunMİktari;
        if (_personel.yıllıkİzinHakkı > 0 && istenilenGunMİktari > 0)
        {
            if (_personel.yıllıkİzinHakkı >= istenilenGunMİktari)
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
                    yıllıkİzinHakkı = guncelDurum,
                };

                await _personelServices.PersonelGuncelle(guncellenecek);
                DisplayAlert("İşlem Başarılı", $"{istenilenGunMİktari} Gün İzin Alınmıştır. Geriye Kalan Gün Miktarı {guncelDurum}", "Tamam");
                izin.Text = guncelDurum.ToString();
                aktif.Text = gunceldurum.ToString();
            }
            else
            {
                await DisplayAlert("", $"İstenilen Miktar {_personel.yıllıkİzinHakkı} Üstünde Olduğundan İşlem Sağlanamıyor.", "Tamam");
            }
        }
        else
        {
            await DisplayAlert("", "Yıllık İzin Hakkınız Bulunmamaktadır", "Tamam");
        }
     }
        else
        {
            DisplayAlert("", "Herhangi Bir Değer Girilmemiştir ya da İşten Ayrılma Durumu Vardır", "Tamam");
        }
       
    }

    private async void aktifOl_Clicked(object sender, EventArgs e)
    {
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);
        bool select = await DisplayAlert("UYARI","İzinden Dönüş Yapmak İstediğinze Emin Misiniz","Evet","Hayır");

        if ( select == true && personel.aktifMi.Trim() != "Ayrıldı")
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
                yıllıkİzinHakkı = _personel.yıllıkİzinHakkı,
            };
            aktif.Text = GuncelDurum.ToString();

            await _personelServices.PersonelGuncelle(guncellenecek);
            await DisplayAlert("","Başarıyla İşe Dönüş Gerçekleştirdiniz","Tamam");
        }
        else
        {
            DisplayAlert("","İşten Ayrıldığınız İçin Gerçekleştiremiyoruz","Tamam");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);

        IstenAyrılma ıstenAyrılma = new IstenAyrılma();
        if(personel != null) { 
        ıstenAyrılma.setPersonel(personel);
        await Navigation.PushAsync(ıstenAyrılma);
        }
        else
        {
            DisplayAlert("","Bilgiler Aktarılamadı","Tamam");
        }
    }
}
