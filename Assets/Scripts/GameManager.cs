using System;
using UnityEngine;
using UnityEngine.UI;
using static SaveController;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _goldText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private UIController UIController;
    [SerializeField] private SaveController _saveController;
    [SerializeField] private GameObject mousePrefab;
    
    private GameObject mouse;
    private GameData _gameData;
    private PlayerController _player;
    private GenerateRoad _generateRoad;
    private GenerateSpikesAndGold _generateSpikesAndGold;
    
    private int maxScore = 5000;

    public PlayerController Player => _player;

    private void Awake()
    {
        UIController.ShowStartScreen();
        _gameData = _saveController.LoadData();
    }

    private void Start()
    {
        GenerateMouse();
    }

    public void StartGame()
    {
        UIController.ShowGameScreen();
        _goldText.text = _gameData.Golds.ToString();
        _player.AddGold += AddGold;
        _player.OnDied += Dead;
        //AddScore();
        //_scoreText.text = _gameData.Score.ToString();
    }

    private void Dead()
    {
        UIController.ShowLoseScreen();
        //_saveController.SaveData(_gameData);
    }
    
    private void AddGold()
    {
        _gameData.Golds++;
        _goldText.text = _gameData.Golds.ToString();
    }

    // private void AddScore()
    // {
    //     for (_gameData.Score = 0; _gameData.Score < maxScore; _gameData.Score++)
    //     {
    //         _scoreText.text = _gameData.Score.ToString();
    //     }
    //     
    // }
    
    private void GenerateMouse()
    {
        mouse = Instantiate(mousePrefab, transform);
        mouse.transform.localPosition = new Vector3(-1.3f, -2f, 0f);
        _player= mouse.GetComponent<PlayerController>();
    }
}
