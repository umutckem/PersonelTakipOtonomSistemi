 using PersonelTakipOtonomSistemi.Services;

namespace PersonelTakipOtonomSistemi.Views;

public partial class LoginViews : ContentPage
{
    private readonly IPersonelServices _personelServices;
    public LoginViews()
	{
		InitializeComponent();
        _personelServices = new PersonelServices();
        
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
       
        var gelenTcNo = tcNo.Text;
        var gelenPassword = sifre.Text;

        var personels = await _personelServices.GetTumPersoneller();
        var personel = personels.FirstOrDefault(x => x.TCKimlikNo == gelenTcNo);

        if (personel!= null &&personel.TCKimlikNo == gelenTcNo && personel.Sifre == gelenPassword && personel.Pozisyon == "Personel")
        {
            DisplayAlert($"{personel.Ad}{personel.Soyad}", "Hoþ Geldiniz.", "Tamam");
            PersonelMenu personelMenu = new PersonelMenu();
            personelMenu.SetPersonel(personel);
            await Navigation.PushAsync(personelMenu);
            
        }
        else
        {
            DisplayAlert("", "Eksik veya Hatalý Giriþ", "Tamam");
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        PasswordForget sifreUnuttum = new PasswordForget();
        await Navigation.PushAsync(sifreUnuttum);
    }
}