using System.Text;
using Test3AlexKimMAUI.Utilities;
using Test3AlexKimMAUI.Models;
using Test3AlexKimMAUI.Data;

namespace Test3AlexKimMAUI
{
    public partial class MainPage : ContentPage
    {
        private App thisApp;
        private List<Sport> sports;
        private List<Athlete> athletes;

        public MainPage()
        {
            InitializeComponent();
            thisApp = Application.Current as App;
            thisApp.needSportRefresh = true;
            sports = new List<Sport> { new Sport { ID = 0, Name = " All Sports" } };
            athletes = new List<Athlete>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            athleteList.ItemsSource = null;

            if (thisApp.needSportRefresh)
            {
                await ShowSports();
                //Remember, this will also trigger ShowArtworks()
            }
            else
            {
                await ShowSports();
            }
            //Enable the Add Button
        }

        private async Task ShowSports()
        {
            //Get the sports
            SportRepository spt = new SportRepository();
            try
            {
                List<Sport> Sports = await spt.GetSports();
                foreach (Sport p in Sports.OrderBy(d => d.Name))
                {
                    sports.Add(p);
                }
                ddlSports.ItemsSource = sports;
                thisApp.needSportRefresh= false;
                ddlSports.SelectedIndex = 0;
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                await DisplayAlert("Problem Getting List of Sports:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.GetBaseException().Message.Contains("connection with the server"))
                    {

                        await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                    }
                }
                else
                {
                    if (ex.Message.Contains("NameResolutionFailure"))
                    {
                        await DisplayAlert("Internet Access Error ", "Cannot resolve the Uri: " + Jeeves.DBUri.ToString(), "Ok");
                    }
                    else
                    {
                        await DisplayAlert("General Error ", ex.Message, "Ok");
                    }
                }
            }
        }


        private async void ddlSports_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ShowAthletes();
        }

        private async Task ShowAthletes()
        {
            Loading.IsRunning = true;
            int? SportID = ((Sport)ddlSports.SelectedItem)?.ID;
            AthleteRepository r = new AthleteRepository();
            try
            {
                if (SportID.GetValueOrDefault() > 0)
                {
                    athletes = await r.GetAthletesBySport(SportID.GetValueOrDefault());
                }
                else
                {
                    athletes = await r.GetAthletes();
                }
                athleteList.ItemsSource = athletes;
                athleteList.SelectedItem = null;
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                //athleteList.IsVisible = false;
                await DisplayAlert("Error Getting Athletes:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.GetBaseException().Message.Contains("connection with the server"))
                    {

                        await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("General Error", "If the problem persists, please call your system administrator.", "Ok");
                }
            }
            finally
            {
                Loading.IsRunning = false;
            }
        }


        private async void AthleteSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var athlete = (Athlete)e.SelectedItem;

                AthleteDetailedPage athleteDetailedPage = new AthleteDetailedPage(athlete);
                athleteDetailedPage.BindingContext = athlete;
                await Navigation.PushAsync(athleteDetailedPage);

            }
        }
    }
}