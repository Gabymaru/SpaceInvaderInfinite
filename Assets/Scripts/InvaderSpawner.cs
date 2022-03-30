using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class InvaderSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject spawnerParent;
    [SerializeField] public List<Invader> activeInvaderList = new List<Invader>();

    public GameObject[] InvadersTypeList;

    public Transform startingSpawn;

    private bool canStart;
    
    [HideInInspector] public bool isStartingWave;

    private int maxInvadersPerRow = 10;
    private int invaderType = 0;
    
    private float initialPosX;
    private float initialPosY;
        
    private float posX;
    private float posY;
    
    
    [HideInInspector] public List<Invader>[] invadersList;
    
    // Start is called before the first frame update
    void Awake()
    {
        canStart = true;
        
        initialPosX= startingSpawn.transform.position.x;
        initialPosY= startingSpawn.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart)
        {
            onSpawn(60);
        }
    }

    public void onSpawn(int howMany)
    {
        posX = initialPosX;
        posY = initialPosY;
        maxInvadersPerRow = 10;

        for (int i = 0; i < howMany; i++)
        {
            if (i == maxInvadersPerRow)
            {
                posX = initialPosX;
                posY += 1f;
                startingSpawn.position = new Vector3(posX, posY, 0);
                maxInvadersPerRow += 10;
            }
            
            GameObject spawnedInvader = Instantiate(InvadersTypeList[invaderType], startingSpawn.transform);
            spawnedInvader.gameObject.transform.SetParent(spawnerParent.transform);
            
            activeInvaderList.Add(spawnedInvader.GetComponent<Invader>());

            activeInvaderList[i].isInStartingAnimation = true;
            
            
            posX += 0.8f;
            startingSpawn.position = new Vector3(posX, posY, 0);
        }
        canStart = false;
    }
}
