using PersonelTakipOtonomSistemi.Views;
namespace PersonelTakipOtonomSistemi
{
    public partial class App : Application
    {
       

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AnaEkran());

        }

        
    }
}
