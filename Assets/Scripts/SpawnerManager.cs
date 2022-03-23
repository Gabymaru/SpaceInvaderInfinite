using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField] private Transform[] spawnerList;
    [SerializeField] private GameObject[] invadersToSpawn;
    [SerializeField] private List<GameObject> invadersList = new List<GameObject>();
    [SerializeField] private GameObject spawnerParent;



    //private int spawnerListMaxIndex = 10;
    private int InvaderType = 0;

    private bool isFirstRaw = true;
    private bool isSecondRaw = false;

    /*[HideInInspector] public Dictionary<int, Invaders>[] invadersDict;*/

    /*private Transform childToSpawnIn;*/

    
    void Awake()
    {
        //Temporaire, le temps de setup un vrai start
        onSpawn();
    }
    
    

    void onSpawn()
    {
        for (int i = 0; i < spawnerList.Length; i++)
        {
            GameObject spawnedInvader = Instantiate(invadersToSpawn[InvaderType], spawnerList[i].transform);
            spawnedInvader.gameObject.transform.SetParent(spawnerParent.transform);
            invadersList.Add(spawnedInvader);
            
            if (isFirstRaw)
            {
                invadersList[i].SendMessage("OnMove", value:3);
                if (i == spawnerList.Length)
                {
                    isFirstRaw = false;
                    isSecondRaw = true;
                }
            }
            else if (isSecondRaw)
            {
                invadersList[i].SendMessage("OnMove", value:2);
                if (i == spawnerList.Length)
                {
                    isSecondRaw = false;
                }
            } else invadersList[i].SendMessage("OnMove", value:0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
