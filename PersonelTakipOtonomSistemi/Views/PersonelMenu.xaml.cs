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
        izin.Text = _personel.y�ll�k�zinHakk�.ToString();
    }

    private async void SifreDegistir_Clicked(object sender, EventArgs e)
    {
        var EskiSifre = eskiSifre.Text;
        var YeniSifre = yeniSifre.Text;
        var TekrardanYeniSifre = yeniSifre.Text;

        var Select = await DisplayAlert("", "De�i�tirmek �stedi�ine Emin misin.", "Evet", "Hay�r");
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
                    y�ll�k�zinHakk� = _personel.y�ll�k�zinHakk�,

                };

                await _personelServices.PersonelGuncelle(personelGuncelleDto);
                DisplayAlert("", "Ba�ar�l� �ifre De�i�tirme", "Tamam");
            }
            else
            {
                DisplayAlert("Uyar�", "Hatal� �ifre Giri�imi", "Tamam");
            }

        }

    }

    private async void talepEt_Clicked(object sender, EventArgs e)
    {
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == x.PersonelID);

        var gunceldurum = "�zinli";
        if (istenilenMiktar.Text != null && personel.aktifMi != "Ayr�ld�") { 
            var istenilenGunM�ktari = int.Parse(istenilenMiktar.Text.Trim());

        var guncelDurum = _personel.y�ll�k�zinHakk� - istenilenGunM�ktari;
        if (_personel.y�ll�k�zinHakk� > 0 && istenilenGunM�ktari > 0)
        {
            if (_personel.y�ll�k�zinHakk� >= istenilenGunM�ktari)
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
                    y�ll�k�zinHakk� = guncelDurum,
                };

                await _personelServices.PersonelGuncelle(guncellenecek);
                DisplayAlert("��lem Ba�ar�l�", $"{istenilenGunM�ktari} G�n �zin Al�nm��t�r. Geriye Kalan G�n Miktar� {guncelDurum}", "Tamam");
                izin.Text = guncelDurum.ToString();
                aktif.Text = gunceldurum.ToString();
            }
            else
            {
                await DisplayAlert("", $"�stenilen Miktar {_personel.y�ll�k�zinHakk�} �st�nde Oldu�undan ��lem Sa�lanam�yor.", "Tamam");
            }
        }
        else
        {
            await DisplayAlert("", "Y�ll�k �zin Hakk�n�z Bulunmamaktad�r", "Tamam");
        }
     }
        else
        {
            DisplayAlert("", "Herhangi Bir De�er Girilmemi�tir ya da ��ten Ayr�lma Durumu Vard�r", "Tamam");
        }
       
    }

    private async void aktifOl_Clicked(object sender, EventArgs e)
    {
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);
        bool select = await DisplayAlert("UYARI","�zinden D�n�� Yapmak �stedi�inze Emin Misiniz","Evet","Hay�r");

        if ( select == true && personel.aktifMi.Trim() != "Ayr�ld�")
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
                y�ll�k�zinHakk� = _personel.y�ll�k�zinHakk�,
            };
            aktif.Text = GuncelDurum.ToString();

            await _personelServices.PersonelGuncelle(guncellenecek);
            await DisplayAlert("","Ba�ar�yla ��e D�n�� Ger�ekle�tirdiniz","Tamam");
        }
        else
        {
            DisplayAlert("","��ten Ayr�ld���n�z ��in Ger�ekle�tiremiyoruz","Tamam");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(x => x.PersonelID == _personel.PersonelID);

        IstenAyr�lma �stenAyr�lma = new IstenAyr�lma();
        if(personel != null) { 
        �stenAyr�lma.setPersonel(personel);
        await Navigation.PushAsync(�stenAyr�lma);
        }
        else
        {
            DisplayAlert("","Bilgiler Aktar�lamad�","Tamam");
        }
    }
}
