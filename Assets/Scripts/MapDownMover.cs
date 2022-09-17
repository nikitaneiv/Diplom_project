using System;
using UnityEngine;

public class MapDownMover : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
