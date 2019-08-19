using System;

using api.visanet.validate.entitys;
using api.visanet.validate.utils;
using System.Net;
using System.IO;

namespace api.visanet.validate.cases
{
    public class Encripted_Payload
    {
        public string runEncripted_Payload()
        {
            Security objSecurity = new Security();
            AccessToken objAccessToken = new AccessToken();
            objSecurity = objAccessToken.getAccessToken();

            Random random = new Random();

            Order objOrder = new Order();
            objOrder.purchaseNumber = random.Next(1000, 9999).ToString();
            objOrder.amount = 0.20;
            objOrder.currency = "PEN";
            objOrder.externalTransactionId = Guid.NewGuid().ToString().ToUpper();

            Card objCard = new Card();
            objCard.cardNumber = "4242424242424242";
            objCard.expirationMonth = 12;
            objCard.expirationYear = 20;
            objCard.cvv2 = "123";

            CardHolder objCardHolder = new CardHolder();
            objCardHolder.firstName = "Juan";
            objCardHolder.lastName = "Perez";
            objCardHolder.email = "jperez@gmail.com";
            objCardHolder.phoneNumber = "2132300";

            ValidateRQ objValidate = new ValidateRQ();
            objValidate.channel = "pasarela";
            objValidate.captureType = "manual";
            objValidate.tokenize = false;
            objValidate.order = objOrder;
            objValidate.card = objCard;
            objValidate.cardHolder = objCardHolder;

            string endpointurl, body, bodyEncript, url, merchantId;

            endpointurl = "apitestenv.vnforapps.com";
            merchantId = "602545705";

            body = JsonHelper.JsonSerializer<ValidateRQ>(objValidate);

            string encriptPayload = CSEncryptDecrypt.EncryptData(body, objSecurity.keys[0].baseKey.ToString(), objSecurity.keys[0].iv.ToString());

            Encriptation objEncriptation = new Encriptation();
            objEncriptation.key = objSecurity.keys[0].token.ToString();
            objEncriptation.payload = encriptPayload;

            bodyEncript = JsonHelper.JsonSerializer<Encriptation>(objEncriptation);

            url = "https://" + endpointurl + "/api.validate/v1/validate/ecommerce/" + merchantId;
            HttpWebRequest request;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", objSecurity.accessToken);
            StreamWriter writer;
            writer = new StreamWriter(request.GetRequestStream());
            writer.Write(bodyEncript);
            writer.Close();

            HttpWebResponse response;
            StreamReader reader;
            string resultadoAuth;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                reader = new StreamReader(response.GetResponseStream());
                var bufferAuth = reader.ReadToEnd();
                resultadoAuth = bufferAuth.ToString();
                reader.Close();
            }
            catch (WebException ex)
            {
                resultadoAuth = ex.Message.ToString();
            }

            return resultadoAuth;
        }
    }
}