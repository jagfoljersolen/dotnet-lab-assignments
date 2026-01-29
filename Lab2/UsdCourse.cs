using System.Xml.Linq;
public class UsdCourse
{
    public static float Current = 0;

    public async static Task<float> GetUsdCourseAsync()
    {
        var wc = new HttpClient();
        var response = await wc.GetAsync("https://api.nbp.pl/api/exchangerates/tables/a/?format=xml");
        if (!response.IsSuccessStatusCode) throw new InvalidOperationException();

        XDocument doc = XDocument.Parse(await response.Content.ReadAsStringAsync());
        // var midUsdValue = doc.Descendants("Rate")
        //                     .Where(rate =>
        //                 (string)rate.Element("Code") == "USD")
        //                     .Select(rate =>
        //                 (string)rate.Element("Mid"))
        //                     .FirstOrDefault();

        var midUsdValue = (from rate in doc.Descendants("Rate")
                           where (string)rate.Element("Code") == "USD"
                           select (string)rate.Element("Mid")).FirstOrDefault();
                            
        
        

        return Convert.ToSingle(midUsdValue , System.Globalization.CultureInfo.InvariantCulture);

        throw new InvalidOperationException();

    }
}