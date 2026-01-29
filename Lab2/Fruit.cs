using System.Globalization;
public class Fruit
{
    public string Name { get; set; } = string.Empty;
    public bool IsSweet { get; set; }
    public decimal Price { get; set; }
    public decimal UsdPrice => Price / (decimal)UsdCourse.Current;

    public static Fruit Create()
    {
        Random r = new Random();
        string[] names = new string[] { "Apple", "Banana", "Cherry", "Durian", "Edelberry", "Grape", "Jackfruit" };

        return new Fruit
        {
            Name = names[r.Next(names.Length)],
            IsSweet = r.NextDouble() > 0.5,
            Price = (decimal)r.NextDouble() * 10

        };
    }

    public override string ToString()
    {
        return $"Fruit: Name={Name}, IsSweet={IsSweet}, Price={Price.ToString("C2")}, UsdPrice={FormatUsdPrice(UsdPrice)}";
    }

    public static string FormatUsdPrice(decimal price)
    {
        var usc = new CultureInfo("en-us");
        return price.ToString("C2", usc);
    }

}

