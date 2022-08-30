using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PLColab.Client.Api.Public;
using PLColab.Client.Core;
using PLColab.Client.Core.Model.Requests;
using System.IO;

namespace SRF_Facture
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public SRF_UploadResponse SRF_Upload(SRF_UploadRequest request)
        {
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            SRF_UploadResponse response = new SRF_UploadResponse();
            string msg = "";
            try
            {

                byte[] encodeBase64 = System.Convert.FromBase64String(request.fileXML);
                string xml = System.Text.UTF8Encoding.UTF8.GetString(encodeBase64);

                PLColab.Client.Core.PLColabClientConfig.Environment.IsBeta = request.Beta;

                var Request = new IssueRequest(xml, PLColab.Client.Core.Model.Requests.IssueRequest.FormatTypeEnum.XML3);

                Request.DocumentType = (PLColab.Client.Core.Model.Requests.IssueRequest.DocumentTypeEnum)request.DocumentType;

                Request.FolioKeyControl = request.FolioKeyControl;//En esta variebla va el clave tecnica asociada a cada numeracion electronica"1111111"
                Request.GraphicRepresentationTemplateName = request.TemplateName; //Variable nombre Rep Gráfica
                // "fc8eac422eba16e22ffd8c6f94b3f40a6e38162c";
                if (request.FileData != "")
                {
                    Request.QR_PositionX = request.QR_PositionX;//47;
                    Request.QR_PositionY = request.QR_PositionY;//11;
                    Request.CUFE_PositionX = request.CUFE_PositionX;//11;
                    Request.CUFE_PositionY = request.CUFE_PositionY;//70;
                    //bytes = Convert.FromBase64String(request.FileData); //encoding.GetBytes(request.FileData);
                    byte[] pdf = Convert.FromBase64String(request.FileData);

                    string name = request.FileName; //+ ".pdf";
                    Request.AddGraphicRepresentation(name, pdf);                   


                }

                if(request.AttachedDocs != "")
                {
                    string [] subAttachDoc = request.AttachedDocs.Split(';');
                    byte[] attachDocumentPath;

                    foreach (var sub in subAttachDoc)
                    {
                        attachDocumentPath = File.ReadAllBytes(sub);
                        //https://srfco.sharepoint.com/Dynamics/Documentos compartidos/Gestión de documentos empresariales/TEST (637720661580411789).pdf
                        string[] subAttachDocPath = sub.Split('/');
                        string nameAttached = subAttachDocPath[subAttachDocPath.Length - 1];
                        Request.AddAttachment(nameAttached, attachDocumentPath);
                    }
                }

                var ClientePrueba = new PLColab.Client.Api.Public.IssueModule();
                ClientePrueba.Auth_UserName = request.User;// "900641938";//Usuario enviado por Roimer
                ClientePrueba.Auth_Password = request.Pass;// "abc123$";//Calve enviada por Roimer
                ClientePrueba.Auth_Who = request.ForeignKey;//"9cc880f37eaf4cb49d63c44788605c0a";//request.ForeignKey;//"7d0ae8f67d8e4edea60b333ee0c59527";//Clave principal del portal de API management
                ClientePrueba.Auth_TenantId = request.TenantId;
                
                var response1 = ClientePrueba.Insert(Request);

                if (response1.IsSuccess)
                {
                    response.Response = "<UUID>" + response1.UUID + "</UUID>" + "<UrlPdf>" + response1.UrlPdf+ "</UrlPdf>" + "<UrlXml>" + response1.UrlXml + "</UrlXml>" + "<LDF>" + response1.LDF + "</LDF>";
                   
                    // response.Response = response1.UrlPdf;

                   // return response;
                }
                else
                {
                    for (int i = 0; i < response1.EventItems.Length; i++)
                    {
                        msg = response1.EventItems[i].ErrorCode;
                        msg = msg + " " + response1.EventItems[i].ShortDescription;
                    }
                    //msg = request.FolioKeyControl + " " + request.User + " " + request.Pass + " " + request.ForeignKey;
                    response.Response = msg;
                    //throw new Exception("Emision fallida");

                }

                return response;
            }
            catch (Exception ex)
            {
                /*throw new Exception("WebSer");
                throw;
                */
                //msg = request.FolioKeyControl + " " + request.User + " " + request.Pass + " " + request.ForeignKey;
                response.Response = ex.Message;
                return response;
            }

        }
    }
}
