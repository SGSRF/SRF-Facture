using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SRF_Facture
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        SRF_UploadResponse SRF_Upload(SRF_UploadRequest request);
        
    }


    [DataContract]
    public class SRF_UploadRequest
    {
        string _user = "";
        string _pass = "";
        string _FolioKeyControl = "";
        string _fileXML = "";
        string _fileName = "";
        string _fileData = "";
        int _QR_PositionX;
        int _QR_PositionY;
        int _CUFE_PositionY;
        int _CUFE_PositionX;
        int _documentType;
        string _foreignKey;
        Boolean _beta;
        string _templateName;
        string _tenantId = "";
        string _attachedDocs = "";

        [DataMember]
        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        [DataMember]
        public int DocumentType
        {
            get { return _documentType; }
            set { _documentType = value; }
        }

        [DataMember]
        public Boolean Beta
        {
            get { return _beta; }
            set { _beta = value; }
        }

        [DataMember]
        public string TemplateName
        {
            get { return _templateName; }
            set { _templateName = value; }
        }

        [DataMember]
        public string ForeignKey
        {
            get { return _foreignKey; }
            set { _foreignKey = value; }
        }

        [DataMember]
        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

        [DataMember]
        public string FolioKeyControl
        {
            get { return _FolioKeyControl; }
            set { _FolioKeyControl = value; }
        }

        [DataMember]
        public string fileXML
        {
            get { return _fileXML; }
            set { _fileXML = value; }
        }

        [DataMember]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [DataMember]
        public string FileData
        {
            get { return _fileData; }
            set { _fileData = value; }
        }
        [DataMember]
        public int QR_PositionX
        {
            get { return _QR_PositionX; }
            set { _QR_PositionX = value; }
        }
        [DataMember]
        public int QR_PositionY
        {
            get { return _QR_PositionY; }
            set { _QR_PositionY = value; }
        }

        [DataMember]
        public int CUFE_PositionX
        {
            get { return _CUFE_PositionX; }
            set { _CUFE_PositionX = value; }
        }

        [DataMember]
        public int CUFE_PositionY
        {
            get { return _CUFE_PositionY; }
            set { _CUFE_PositionY = value; }
        }

        [DataMember]
        public string TenantId
        {
            get { return _tenantId; }
            set { _tenantId = value; }
        }

        [DataMember]
        public string AttachedDocs
        {
            get { return _attachedDocs; }
            set { _attachedDocs = value; }
        }
    }

    [DataContract]
    public class SRF_UploadResponse
    {
        string response = "";

        [DataMember]
        public string Response
        {
            get { return response; }
            set { response = value; }
        }
    }
}
