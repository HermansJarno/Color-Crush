using System;
using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;

public static class GoogleAuthHandler 
{
    private static string _clientId = "1046461917421-tk0jc75qni94uf98uau4p8jstsq69gh8.apps.googleusercontent.com";
    private static string _clientSecret = "ujYJbpX0924L0ZCIi2tUhNzC";
    private static string _authUri = "https://accounts.google.com/o/oauth2/v2/auth";
    private static string _tokenUri = "https://oauth2.googleapis.com/token";
    private static string _redirectUri = "com.trigate.ColorCrush.app";

    public static void GetUserCode(){
        Application.OpenURL($"{_authUri}?client_id={_clientId}&redirect_uri={_redirectUri}&response_type=code&scope=email");
    }

    public static void ExchangeAuthCodeWithIdToken(string code, Action<GoogleTokenResponse> callback){
        RestClient.Post($"{_tokenUri}?code={code}&client_id={_clientId}&client_secret={_clientSecret}&redirect_uri={_redirectUri}&grant_type=authorization_code", null).Then(response => {
            Debug.Log(response.Text);
            var data = StringSerializationHelper.Deserialize(typeof(GoogleTokenResponse), response.Text) as GoogleTokenResponse;
            callback(data);
        });
    }
}
