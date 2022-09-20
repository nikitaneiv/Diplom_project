using System;
using DG.Tweening;
using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        private Vector2 targetPos;
        private float rightPos = 1.3f;
        private float leftPos = -1.3f;
        private float positionY = -2f;
        private float duration = 0.3f;
        private float midPos = 1; 

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            targetPos = transform.position;
            SwipeController.MoveEvent += MovePlayer;
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
        }

        private void SwipeLeftMove()
        {
            transform.DOMove(targetPos = new Vector2(leftPos, positionY), duration).SetEase(Ease.Linear);
        }
        private void SwipeRightMove()
        {
            transform.DOMove(targetPos = new Vector2(rightPos, positionY), duration).SetEase(Ease.Linear);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Spikes>())
            {
                Debug.Log("- 1 Life");
            }
        }
    }


