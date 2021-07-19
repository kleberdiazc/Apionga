using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppSonga.Clases
{
    public class ConexionIBM
    {
        string _URLaccDes = "https://iam.ng.bluemix.net/oidc/token";
        string _URLserDes = "https://sandbox.food.ibm.com/ift/api/identity-proxy/exchange_token/v1/organization/803480ff-88b2-4f4a-9ef7-6b98c4373298";
        string _apiKey = "qlqiJ7gVwe2jXYkwMVg9b4zNJlToDUeVcQ4N4vPaM0zY";
        string _URLxmlDess = "https://sandbox.food.ibm.com/fs/connector/v1/assets";
        public string URLaccDes { get { return _URLaccDes; } }
        public string URLserDes { get { return _URLserDes; } }
        public string apiKey { get { return _apiKey; } }
        public string URLxmlDess { get { return _URLxmlDess; } }

        string _URLaccPrd = "https://iam.cloud.ibm.com/identity/token";
        string _apiKeyPrd = "O-dVnObB9sv0dzn5QHsjRpsB-kbpqKnlXGgGjzOKMTXo";
        string _URLserPrd = "https://food.ibm.com/ift/api/identity-proxy/exchange_token/v1/organization/7d7929c2-7056-438c-b8d6-35d6af2b54a5";
        string _URLxmlPrd = "https://food.ibm.com/fs/connector/v1/assets";
        public string URLaccPrd { get { return _URLaccPrd; } }
        public string URLserPrd { get { return _URLserPrd; } }
        public string apiKeyPrd { get { return _apiKeyPrd; } }
        public string URLxmlPrd { get { return _URLxmlPrd; } }


    }
}
