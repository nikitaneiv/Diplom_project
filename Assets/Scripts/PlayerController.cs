using System;
using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        private Vector2 targetPos;
        private float laneOffset = 2.6f;
        private float laneChangeSpeed = 15;
        private float wallDistance = 1; // значение, чтобы не выйти за стену

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            targetPos = transform.position;
            SwipeController.MoveEvent += MovePlayer;
        }
        
        private void MovePlayer(bool[] swipes)
        {
            if (swipes[(int)SwipeController.Direction.Left] && targetPos.x > - wallDistance)
            {
                targetPos = new Vector2(targetPos.x - laneOffset, transform.position.y);
                _spriteRenderer.flipX = false;
            }
            if (swipes[(int)SwipeController.Direction.Right] && targetPos.x < wallDistance)
            {
                targetPos = new Vector2(targetPos.x + laneOffset, transform.position.y);
                _spriteRenderer.flipX = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, targetPos, laneChangeSpeed * Time.deltaTime);
        
        }
        
            
    }


