using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Invaders : MonoBehaviour
{

    [SerializeField] public int score = 100;
    [SerializeField] public int speed = 1;

    [HideInInspector] public BoxCollider2D myBoxCollider2D;
    [HideInInspector] public List<Invaders>[] invadersList;

    private bool isGoingRight;
    private bool isStartingFinished;
    private bool canContinue = true;
    private bool canSpawn;

    private Transform invadersSceneParent;
    private Transform actualparent;
    private Transform myTransform;
    
    private float posX;
    private float posY;

    /*private int iterationsToGoDown = 3;*/
    private int iterationsToGoDown = 3;
    private int actualIterationsToGoDown;

    // Start is called before the first frame update
    void Awake()
    {
        myTransform = GetComponent<Transform>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();

        posX = transform.position.x;
        posY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(int rawBeforeAutomation)
    {
        if (canContinue)
        {
            Debug.Log("bonsoir");
            canContinue = false;
            if (iterationsToGoDown >= 0)
            {
                myTransform.position = new Vector3( posX, posY,0);
                posY -= 0.5f;
                
                actualIterationsToGoDown = iterationsToGoDown - 1;
                iterationsToGoDown = actualIterationsToGoDown;
                
                StartCoroutine(OnCoroutineStartWave(1f));

                canSpawn = true;
            }
        }
    }

   
    
    IEnumerator OnCoroutineStartWave(float timeToWait)
    {
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds(timeToWait);
        canContinue = true;
        OnMove(actualIterationsToGoDown);

        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
