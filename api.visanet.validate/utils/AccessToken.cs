using System;
using System.Text;

using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;

using api.visanet.validate.entitys;

namespace api.visanet.validate.utils
{
    public class AccessToken
    {
        public Security getAccessToken()
        {
            string endpointurl, url, user, password, credentials;

            endpointurl = "apitestenv.vnforapps.com";
            user = "pruebacevi@gmail.com";
            password = "z3ez5?XV";
            credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(user + ":" + password));

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;

            url = "https://" + endpointurl + "/api.security/v2/security/keys";

            HttpWebRequest request;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic " + credentials);
            StreamWriter writer;
            writer = new StreamWriter(request.GetRequestStream());
            writer.Close();

            HttpWebResponse response;
            StreamReader reader;
            string respuesta;
            Security objResultado = new Security();
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                reader = new StreamReader(response.GetResponseStream());
                var buffer = reader.ReadToEnd();
                respuesta = buffer.ToString();
                reader.Close();

                DataContractJsonSerializer service = new DataContractJsonSerializer(typeof(Security));
                MemoryStream memory = new MemoryStream(Encoding.UTF8.GetBytes(respuesta));
                objResultado = (Security)service.ReadObject(memory);

                return objResultado;
            }
            catch (Exception)
            {
                return objResultado;
            }
        }
    }
}
