using Test3AlexKimMAUI.Models;

namespace Test3AlexKimMAUI
{

    public partial class App : Application
    {
        public bool needSportRefresh;
        public List<Sport> AllSport;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}