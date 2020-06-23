using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using System.Threading.Tasks;

public class PlayerSaveManager 
{
    private const string databaseUrl = "https://color-crush-35ba8.firebaseio.com/";
    private const string PLAYER_KEY = "PLAYER_KEY";

    /*
    public void SavePlayer(PlayerData playerData){
        _database.GetReference(PLAYER_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(playerData));
    }*/

    public async Task<PlayerData?> LoadPlayer(){
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(databaseUrl);

        var dataSnapshot = await FirebaseDatabase.DefaultInstance.GetReference("PLAYER_KEY").GetValueAsync();
        if(!dataSnapshot.Exists){
            return null;
        }

        return JsonUtility.FromJson<PlayerData>(dataSnapshot.GetRawJsonValue());
    }

    
    public async Task<bool> SaveExists() {
        bool saveExists = false;
        
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(databaseUrl);
        await FirebaseDatabase.DefaultInstance.GetReference("PLAYER_KEY").GetValueAsync().ContinueWith(query => {
            saveExists = query.Result.Exists;
            Debug.Log($"Save exists : {saveExists}");
        });
        return saveExists;
    }
}
