using System;

using api.visanet.validate.entitys;
using api.visanet.validate.utils;
using System.Net;
using System.IO;

namespace api.visanet.validate.cases
{
    public class Encripted_Scope
    {
        public string runEncripted_Scope()
        {
            Security objSecurity = new Security();
            AccessToken objAccessToken = new AccessToken();
            objSecurity = objAccessToken.getAccessToken();

            string encriptScope = CSEncryptDecrypt.EncodeTo64("{\"scopes\":[{\"key\":\"" + objSecurity.keys[0].token.ToString() + "\",\"elements\":[\"card.cardNumber\",\"card.cvv2\"]}]}");
            string encriptCardNumber = CSEncryptDecrypt.EncryptData("4242424242424242", objSecurity.keys[0].baseKey.ToString(), objSecurity.keys[0].iv.ToString());
            string encriptCvv2 = CSEncryptDecrypt.EncryptData("123", objSecurity.keys[0].baseKey.ToString(), objSecurity.keys[0].iv.ToString());

            Random random = new Random();

            Order objOrder = new Order();
            objOrder.purchaseNumber = random.Next(1000, 9999).ToString();
            objOrder.amount = 0.20;
            objOrder.currency = "PEN";
            objOrder.externalTransactionId = Guid.NewGuid().ToString().ToUpper();

            Card objCard = new Card();
            objCard.cardNumber = encriptCardNumber;
            objCard.expirationMonth = 12;
            objCard.expirationYear = 20;
            objCard.cvv2 = encriptCvv2;

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

            string endpointurl, body, url, merchantId;

            endpointurl = "apitestenv.vnforapps.com";
            merchantId = "602545705";

            body = JsonHelper.JsonSerializer<ValidateRQ>(objValidate);

            url = "https://" + endpointurl + "/api.validate/v1/validate/ecommerce/" + merchantId;
            HttpWebRequest request;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", objSecurity.accessToken);
            request.Headers.Add("Scope", encriptScope);
            StreamWriter writer;
            writer = new StreamWriter(request.GetRequestStream());
            writer.Write(body);
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