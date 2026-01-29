using System.Runtime.InteropServices;

UsdCourse.Current = await UsdCourse.GetUsdCourseAsync();

List<Fruit> fruits = new List<Fruit>();

for (int i = 0; i < 15; i++)
{
    Fruit fruit = Fruit.Create();
    fruits.Add(fruit);
    
}


var sweetFruits = fruits.Where(x => x.IsSweet).OrderByDescending(x => x.Price);

foreach (var sweet in sweetFruits)
{
    Console.WriteLine(sweet.ToString());
}