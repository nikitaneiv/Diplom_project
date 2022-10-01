using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SaveController;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _goldText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private UIController UIController;
    [SerializeField] private SaveController _saveController;
    [SerializeField] private GameObject mousePrefab;

    //private GameObject mouse;
    private GameData _gameData;
    private PlayerController _player;
    private Health _health;

    public PlayerController Player => _player;

    private GameObject mouse;

    private void Awake()
    {
        _gameData = _saveController.LoadData();
    }

    private void Start()
    {
        UIController.ShowStartScreen();
        GenerateMouse();
        mouse.SetActive(false);
        _player.AddGold += AddGold;
        _player.OnDied += Dead;
        StartCoroutine(ScoreCounterCoroutine());
    }

    public void StartGame()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        _gameData.Score = 0;
        _goldText.text = _gameData.Golds.ToString();
        _bestScoreText.text = _gameData.BestScore.ToString();
        mouse.SetActive(true);
        UIController.ShowGameScreen();
        _player.ReturnLive();
    }

    public void MenuGame()
    {
        UIController.ShowStartScreen();
    }

    private void Dead()
    {
        UIController.ShowLoseScreen();
        StopCoroutine(ScoreCounterCoroutine());
        BestScoreSave();
        mouse.SetActive(false);
        _saveController.SaveData(_gameData);
    }

    public void QuitGame()
    {
        Application.Quit();
        _saveController.SaveData(_gameData);
    }

    private void AddGold()
    {
        _gameData.Golds++;
        _goldText.text = _gameData.Golds.ToString();
    }

    private void GenerateMouse()
    {
        mouse = Instantiate(mousePrefab, transform);
        mouse.transform.localPosition = new Vector3(-1.3f, -2f, 0f);
        _player = mouse.GetComponent<PlayerController>();
        _player.StartComponent();
    }

    private void BestScoreSave()
    {
        if (_gameData.Score > _gameData.BestScore)
        {
            _gameData.BestScore = _gameData.Score;
            _bestScoreText.text = _gameData.BestScore.ToString();
        }
    }

    IEnumerator ScoreCounterCoroutine()
    {
        for (; ;)
        {
            _gameData.Score++;
            yield return new WaitForSeconds(1f);
            _scoreText.text = _gameData.Score.ToString();
        }
    }
}

