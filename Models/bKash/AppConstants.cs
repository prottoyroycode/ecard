using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
  public  class AppConstants
    {

        //public const string AppKey = "5tunt4masn6pv2hnvte1sb5n3j";
        //public const string AppSecret = "1vggbqd4hqk9g96o9rrrp2jftvek578v7d2bnerim12a87dbrrka";
        //public const string UserName = "sandboxTestUser";
        //public const string Password = "hWD@8vtzw0";
        //

        public const string tokenize_app_key = "4f6o0cjiki2rfm34kfdadl1eqq";
        public const string tokenize_app_secret = "2is7hdktrekvrbljjh44ll3d9l1dtjo4pasmjvs5vl5qr3fug4b";
        public const string tokenize_username = "sandboxTokenizedUser02";
        public const string tokenize_pass = "sandboxTokenizedUser02@12345";


        public const string agreementID = "TokenizedMerchant02RIEQQQ41639231028012";

        public const string TokenUrl = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/token/grant";




        public const string CreateAgreementUrl = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/create";
                                                  

        public const string ExexAgreementUrl = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/execute";



        //local
        //public const string PaymentCallbackTest = "http://localhost:55810/api/BkashCallBack/transection";
        public const string PaymentCallback = "http://localhost:55810/api/BkashCallBack/transection";
        //local
      //  public const string PaymentCallback = "https://api.evouchers.store/api/BkashCallBack/transection";


    }
}
