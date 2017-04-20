using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace ParsingJSONDictionaries
{
    class Program
    {
        //http://fixer.io/
        static void Main(string[] args)
        {
            Currency currencyResponse = new Currency();
            currencyResponse = Get("USD");
            Console.WriteLine(currencyResponse.Base);
            Console.WriteLine(currencyResponse.Date);
            foreach(var item in currencyResponse.Rates)
            {
                Console.WriteLine(item);
            }
            

            if(currencyResponse.Rates.ContainsKey("CAD"))
            {
                decimal CAD = currencyResponse.Rates["CAD"];
                Console.WriteLine("One USD is {0} CAD", CAD);
            }

            Console.ReadKey();
        }

        public static Currency Get(string type)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var jsonResponse = client.DownloadString("http://api.fixer.io/latest?base=" + type);
                    Currency currencyResponse = new Currency();
                    currencyResponse = JsonConvert.DeserializeObject<Currency>(jsonResponse);
                    return currencyResponse;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
