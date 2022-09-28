using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private const string _dataKey = "GAME_DATA";
    public class GameData
    {
        public int Golds = 0;
        public int Score = 0;
        public int BestScore = 0;
    }

    public GameData LoadData()
    {
        if (PlayerPrefs.HasKey(_dataKey))
        {
            return JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(_dataKey));
        }
        else
        {
            return new GameData();
        }
    }

    public void SaveData(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(_dataKey, json);
    }
}
