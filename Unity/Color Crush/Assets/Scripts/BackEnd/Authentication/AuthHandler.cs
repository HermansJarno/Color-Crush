using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;

public class AuthHandler : MonoBehaviour
{
    public Text text;
    Firebase.Auth.FirebaseAuth auth;

    Task<GoogleSignInUser> signIn;

    public void SignIn(){
        GoogleSignIn.Configuration = new GoogleSignInConfiguration {
        RequestIdToken = true,
        // Copy this value from the google-service.json file.
        // oauth_client with type == 3
        WebClientId = "1046461917421-tk0jc75qni94uf98uau4p8jstsq69gh8.apps.googleusercontent.com"
        };

        signIn = GoogleSignIn.DefaultInstance.SignIn ();

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser> ();
        signIn.ContinueWith (task => {
            if (task.IsCanceled) {
                signInCompleted.SetCanceled ();
            } else if (task.IsFaulted) {
                signInCompleted.SetException (task.Exception);
            } else {
                Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential (((Task<GoogleSignInUser>)task).Result.IdToken, null);
                auth.SignInWithCredentialAsync (credential).ContinueWith (authTask => {
                    if (authTask.IsCanceled) {
                        signInCompleted.SetCanceled();
                    } else if (authTask.IsFaulted) {
                        signInCompleted.SetException(authTask.Exception);
                    } else {
                        signInCompleted.SetResult(((Task<FirebaseUser>)authTask).Result);

                        FirebaseUser user = auth.CurrentUser;
                        if(user != null){
                            string name = user.DisplayName;
                            string email = user.Email;
                            System.Uri photo_url = user.PhotoUrl;
                            // The user's Id, unique to the Firebase project.
                            // Do NOT use this value to authenticate with your backend server, if you
                            // have one; use User.TokenAsync() instead.
                            string uid = user.UserId;
                            text.text = uid + " - " + name;
                            PlayerPrefs.SetString("uid", uid);
                        } else {
                            text.text = "error: " + user;
                        }
                    }
                });
            }
        });
    }

    public void SignOut() {
        if(auth != null){
            auth.SignOut();
            auth = null;
        } 

        if(signIn != null){
            GoogleSignIn.DefaultInstance.SignOut();
            signIn = null;
        } 
        text.text = "signed out";
    }
}
