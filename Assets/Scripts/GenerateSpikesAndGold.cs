using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateSpikesAndGold : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefabRight;
    [SerializeField] private GameObject spikePrefabLeft;
    [SerializeField] private GameObject goldPrefabs;

    [SerializeField] private GameSetteng _setting;

    private List<GameObject> spikesLeft = new List<GameObject>();
    private List<GameObject> spikesRight = new List<GameObject>();
    private List<GameObject> goldList = new List<GameObject>();

    Vector3 posLeft = new Vector3 (-1.60f, 10, 0);
    Vector3 posRight = new Vector3 (1.60f, 10, 0);
    Vector3 posGold = new Vector3 (-1.60f, 12, 0);
    
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
        StartLevel();
    }
    
    private void Update()
    {
        if(IsActive != true) return;
        if (_setting.Speed < _setting.MaxSpeed)
        {
            _setting.Speed += 0.00005f;
        }

        foreach (GameObject spike in spikesLeft)
        {
            spike.transform.position -= new Vector3(0 ,_setting.Speed * Time.deltaTime, 0 );
        }
        foreach (GameObject spike in spikesRight)
        {
            spike.transform.position -= new Vector3(0 ,_setting.Speed * Time.deltaTime, 0 );
        }

        foreach (GameObject gold in goldList)
        {
            gold.transform.position -= new Vector3(0, _setting.Speed * Time.deltaTime, 0);
        }

        if (spikesLeft[0].transform.position.y < -5)
        {
            Destroy(spikesLeft[0]);
            spikesLeft.RemoveAt(0);
            CreateSpikes();
        }
        if (spikesRight[0].transform.position.y < -5)
        {
            Destroy(spikesRight[0]);
            spikesRight.RemoveAt(0);
        }

        if (goldList[0].transform.position.y < -5)
        {
            Destroy(goldList[0]);
            goldList.RemoveAt(0);
            CreateGold();
        }
    }
    
    private void ResetLevel()
    {
        _setting.Speed = 0;
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

        while (goldList.Count > 0)
        {
            Destroy(goldList[0]);
            goldList.RemoveAt(0);
        }

        for (int i = 0; i < _setting.MaxSpikesCount; i++)
        {
            CreateSpikes();
        }

        for (int i = 0; i < _setting.MaxGoldsCount; i++)
        {
            CreateGold();
        }
    }
    
    public void StartLevel()
    {
        ResetLevel();
        _setting.Speed = 3f;
    }
    
    private void CreateSpikes()
    {
        var rnd = Random.Range(2, 10);
        
        if (spikesLeft.Count > 0 )
        {
            posLeft = spikesLeft[spikesLeft.Count - 1].transform.position + new Vector3(0, rnd , 0);
        }
        
        GameObject leftSpikes = Instantiate(spikePrefabLeft, posLeft, Quaternion.identity);
        leftSpikes.transform.SetParent(transform);
        spikesLeft.Add(leftSpikes);
        
        if (spikesRight.Count > -1 )
        {
            posRight = posLeft + new Vector3(3.20f, 4, 0);
        }
        
        GameObject rightSpikes = Instantiate(spikePrefabRight, posRight, Quaternion.identity);
        rightSpikes.transform.SetParent(transform);
        spikesRight.Add(rightSpikes);
    }

    private void CreateGold()
    {
        var rndY = Random.Range(2, 10);
        var rndX = Random.Range(0, 3.20f);
        if (goldList.Count > 0 )
        {
            posGold = posLeft + new Vector3(rndX, rndY , 0);
        }
        GameObject Gold = Instantiate(goldPrefabs, posGold, Quaternion.identity);
        Gold.transform.SetParent(transform);
        goldList.Add(Gold);
    }

    public void StartMove()
    {
        _isActive = true;
    }

}
