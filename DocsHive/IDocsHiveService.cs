using System.ServiceModel;
using System.Xml;
using System.IO;
using System;
using System.Runtime.Serialization;

namespace DocsHive.Service
{
    [ServiceContract]
    public interface IDocsHiveService
    {
        [OperationContract]
        string Test(string value);

        [OperationContract]
        void UploadDocument(string username, string password, XmlNode documentData, string documentFile, string description, string receiverKey);

        [OperationContract]
        DocumentDetails DownloadDocument(string username, string password, int documentID);

        [OperationContract]
        void PendingDocuments(string username, string password);
    }

    public enum Result
    {
        Success,
        Failure
    }

    [DataContract]
    public class DocumentDetails
    {
        public DocumentMetaData MetaData { get; set; }

    }

    [DataContract]
    public class DocumentMetaData
    {
        [DataMember]
        public Guid KeyGuid { get; set; }

        [DataMember]
        public string  Description { get;set; }

        [DataMember]
        public string IssuedBy { get; set; }

        [DataMember]
        DateTime issuedDate { get; set; }
    }

}
