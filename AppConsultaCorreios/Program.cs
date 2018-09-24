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
            ConsultaCodigos();

            Console.ReadKey();
        }

        static async void ConsultaCodigos()
        {
            WebServiceCorreios.AtendeClienteClient client = new WebServiceCorreios.AtendeClienteClient();

            string[] list = File.ReadAllLines(@"C:\crs_pagamento_pendente.txt");


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
