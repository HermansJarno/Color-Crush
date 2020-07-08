using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

    public PlayerData PlayerData => _playerData;
    public string Name => _playerData.playerName;
    public int NumberOfLifes => _playerData.numberOfLifes;
    public int CurrentLevel => _playerData.currentLevel;

    public PlayerSaveManager playerSaveManager = new PlayerSaveManager();

    private Coroutine _coroutine;

    private void Start() {
        if(_coroutine == null){
            _coroutine = StartCoroutine(LoadPlayer());
        }
    }

    public void UpdatePlayer(PlayerData playerData){
        if(!playerData.Equals(_playerData)){
            _playerData = playerData;
            Debug.Log($"Player name : {_playerData.playerName}");
            Debug.Log($"Player's number of lifes : {_playerData.numberOfLifes}");
        }
    }
    
    public IEnumerator LoadPlayer(){
        var saveExistsTask = playerSaveManager.SaveExists();
        yield return new WaitUntil(() => saveExistsTask.IsCompleted);
        if(saveExistsTask.Result){
            var playerDataLoadTask = playerSaveManager.LoadPlayer();
            yield return new WaitUntil(() => playerDataLoadTask.IsCompleted);
            var playerData = playerDataLoadTask.Result;
        
            if(playerData.HasValue){
                UpdatePlayer(playerData.Value);
            }
        }

        _coroutine = null;
    }
}
