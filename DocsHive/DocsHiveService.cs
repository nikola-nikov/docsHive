using System.ServiceModel;
using System.IO;
using System.Xml;

namespace DocsHive.Service
{
    public class DocsHiveService : IDocsHiveService
    {
        public string Test(string value)
        {
            return "Test: " + value;
        }

        public void UploadDocument(string username, string password, XmlNode documentData, string documentFile, string description, string receiverKey)
        {
            throw new FaultException("Not implemented.");
        }

        public DocumentDetails DownloadDocument(string username, string password, int documentID)
        {
            throw new FaultException("Not implemented.");
        }

        public void PendingDocuments(string username, string password)
        {
            throw new FaultException("Not implemented.");
        }
    }
}
