using System.Threading.Tasks;
using System.Text.Json;

namespace IMT;

public partial class ResultPage : ContentPage
{
	double Result, weight, height;
	public ResultPage(double weight, double height)
	{
		InitializeComponent();
		height /= 100;
		Result = Math.Round(weight / (height * height), 1);
		IMTResult.Text = $"��� ���: {Result.ToString()}";
		if (Result < 18.6) labelResult.Text = "��������������� ����.";
		else if (Result < 25) labelResult.Text = "���������� ���.";
		else if (Result < 30) labelResult.Text = "���������� ���.";
		else if (Result < 35) labelResult.Text = "��������.";
		else labelResult.Text = "������� ��������.";
    }

    async void toMainPage_Clicked(object sender, EventArgs e)
    {

		await Navigation.PopAsync();
    }

    private async void saveResult_Clicked(object sender, EventArgs e)
    {
        var result = new IMTResults
        {
            Weight = weight, 
            Height = height,
            IMT = Result,
            Date = DateTime.Now
        };

        await SaveResultAsync(result);
        await DisplayAlert("�����", "��������� �������", "OK");
    }

    private async Task SaveResultAsync(IMTResults result)
    {
        string fileName = "imt_results.json";
        string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        List<IMTResults> existingResults = new();

        // ��������� ������������
        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            existingResults = JsonSerializer.Deserialize<List<IMTResults>>(json) ?? new List<IMTResults>();
        }

        // ��������� �����
        existingResults.Add(result);

        // ���������
        string newJson = JsonSerializer.Serialize(existingResults, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, newJson);
    }
}