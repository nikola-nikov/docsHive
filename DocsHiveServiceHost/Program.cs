using DocsHive.Service;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DocsHive.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Uri httpUrl = new Uri("http://localhost:8090/DocsHiveService");
                ServiceHost host = new ServiceHost(typeof(DocsHiveService), httpUrl);
                host.AddServiceEndpoint(typeof(IDocsHiveService), new WSHttpBinding(), "");
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                host.Description.Behaviors.Add(smb);
                host.Open();
                Console.WriteLine("Service is hosted at " + httpUrl.ToString());
                Console.WriteLine("Press <Enter> to close.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
