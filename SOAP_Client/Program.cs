using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SOAP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = RequireСontract("http://localhost:8811");
            Console.WriteLine(service.Execute());
            Console.ReadKey();
        }

        private static IService RequireСontract(string address)
        {
            var endpoint = new EndpointAddress(address);
            ChannelFactory<IService> channel = new ChannelFactory<IService>(new BasicHttpBinding(), endpoint);
            return channel.CreateChannel();
        }
        [ServiceContract]
        public interface IService
        {
            [OperationContract]
            string Execute();
        }


    }
}
