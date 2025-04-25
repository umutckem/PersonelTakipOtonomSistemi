using PersonelTakipOtonomSistemi.Dtos;
using PersonelTakipOtonomSistemi.Services;

namespace PersonelTakipOtonomSistemi.Views;

public partial class YoneticiLogin : ContentPage
{
    private readonly IPersonelServices _personelServices;
    public YoneticiLogin()
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
        
        var gelenTcNo = kullanýcýAdý.Text;
        var gelenKullanýcýSýfresi = sifre.Text;
        if(gelenTcNo != null && gelenKullanýcýSýfresi != null) { 
        var personeller = await _personelServices.GetTumPersoneller();
        var personel = personeller.FirstOrDefault(p => p.TCKimlikNo == gelenTcNo);

        
        if (personel.TCKimlikNo == gelenTcNo  &&  personel.Sifre == gelenKullanýcýSýfresi  && personel.Pozisyon == "Yonetici")
        {
            

            YoneticiMenu yoneticiMenu = new YoneticiMenu();
            yoneticiMenu.SetPersonel(personel);
            await Navigation.PushAsync(yoneticiMenu);
            await DisplayAlert($"{personel.Ad} {personel.Soyad}", $"Hoþgeldiniz.", "Tamam");
        }
        else
        {
            await DisplayAlert("", "Eksik veya Hatalý Giriþ", "Tamam");
        }
        }
        else
        {
            DisplayAlert("","Eksik Giriþ Yaptýnýz","Tamam");
        }
    }
}
