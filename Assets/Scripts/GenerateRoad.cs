using System;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoad : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    
    private List<GameObject> roads = new List<GameObject>();
    
    private int maxRoadCount = 3;
    private float speed = 0;
    private float maxSpeed = 5f;

    private bool _isActive = false;
    
    public bool IsActive
    {
        get
        {
            return _isActive;
        }

        set
        {
            if (value == true);
            _isActive = value;
        }
    }
    private void Start()
    {
        ResetLevel();
        StartLevel();
    }

    private void Update()
    {
        if (speed == 0) return;
        if(IsActive != true) return;

        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0,speed * Time.deltaTime, 0 );
        }

        if (roads[0].transform.position.y < -10)
        {
           Destroy(roads[0]);
           roads.RemoveAt(0);
           
           CreateNextRoad();
        }
    }

    public void ResetLevel()
    {
        speed = 0;
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }

        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }
    private void StartLevel()
    {
        speed = 3;
    }

    private void CreateNextRoad()
    {
        Vector2 pos = Vector2.zero;
        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 10, 0);
        }
        
        GameObject go = Instantiate(roadPrefab, pos, Quaternion.identity);
       go.transform.SetParent(transform);
       roads.Add(go);
    }
    
    public void StartMove()
    {
        _isActive = true;
    }
}
