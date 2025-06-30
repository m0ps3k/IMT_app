using System.Globalization;
using System.Threading.Tasks;

namespace IMT
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void goToResult_Clicked(object sender, EventArgs e)
        {
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            string weightStr = weightEntry.Text.Replace(',', '.');
            string heightStr = heightEntry.Text.Replace(',', '.');
            if (Double.TryParse(weightStr, formatter, out double weight) && Double.TryParse(heightStr, formatter, out double height))
            {
                await Navigation.PushAsync(new ResultPage(weight, height));
                weightEntry.Text = "";
                heightEntry.Text = "";
            }
            else
            {
                await DisplayAlert("Ошибка", "Произошла ошибка при подсчете ИМТ. Пожалуйста, проверьте корректность введных данных.", "ОK");
            }
        }

        private async void goToHistrory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistroryPage());
        }
    }
}
