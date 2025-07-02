using System.Threading.Tasks;
using System.Text.Json;

namespace IMT;

public partial class ResultPage : ContentPage
{
	double Result, weight, height;
    public ResultPage(double weightD, double heightD)
    {
        InitializeComponent();
        height = heightD;
        weight = weightD;
        height /= 100;
        Result = Math.Round(weight / (height * height), 1);
        IMTResult.Text = $"Ваш ИМТ: {Result.ToString()}";
        if (Result < 18.6)
        {
            labelResult.Text = "Недостаточность веса.";
            imageResult.Source = "imt_risunok_1.png";
        }
        else if (Result < 25)
        {
            labelResult.Text = "Нормальный вес.";
            imageResult.Source = "imt_risunok_2.png";
        }
        else if (Result < 30)
        {
            labelResult.Text = "Избыточный вес.";
            imageResult.Source = "imt_risunok_3.png";
        }
        else if (Result < 35)
        {
            labelResult.Text = "Ожирение.";
            imageResult.Source = "imt_risunok_4.png";
        }
        else
        {
            labelResult.Text = "Сильное ожирение.";
            imageResult.Source = "imt_risunok_5.png";
        }
    }

    async void toMainPage_Clicked(object sender, EventArgs e)
    {

		await Navigation.PopAsync();
    }

    private async void saveResult_Clicked(object sender, EventArgs e)
    {
        IMTResults result = new IMTResults
        {
            Weight = weight, 
            Height = height,
            IMT = Result,
            Date = DateTime.Now
        };

        await SaveResultAsync(result);
        await DisplayAlert("Успех", "Результат сохранён", "OK");
    }

    private async Task SaveResultAsync(IMTResults result)
    {
        string fileName = "imt_results.json";
        string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        List<IMTResults> existingResults = new();

        // Загружаем существующие
        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            existingResults = JsonSerializer.Deserialize<List<IMTResults>>(json) ?? new List<IMTResults>();
        }

        // Добавляем новый
        existingResults.Add(result);

        // Сохраняем
        string newJson = JsonSerializer.Serialize(existingResults, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, newJson);
    }
}