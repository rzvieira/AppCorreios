using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AppConsultaCorreios
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (IWebDriver driver = new ChromeDriver())
            //{
            //    //Notice navigation is slightly different than the Java version
            //    //This is because 'get' is a keyword in C#
            //    driver.Navigate().GoToUrl("https://www2.correios.com.br/sistemas/rastreamento/");

            //    // Find the text input element by its name
            //    IWebElement query = driver.FindElement(By.Name("objetos"));

            //    // Enter something to search for
            //    query.SendKeys("RS221708965DE");

            //    // Now submit the form. WebDriver will find the form for us from the element
            //    query.Submit();

            //    // Google's search is rendered dynamically with JavaScript.
            //    // Wait for the page to load, timeout after 10 seconds
            //    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10000));
            //    //wait.Until(d => d.Title.StartsWith("cheese", StringComparison.OrdinalIgnoreCase));

            //    // Should see: "Cheese - Google Search" (for an English locale)
            //    Console.WriteLine("Page title is: " + driver.Title);
            //}



            test();

            Console.ReadKey();
        }

        static async void test()
        {
            WebServiceCorreios.AtendeClienteClient client = new WebServiceCorreios.AtendeClienteClient();

            string[] list = { "RS221709008DE",
"RS221709011DE",
"RS221709025DE",
"RS221709039DE",
"RS221709042DE",
"RS221709056DE",
"RS221709073DE",
"RS221709087DE",
"RS221709095DE",
"RS221709100DE",
"RS221709113DE",
"RS221709127DE",
"RS221709135DE",
"RS221709144DE",
"RS221709158DE",
"RS221709161DE",
"RS221709175DE",
"RS221709189DE",
"RS221709215DE",
"RS221709246DE",
"RS221709250DE",
"RS221709263DE",
"RS221709277DE",
"RS221709285DE",
"RS221709294DE",
"RS221709303DE",
"RS221709317DE",
"RS221709325DE",
"RS221709334DE",
"RS221709348DE",
"RS221709351DE",
"RS221709365DE",
"RS221666608DE",
"RS221666611DE",
"RS221666625DE",
"RS221666639DE",
"RS221666642DE",
"RS221666656DE",
"RS221666660DE",
"RS221666673DE",
"RS221666687DE",
"RS221666695DE",
"RS221666700DE",
"RS221666713DE",
"RS221666727DE",
"RS221666735DE",
"RS221666744DE",
"RS221666758DE",
"RS221666761DE",
"RS221666775DE",
"RS221666789DE",
"RS221666792DE",
"RS221666801DE",
"RS221666815DE",
"RS221666829DE",
"RS221666832DE",
"RS221666846DE",
"RS221666850DE",
"RS221666863DE",
"RS221666877DE",
"RS221666885DE",
"RS221666894DE",
"RS221666903DE",
"RS221666925DE",
"RS221666934DE",
"RS221666951DE",
"RS221666965DE",
"RS221666979DE",
"RS221666982DE",
"RS221666996DE",
"RS221667002DE",
"RS221667016DE",
"RS221667020DE",
"RS221667033DE",
"RS221667047DE",
"RS221667055DE",
"RS221667118DE",
"RS221667121DE",
"RS221667135DE",
"RS221667149DE",
"RS221667152DE",
"RS221667166DE",
"RS221667170DE",
"RS221667183DE",
"RS221667197DE",
"RS221667206DE",
"RS221667210DE",
"RS221667223DE",
"RS221667237DE",
"RS221667245DE",
"RS221667254DE",
"RS221667268DE",
"RS221667271DE",
"RS221667285DE",
"RS221667299DE",
"RS221667308DE",
"RS221667311DE",
"RS221667325DE",
"RS221667339DE",
"RS221667356DE",
"RS221667373DE",
"RS221667387DE",
"RS221667395DE",
"RS221667400DE",
"RS221667413DE",
"RS221667427DE",
"RS221667435DE",
"RS221667444DE",
"RS221667458DE",
"RS221667461DE",
"RS221667475DE",
"RS221667489DE"
 };


            List<string> crs = new List<string>();

            for (int i = 0; i < list.Length; i++)
            {
                string[] obj = { list[i] };

                var response = await client.consultaSROAsync(obj, "L", "U", "ECT", "SRO");

                var str = XElement.Parse(response.@return);

                foreach (var item in str.Elements("objeto"))
                {
                    var cr = item.Element("numero").Value.ToString();
                    var result = item.Elements("evento").Where(x => x.Element("status").Value.Equals("30")).ToList();

                    if (result.Count > 0)
                    {
                        Console.WriteLine("CR: " + cr + "\nResultado: " +  result[0].ToString());
                        //Console.WriteLine(response.@return.ToString());

                        if(string.IsNullOrEmpty(crs.FirstOrDefault(c => c.Contains(cr))))
                        {
                            crs.Add(cr);
                        }
                    }
                }
            }

            File.AppendAllLines(@"C:\crs_pagamento_pendente1.txt", crs);

            Console.WriteLine("Total Crs: " + crs.Count.ToString());
        }
    }
}
