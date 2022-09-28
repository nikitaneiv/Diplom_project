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

    private GameObject mouse;
    private GameData _gameData;
    private PlayerController _player;

    public PlayerController Player => _player;

    private void Awake()
    {
        UIController.ShowStartScreen();
        _gameData = _saveController.LoadData();
    }

    public void StartGame()
    {
        GenerateMouse();
        _gameData.Score = 0;
        UIController.ShowGameScreen();
        _goldText.text = _gameData.Golds.ToString();
        _bestScoreText.text = _gameData.BestScore.ToString();
        _player.AddGold += AddGold;
        _player.OnDied += Dead;
        StartCoroutine(ScoreCounterCoroutine());
    }

    private void Dead()
    {
        UIController.ShowLoseScreen();
        StopCoroutine(ScoreCounterCoroutine());
        BestScoreSave();
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
        _player= mouse.GetComponent<PlayerController>();
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

