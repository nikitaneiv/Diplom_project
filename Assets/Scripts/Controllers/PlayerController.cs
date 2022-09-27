using System;
using DG.Tweening;
using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private AnimatorController _animatorController;

        private Vector2 targetPos;
        
        private float rightPos = 1.3f;
        private float leftPos = -1.3f;
        private float positionY = -2f;
        private float duration = 0.3f;
        private float midPos = 1; 

        private SpriteRenderer _spriteRenderer;
        private Health _health;
        
        public event Action AddGold;
        public event Action OnDied;
        
        private int lives = 3;
        public int Lives
        {
            get { return lives; }
            set
            {
                if (value < 3) lives = value;
                _health.Refresh();
            }
        }
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            targetPos = transform.position;
            SwipeController.MoveEvent += MovePlayer;
        }

        private void Update()
        {
            _animatorController.SetFlyTrigger();
            _health = FindObjectOfType<Health>();
        }

        private void MovePlayer(bool[] swipes)
        {
            if (swipes[(int)SwipeController.Direction.Left] && targetPos.x > - midPos)
            {
                SwipeLeftMove();
                _spriteRenderer.flipX = false;
            }

            if (swipes[(int)SwipeController.Direction.Right] && targetPos.x < midPos)
            {
                SwipeRightMove();
                _spriteRenderer.flipX = true;
            }
            
            if (swipes[(int)SwipeController.Direction.Down] && targetPos.y < -midPos)
            {
                SwipeDown();
            }
            
            if (swipes[(int)SwipeController.Direction.Up] && targetPos.y < midPos)
            {
                SwipeUp();
            }
            
        }

        private void SwipeLeftMove()
        {
            transform.DOMove(targetPos = new Vector2(leftPos, positionY), duration).SetEase(Ease.Linear);
        }
        private void SwipeRightMove()
        {
            transform.DOMove(targetPos = new Vector2(rightPos, positionY), duration).SetEase(Ease.Linear);
        }

        private void SwipeDown()
        {
            _animatorController.SetSwipeDownTrigger();
        }

        private void SwipeUp()
        {
            _animatorController.SetBoostTrigger();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Spikes>())
            {
                Died();
            }
            if (other.gameObject.GetComponent<Gold>())
            {
                AddGold?.Invoke();
                Debug.Log("+ 1 Gold");
            }
        }

        private void Died()
        {
            Lives--;
            if (lives == 0)
            {
                OnDied?.Invoke();
                Debug.Log("Game Over");
            }
        }
    }




