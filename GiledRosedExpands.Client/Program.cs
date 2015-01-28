using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Formatting;

namespace GiledRosedExpands.Client
{
    class Program
    {
        const string BasePath = "http://localhost:3512/";

        static void Main(string[] args)
        {
            string response;
            do
            {
                Console.WriteLine("Choose one of the options: ");
                Console.WriteLine("1 - Get Item");
                Console.WriteLine("2 - Purchase an item");
                response = Console.ReadLine();
            } while (response != "1" && response != "2");

            if (response == "1")
            {
                GetItems().Wait();
            }
            else
            {
                PurchaseItemOption().Wait();
            }
        }

        static async Task PurchaseItemOption()
        {

            Console.WriteLine("Insert the item name (Book): ");
            var userInput = Console.ReadLine();
            await PurchaseItem(userInput);
        }

        static async Task PurchaseItem(string itemName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var purchase = new { ItemName = itemName, Username = "Jorge" };
                    HttpResponseMessage response = await client.PostAsJsonAsync(BasePath + "api/purchase", purchase);
                    response.EnsureSuccessStatusCode();


                    Uri location = response.Headers.Location;
                    Console.WriteLine("Purchase location {0}", location);
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
            }
        }

        static async Task GetItems()
        {
            
            List<Item> result;
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(BasePath + "api/item");

                    //will throw an exception if not successful
                    response.EnsureSuccessStatusCode();

                    result = await response.Content.ReadAsAsync<List<Item>>();
                    result.ForEach(item => Console.WriteLine("Name: {0}",item.Name));
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }


    }

    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
