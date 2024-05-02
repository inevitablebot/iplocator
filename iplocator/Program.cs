using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace iplocator
{

    public class Data
    {
        public string city { get; set; }
        public string loc { get; set; }
        public string country { get; set; }
        public string postal { get; set; }
        public string region { get; set; }

        public string org { get; set; }
    }

        internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "geolocator";
            Console.Write("enter ip address: ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine("[+] Request Successfully Made");
                    string responcedata = await response.Content.ReadAsStringAsync();
                    Data ipinfo = JsonConvert.DeserializeObject<Data>(responcedata);

                    Console.Clear();
                    Console.WriteLine($"country: {ipinfo.country}");
                    Console.WriteLine($"city   : { ipinfo.city}" );
                    Console.WriteLine( $"cordinates: {ipinfo.loc}");
                    Console.WriteLine($"Postal code:{ipinfo.postal}");
                    Console.WriteLine($"region : {ipinfo.region}");
                    Console.WriteLine($"ASN {ipinfo.org}");
                    string[] cords = ipinfo.loc.Split(',');
                    Console.WriteLine($"google maps: https://www.google.com/maps/?q={cords[0]},{cords[1]}");

                }
                catch ( HttpRequestException ex ) 
                {
                        Console.WriteLine($"ERROR: {ex.Message}");

                }
            }
        }
    }
}

