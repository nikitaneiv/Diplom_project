using System;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Vector2 startTouch;
    private bool touchMoved;
    private Vector2 swipeDelta;

    private const float SWIPE_THRESHOLD = 50;

    public enum Direction { Left, Right };

    private bool[] swipe = new bool[2];

    public delegate void MoveDelegate(bool[] swipes);

    public static MoveDelegate MoveEvent;
    
    public delegate void ClickDelegate(Vector2 pos);

    public ClickDelegate ClickEvent;

    Vector2 TouchPosition() { return (Vector2)Input.mousePosition; }
    bool TouchBegan() { return Input.GetMouseButtonDown(0); }
    bool TouchEnded() { return Input.GetMouseButtonUp(0); }
    bool GetTouch() { return Input.GetMouseButton(0); }
    private void Update()
    {
        // Начало и завершение свайпа
        if (TouchBegan())
        {
            startTouch = TouchPosition(); // определяем начало касания, запоминаем его позицию
            touchMoved = true;            // начали движение
        }
        else if (TouchEnded() && touchMoved == true)
        {
            SendSwipe();
            touchMoved = false;
        }
        
        // Измерение дистанции
        swipeDelta = Vector2.zero;
        if (touchMoved && GetTouch())
        {
            swipeDelta = TouchPosition() - startTouch;
        }
        
        // Проверка свайпа
        if (swipeDelta.magnitude > SWIPE_THRESHOLD)
        {
            if (Math.Abs(swipeDelta.x) > Math.Abs(swipeDelta.y))
            {
                // Лево-Право
                swipe[(int)Direction.Left] = swipeDelta.x < 0;
                swipe[(int)Direction.Right] = swipeDelta.x > 0;
            }
            else
            {
                // Вверх-вниз
            }
            SendSwipe();
        }

    }

    private void SendSwipe()
    {
        if (swipe[0] || swipe[1])
        {
            //Debug.Log(swipe[0] + "|" + swipe[1] );
            MoveEvent?.Invoke(swipe);
        }
        else
        {
            Debug.Log("Click");
            ClickEvent?.Invoke(TouchPosition());
        }
        
        Reset();
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        touchMoved = false;
        for (int i = 0; i < 2; i++)
        {
            swipe[i] = false;
        }
    }
}

