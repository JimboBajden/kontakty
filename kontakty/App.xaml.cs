namespace kontakty
{using MVVM.Pages;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MVVM.Pages.MainPage());
        }
    }
}
