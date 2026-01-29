namespace Lab2.Test;

public class UnitTest1
{


    [Fact]
    public void Fruit_ProperFormat_ShouldMatchString()
    {
        var fruit = new Fruit
        {
            Name = "Apple",
            IsSweet = false,
            Price = 1.25,
        };


        string expected = $"Fruit: Name={fruit.Name}, IsSweet={fruit.IsSweet}, Price={fruit.Price.ToString("C2")}, UsdPrice={Fruit.FormatUsdPrice(fruit.Price)}";

        var result = fruit.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void More_Than_One_Distinct_Name()
    {
        var fruits = new List<Fruit>();

        for (int i = 0; i < 20; i++)
        {
            Fruit fruit = Fruit.Create();
            fruits.Add(fruit);
        }

        var distinctNamesCount = fruits.Select(f => f.Name).Distinct().Count();

        Assert.True(distinctNamesCount > 1);

    }
}