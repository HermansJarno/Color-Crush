using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public Level LoadLevel(int level){
        Level newLevel = new Level(level, 20, new BlockType[4,2]);
        newLevel.AddEntryToLevelGoals(BlockType.Color, 1);
        newLevel.AddEntryToLevelGoals(BlockType.Ice, 20);
        newLevel.AddEntryToColorGoals(ColorType.Green, 10);
        newLevel.AddEntryToColorGoals(ColorType.Red, 5);
        return newLevel;
    }

    /*
        public void SaveFile(int level, int numberOfMoves, int[,] hives, List<int> targets, List<int> scoreTargets)
     {
         string destination = "D:/Dev/Mathive/Mathive/Assets/Resources/Levels/level" + level.ToString() +  ".bytes";
         FileStream file;
 
         if(File.Exists(destination)) file = File.OpenWrite(destination);
         else file = File.Create(destination);
 
         LevelData data = new LevelData(numberOfMoves, targets, hives, scoreTargets);
         BinaryFormatter bf = new BinaryFormatter();
         bf.Serialize(file, data);
         file.Close();
     }
 
     public LevelData LoadLevel(int levelNumber)
     {
         string destination = Application.persistentDataPath + "/level" + levelNumber.ToString() + ".dat";
         Debug.Log(destination);
         FileStream file;
 
         if(File.Exists(destination)) file = File.OpenRead(destination);
         else
         {
             Debug.LogError("File not found");
             return null;
         }
 
         BinaryFormatter bf = new BinaryFormatter();
         LevelData data = (LevelData) bf.Deserialize(file);
         file.Close();
 
         return data;
     }

     public LevelData LoadLevelFromResources(int levelNumber){
        string level = "Levels/level" + levelNumber.ToString();
        TextAsset textAsset = Resources.Load(level) as TextAsset;
        byte[] data = textAsset.bytes;

        Stream stream = new MemoryStream(data);
        BinaryFormatter formatter = new BinaryFormatter();
        LevelData myInstance = formatter.Deserialize(stream) as LevelData;

        return myInstance;
     }*/
}
