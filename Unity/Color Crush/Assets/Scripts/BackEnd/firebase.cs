using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Analytics;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;

public class firebase : MonoBehaviour
{
    private const string databaseUrl = "https://color-crush-35ba8.firebaseio.com/";

    private void Start() {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

            if(task.Exception != null){
                Debug.LogError($"Firebase failed with exception : {task.Exception}");
                return;
            }

            if(task.IsCompleted){
                FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(databaseUrl);

                FirebaseDatabase.DefaultInstance.GetReference("PLAYER_KEY").GetValueAsync().ContinueWith(query => {
                    if(query.IsFaulted) {
                        Debug.LogError(query.Exception);
                    }else if(query.IsCompleted) {
                        DataSnapshot snapshot = query.Result;
                        PlayerData playerData = JsonUtility.FromJson<PlayerData>(snapshot.GetRawJsonValue());
                    }
                });
            }
        });
    }
}
