namespace Parkwhere05.Services
{
    interface IWeatherGateway
    {
        string GetCurrentWeather(string location);
    }
}
