namespace aspnet05.Models;

public class Weather
{
    public int Id { get; set; }
    public string City { get; set; } = "";
    public int Temperature { get; set; }
    public string Condition { get; set; } = "";
}