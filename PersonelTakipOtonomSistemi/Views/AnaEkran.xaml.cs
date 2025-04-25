
namespace PersonelTakipOtonomSistemi.Views;

public partial class AnaEkran : ContentPage
{
	public AnaEkran()
	{
		InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        
        YoneticiLogin yoneticiLogin = new YoneticiLogin();
        await Navigation.PushAsync(yoneticiLogin);
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        LoginViews loginViews = new LoginViews();
        await Navigation.PushAsync(loginViews);
    }
}