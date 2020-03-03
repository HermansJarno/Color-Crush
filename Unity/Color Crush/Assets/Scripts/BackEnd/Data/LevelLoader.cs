using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
        Destroy(this.gameObject);
    }

    private void LoadLevel(){
        // get data from level data
        GameObject.FindObjectOfType<LevelManager>().SetMoves(20);
    }
}
