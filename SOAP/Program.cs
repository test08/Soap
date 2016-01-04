using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SOAP
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Service(new Function());
            var webService = service.OpenService("http://localhost:8811/");
            Console.ReadKey();
            service.CloseService(webService);
        }

        public class Function : Service.IService
        {
            public string Execute()
            {
                Console.WriteLine(OperationContext.Current.RequestContext.RequestMessage.ToString());
                return "SOAP";
            }
        }

        public class Service
        {
            private readonly IService _function;

            public Service(IService service)
            {
                this._function = service;
            }

            [ServiceContract]
            public interface IService
            {
                [OperationContract]
                string Execute();
            }

            public WebServiceHost OpenService(string address)
            {
                var uri = new Uri(address);
                WebServiceHost service = new WebServiceHost(_function.GetType(), uri);
                service.AddServiceEndpoint(typeof(IService), new BasicHttpBinding(), String.Empty);
                service.Open();
                return service;
            }

            public void CloseService(WebServiceHost service) => service?.Close();

        }
    }
}
