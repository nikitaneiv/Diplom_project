using UnityEngine;

    public class Health : MonoBehaviour
    {
        public Transform[] _hearts = new Transform[3];
        
        private GameManager _manager;

        private void Awake()
        {
            _manager = FindObjectOfType<GameManager>();
            for (int i = 0; i < _hearts.Length; i++)
            {
                _hearts[i] = transform.GetChild(i);
            }
        }
        public void Refresh()
        {
            for (int i = 0; i < _hearts.Length; i++)
            {
                if (i < _manager.Player.Lives) _hearts[i].gameObject.SetActive(true);
                else _hearts[i].gameObject.SetActive(false);
            }
        }
    }
