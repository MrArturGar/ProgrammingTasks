using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

internal class Program
{
    static void Main(string[] args)
    {
        int countLines = int.Parse(Console.ReadLine());

        Rootobject[] elements = new Rootobject[countLines];
        for (int i = 0; i < countLines; i++)
            elements[i] = JsonSerializer.Deserialize<Rootobject>(Console.ReadLine());

        List<Offer> offers = new List<Offer>();
        foreach (var el in elements)
        {
            offers.InsertRange(0, el.offers);
        }
        offers = offers.OrderBy(c => c.price).ThenBy(c => c.offer_id).ToList();

        Rootobject result = new Rootobject();
        result.offers = offers.ToArray();
        Console.WriteLine(JsonSerializer.Serialize<Rootobject>(result));
    }

}
public class Rootobject
{
    public Offer[] offers { get; set; }
}

public class Offer
{
    public string offer_id { get; set; }
    public long market_sku { get; set; }
    public int price { get; set; }
}