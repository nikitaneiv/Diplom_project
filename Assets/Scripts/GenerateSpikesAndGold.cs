using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateSpikesAndGold : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefabRight;
    [SerializeField] private GameObject spikePrefabLeft;
    
    private List<GameObject> spikesLeft = new List<GameObject>();
    private List<GameObject> spikesRight = new List<GameObject>();
    
    private int maxSpikesCount = 8;
    private float speed = 0;
    private float maxSpeed = 2;
    
    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        ResetLevel();
        StartLevel();
    }
    
    private void Update()
    {
        if (speed == 0) return;

        foreach (GameObject spike in spikesLeft)
        {
            spike.transform.position -= new Vector3(0 ,speed * Time.deltaTime, 0 );
        }
        foreach (GameObject spike in spikesRight)
        {
            spike.transform.position -= new Vector3(0 ,speed * Time.deltaTime, 0 );
        }

        if (spikesLeft[0].transform.position.y < -5)
        {
            Destroy(spikesLeft[0]);
            spikesLeft.RemoveAt(0);
            CreateLeftSpike();
        }
        if (spikesRight[0].transform.position.y < -5)
        {
            Destroy(spikesRight[0]);
            spikesRight.RemoveAt(0);
            CreateRightSpike();
        }
    }
    
    public void ResetLevel()
    {
        speed = 0;
        while (spikesLeft.Count > 0)
        {
            Destroy(spikesLeft[0]);
            spikesLeft.RemoveAt(0);
        }
        while (spikesRight.Count > 0)
        {
            Destroy(spikesRight[0]);
            spikesRight.RemoveAt(0);
        }

        for (int i = 0; i < maxSpikesCount; i++)
        {
            CreateLeftSpike();
            CreateRightSpike();
            
        }
    }
    
    private void StartLevel()
    {
        speed = maxSpeed;
    }
    
    private void CreateLeftSpike()
    {
       
        Vector3 posLeft = new Vector3 (-1.60f, 2, 0);
        
        if (spikesLeft.Count > 0)
        {
            posLeft = spikesLeft[spikesLeft.Count - 1].transform.position + new Vector3(0, 4, 0);
        }
        
        GameObject go = Instantiate(spikePrefabLeft, posLeft, Quaternion.identity);
        go.transform.SetParent(transform);
        spikesLeft.Add(go);
    }
    private void CreateRightSpike()
    {
        Vector3 posRight = new Vector3 (1.60f, 4, 0);
       
        if (spikesRight.Count > 0 && spikesRight.Count < maxSpikesCount)
        {
            posRight = spikesRight[spikesRight.Count - 1].transform.position + new Vector3(0, 4, 0);
        }
        
        GameObject go = Instantiate(spikePrefabRight, posRight, Quaternion.identity);
        go.transform.SetParent(transform);
        spikesRight.Add(go);
    }
    
    // private void GenerateWall()
    // {
    //     float roadLenght = 10;
    //     float currentLenght = 3;
    //     float startX = 2;
    //     float xOffset = roadLenght / 2f;
    //     
    //     while (xOffset < roadLenght)
    //     {
    //         int intWallCount = 6;
    //         int intWallPositionY = UnityEngine.Random.Range(0, 6);
    //         if (intWallPositionY == 1)
    //         {
    //             intWallCount = 3;
    //         }
    //         if (intWallPositionY == 0)
    //         {
    //             intWallCount = 1;
    //         }
    //         for (int intWallPosition = intWallPositionY; intWallPosition < intWallCount; intWallPosition++)
    //         {
    //
    //             float positionX = startX + intWallPosition * xOffset;
    //
    //             Vector3 localPosition = new Vector3(positionX, 0f, currentLenght);
    //
    //
    //             GameObject go = Instantiate(spikePrefabLeft, localPosition, Quaternion.identity);
    //             go.transform.SetParent(transform);
    //             spikesLeft.Add(go);
    //         }
    //         currentLenght += UnityEngine.Random.Range(2, 4);
    //     }
    // }
}
