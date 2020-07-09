using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FirebaseAuthHandler 
{
    private static string _apiKey = "AIzaSyAJcIFGa0ZKs8BmBjjOxgi7057xdvPrZ34";

    public static void SignInWithToken(string googleIdToken, string googleAccessToken){
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.Credential credential =
            Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
        if (task.IsCanceled) {
            Debug.LogError("SignInWithCredentialAsync was canceled.");
            return;
        }
        if (task.IsFaulted) {
            Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
            return;
        }

        Firebase.Auth.FirebaseUser newUser = task.Result;
        Debug.LogFormat("User signed in successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);
        });

        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null) {
        string name = user.DisplayName;
        string email = user.Email;
        string uid = user.UserId;
        //use user.tokenAsync()
        Debug.Log(uid);
        }
    }
}
