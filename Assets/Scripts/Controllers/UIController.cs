using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
        [SerializeField] private GameObject _startScreen;
        [SerializeField] private GameObject _gameScreen;
        [SerializeField] private GameObject _loseScreen;
    
        private GameObject _currentScreen;
    
        private void Awake()
        {
            _currentScreen = _startScreen;
        }
    
        public void ShowStartScreen()
        {
            _currentScreen.SetActive(false);
            _startScreen.SetActive(true);
            _currentScreen = _startScreen;
        }
        public void ShowGameScreen()
        {
            _currentScreen.SetActive(false);
            _gameScreen.SetActive(true);
            _currentScreen = _gameScreen;
    
        }
        public void ShowLoseScreen()
        {
            _currentScreen.SetActive(false);
            _loseScreen.SetActive(true);
            _currentScreen = _loseScreen;
        }
        
}
