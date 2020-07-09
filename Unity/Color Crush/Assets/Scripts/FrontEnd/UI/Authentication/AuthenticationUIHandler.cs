using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthenticationUIHandler : MonoBehaviour
{
    public InputField code;

    public void OnClickGetGoogleCode(){
    }

    public void OnClickGoogleSignIn(){
        GoogleAuthHandler.GetUserCode();
        /*GoogleAuthHandler.ExchangeAuthCodeWithIdToken(code.text, tokenResponse => {
            FirebaseAuthHandler.SignInWithToken(tokenResponse.id_token, tokenResponse.access_token);
        });*/
    }
}
