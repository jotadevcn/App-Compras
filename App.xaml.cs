namespace AppCompras;

public partial class App : Application
{
    [Obsolete]
    public App()
    {
        InitializeComponent();

        // ALTERADO (pra permitir navegação)
        MainPage = new NavigationPage(new MainPage());
    }
}