using System.Text.Json;
using System.Threading.Tasks;

namespace IMT;

public partial class HistroryPage : ContentPage
{
    string fileName = "imt_results.json";
    string filePath;
    public HistroryPage()
	{
		InitializeComponent();
        filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        resultsView.ItemsSource = await LoadResultsAsync();
    }


    private async Task<List<IMTResults>> LoadResultsAsync()
    {
        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<IMTResults>>(json) ?? new List<IMTResults>();
        }
        return new List<IMTResults>();
    }

    async void toMainPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}