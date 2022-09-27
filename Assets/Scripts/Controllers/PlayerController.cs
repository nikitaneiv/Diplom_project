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
        public event Action AddGold;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            targetPos = transform.position;
            SwipeController.MoveEvent += MovePlayer;
        }

        private void Update()
        {
            _animatorController.SetFlyTrigger();
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
                Debug.Log("- 1 Life");
            }
            if (other.gameObject.GetComponent<Gold>())
            {
                AddGold?.Invoke();
                Debug.Log("+ 1 Gold");
            }
        }
        
    }




