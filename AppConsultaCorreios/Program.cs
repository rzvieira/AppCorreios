using System;

namespace AppConsultaCorreios
{
    class Program
    {
        static void Main(string[] args)
        {


            //var client = new ServiceCorreio.ServiceClient();
            //var response = client.buscaEventosAsync("mundofast", "220100ra", "L", "T", "101", "LZ783294953CN");

            test();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        static async void test()
        {
            WebServiceCorreios.AtendeClienteClient client = new WebServiceCorreios.AtendeClienteClient();

            string[] list = { "LZ783294953CN", "RS221667342DE", "RS221667475DE" };
            var response = await client.consultaSROAsync(list, "L", "T", "ECT", "SRO");
        }
    }
}
