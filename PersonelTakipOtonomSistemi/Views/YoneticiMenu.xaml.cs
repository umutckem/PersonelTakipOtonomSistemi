    
using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Services;
using static System.Net.Mime.MediaTypeNames;

namespace PersonelTakipOtonomSistemi.Views;

public partial class YoneticiMenu : ContentPage
{
    private readonly IPersonelServices _personelServices;
    private Personel _personel;

    public void SetPersonel(Personel personel)
    {
        _personel = personel;
    }
    public YoneticiMenu()
	{
        InitializeComponent();
        _personelServices = new PersonelServices();
        
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await GetPersoneller();
    }
    private async Task GetPersoneller()
    {
        var pesoneller = await _personelServices.GetTumPersoneller();
        
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        PersonelGoruntule personelGoruntule = new PersonelGoruntule();
        await Navigation.PushAsync(personelGoruntule);
        
    }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            DateTime dogumTarihi = DateTime.Parse(DogumTarihi.Text.Trim());
            double maas = double.Parse(Maas.Text.Trim());
            int izin = int.Parse(izinHakki.Text.Trim());

            var eklencek = new PersonelOlusturDto()
            {
                Ad = Ad.Text.Trim(),
                Soyad = Soyad.Text.Trim(),
                TCKimlikNo = TcNo.Text.Trim(),
                Departman = Depertman.Text.Trim(),
                DogumTarihi = dogumTarihi,
                Pozisyon = Pozisyon.Text.Trim(),
                TelefonNo = TelefonNo.Text.Trim(),
                Eposta = Eposta.Text.Trim(),
                Sifre = Sifre.Text.Trim(),
                Maas = maas,
                aktifMi = aktif.Text.Trim(),
                yýllýkÝzinHakký = izin,
               
            };
            await _personelServices.PersonelEkle(eklencek);

            await DisplayAlert("Ýþlem baþarýlý", "Personel kaydedildi", "Tamam");

            await GetPersoneller();
        }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        
        int personelId;
        if (!int.TryParse(PersonelId.Text.Trim(), out personelId))
        {
            await DisplayAlert("Hata", "Lütfen geçerli bir personel ID'si girin.", "Tamam");
            return;
        }

        
        string ad = Ad.Text.Trim();
        string soyad = Soyad.Text.Trim();
        string tcKimlikNo = TcNo.Text.Trim();
        DateTime dogumTarihi;
        if (!DateTime.TryParse(DogumTarihi.Text.Trim(), out dogumTarihi))
        {
            await DisplayAlert("Hata", "Lütfen geçerli bir doðum tarihi girin.", "Tamam");
            return;
        }
        string eposta = Eposta.Text.Trim();
        string telefonNo = TelefonNo.Text.Trim();
        string pozisyon = Pozisyon.Text.Trim();
        string departman = Depertman.Text.Trim();
        DateTime iseBaslamaTarihi;
        if (!DateTime.TryParse(IseBaslamaTarihi.Text.Trim(), out iseBaslamaTarihi))
        {
            await DisplayAlert("Hata", "Lütfen geçerli bir iþe baþlama tarihi girin.", "Tamam");
            return;
        }
        double maas = double.Parse(Maas.Text.Trim());
        string aktifmi = aktif.Text.Trim();
        int yýllýkizin = int.Parse(izinHakki.Text.Trim());


        var personelGuncelleDto = new PersonelGuncelleDto
        {
            PersonelID = personelId,
            Ad = ad,
            Soyad = soyad,
            TCKimlikNo = tcKimlikNo,
            DogumTarihi = dogumTarihi,
            Eposta = eposta,
            TelefonNo = telefonNo,
            Pozisyon = pozisyon,
            Departman = departman,
            IseBaslamaTarihi = iseBaslamaTarihi,
            Sifre = Sifre.Text,
            Maas = maas,
            aktifMi =aktifmi,
            yýllýkÝzinHakký = yýllýkizin,

            
        };

        
        await _personelServices.PersonelGuncelle(personelGuncelleDto);

        
        await DisplayAlert("Bilgi", "Personel baþarýyla güncellendi.", "Tamam");
    }

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        
        int personelId;
        if (!int.TryParse(PersonelId.Text.Trim(), out personelId))
        {
            await DisplayAlert("Hata", "Lütfen geçerli bir personel ID'si girin.", "Tamam");
            return;
        }

        
        var personeller = await _personelServices.GetTumPersoneller();

        
        var personel = personeller.FirstOrDefault(p => p.PersonelID == personelId);

        
        if (personel == null)
        {
            await DisplayAlert("Bilgi", "Bu ID'ye sahip bir personel bulunamadý.", "Tamam");
            return;
        }

        
        await _personelServices.PersonelSil(personelId);

        
        await DisplayAlert("Bilgi", "Personel baþarýyla silindi.", "Tamam");
    }

    private async void Button_Clicked_4(object sender, EventArgs e)
    {
        
        int personelId;
        if (!int.TryParse(PersonelId.Text.Trim(), out personelId))
        {
            await DisplayAlert("Hata", "Lütfen geçerli bir personel ID'si girin.", "Tamam");
            return;
        }

        
        var personeller = await _personelServices.GetTumPersoneller();

        
        var personel = personeller.FirstOrDefault(p => p.PersonelID == personelId);

        
        if (personel == null)
        {
            await DisplayAlert("Bilgi", "Bu ID'ye sahip bir personel bulunamadý.", "Tamam");
            return;
        }

        
        Ad.Text = personel.Ad;
        Soyad.Text = personel.Soyad;
        TcNo.Text = personel.TCKimlikNo;
        Depertman.Text = personel.Departman;
        DogumTarihi.Text = personel.DogumTarihi.ToString();
        Pozisyon.Text = personel.Pozisyon;
        TelefonNo.Text = personel.TelefonNo.ToString();
        Eposta.Text = personel.Eposta;
        IseBaslamaTarihi.Text = personel.IseBaslamaTarihi.ToString();
        Sifre.Text = personel.Sifre;
        Maas.Text = personel.Maas.ToString();
        aktif.Text = personel.aktifMi.ToString();
        izinHakki.Text = personel.yýllýkÝzinHakký.ToString();

    }
}