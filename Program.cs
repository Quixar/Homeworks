using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

internal class Program
{
    private const string ApiKey = "7681824a2082a8c93c332b583d896d22";
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

    static async Task Main()
    {
        Console.Write("Введіть назву міста: ");
        string city = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(city))
        {
            Console.WriteLine("Назва міста не може бути порожньою.");
            return;
        }

        await GetWeather(city);
    }

    static async Task GetWeather(string city)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric&lang=ua";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject weatherData = JObject.Parse(responseBody);

                string description = weatherData["weather"][0]["description"].ToString();
                double temp = weatherData["main"]["temp"].ToObject<double>();
                double feelsLike = weatherData["main"]["feels_like"].ToObject<double>();
                double humidity = weatherData["main"]["humidity"].ToObject<double>();

                Console.WriteLine($"Погода в {city}:");
                Console.WriteLine($"Опис: {description}");
                Console.WriteLine($"Температура: {temp}°C");
                Console.WriteLine($"Відчувається як: {feelsLike}°C");
                Console.WriteLine($"Вологість: {humidity}%");
            }
            else
            {
                Console.WriteLine("Не вдалося отримати дані. Перевірте правильність назви міста.");
            }
        }
    }
}