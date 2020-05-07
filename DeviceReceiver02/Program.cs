using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;

namespace DeviceReceiver02
{

    public class InputData
    {
        public string Message { get; set; }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            Console.WriteLine("Welcome to the Message Receiver");
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });
            appBuilder.UseWebApi(config);
        }
    }

    public class ValuesController : ApiController
    {
        private static int counter = 0;
        public string Post(InputData data)
        {
            if (data != null)
            {
                counter++;
                string received_message = data.Message;
                string[] divided_message  = received_message.Split('~');
                string username = divided_message[0];
                string password = divided_message[1];
                string tmessage = divided_message[2];
                Console.WriteLine("Received Message: ---" + tmessage + "--- from " + username + ".  Total messages so far: " + counter);
            }
            return "Count=" + counter.ToString();
        }

        public string Get()
        {
            Console.WriteLine("GOT " );
            return "Messages Received so far...=" + counter.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string aaa = "aaa";

            string baseAddress = "http://localhost:55461/";
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Local host set to:" + baseAddress + ". Awaiting Messages...");
                Console.ReadLine();
            }
        }
    }
}
