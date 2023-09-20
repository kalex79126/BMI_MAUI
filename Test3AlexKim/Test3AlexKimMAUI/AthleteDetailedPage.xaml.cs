using Test3AlexKimMAUI.Data;
using Test3AlexKimMAUI.Models;
using Test3AlexKimMAUI.Utilities;
using System.Text;
using Test3AlexKim;

namespace Test3AlexKimMAUI;

public partial class AthleteDetailedPage : ContentPage
{
    private Athlete athlete;
    private App thisApp;
    private List<Sport> sports;

    public AthleteDetailedPage(Athlete athlete)
    {
        InitializeComponent();
        thisApp = Application.Current as App;
        sports = new List<Sport>();

        // Assign the athlete object passed to the constructor to the private athlete field
        this.athlete = athlete;

        // Calculate BMI and BMI category
        double bmi = BMI.bmiValue(athlete.Height, athlete.Weight, out string bmiCategory);

        // Set athlete details labels
        lblNameValue.Text = athlete.FullName;
        lblAgeValue.Text = athlete.Age.ToString();
        lblSportValue.Text = athlete.Sport.Name;
        lblHeightValue.Text = athlete.Height.ToString() + " cm";
        lblWeightValue.Text = athlete.Weight.ToString() + " kg";
        lblBMIValue.Text = bmi.ToString("F2");
        lblBMICategoryValue.Text = bmiCategory;
    }

    private async void ReturnClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}