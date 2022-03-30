using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Invader : MonoBehaviour
{

    public int score = 100;

    [HideInInspector] public bool isInStartingAnimation;
    [HideInInspector] public bool isWaveGoing;

    private Transform myTransform;
    private InvaderSpawner invaderSpawner;

    private float posX;
    private float posY;

    private int numberOfTimesToGoDown = 2;
    private int numberOfTimesToGoSideway = 3;
    
    private bool canContinueStartingAnimation;
    private bool isGoingRight;
    private bool canContinue = true;
    private bool isInCoroutine;
    private bool hasWaitedAtStart;
    private bool hasAnimationAlreadyBeenDone;
    
    // Start is called before the first frame update
    void Awake()
    {
        myTransform = GetComponent<Transform>();

        invaderSpawner = GetComponent<InvaderSpawner>();

        posX = myTransform.position.x;
        posY = myTransform.position.y;
    }

    private void Start()
    {
        if (isInStartingAnimation)
        {
            StartCoroutine(OnStartCoroutine(1f));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isInStartingAnimation && hasWaitedAtStart && canContinue && !hasAnimationAlreadyBeenDone)
        {
            OnStartingAnimationMove();
        }else if (isWaveGoing && canContinue)
        {
            onMove();
        }
    }

    void OnStartingAnimationMove()
    {
        if (numberOfTimesToGoDown > 0 && !isInCoroutine)
        {
            canContinue = false;

            posY -= 1;
            myTransform.position = new Vector3(posX, posY, 0);

            numberOfTimesToGoDown -= 1;

            Debug.Log(numberOfTimesToGoDown);

            StartCoroutine(OnCoroutine(1f));
        }
        else
        {
            hasAnimationAlreadyBeenDone = true;
            isInStartingAnimation = false;
            canContinue = true;
            isWaveGoing = true;
        }
    }

    void onMove()
    {
        Debug.Log("is OnMove");

        if (!isInCoroutine)
        {
            if (numberOfTimesToGoSideway > 0)
            {
                if (isGoingRight)
                {
                    posX += 0.5f;
                }
                else posX -= 0.5f;
                
                myTransform.position = new Vector3(posX, posY, 0);
                numberOfTimesToGoSideway -= 1;
                
                StartCoroutine(OnCoroutine(1f));
            }
            else
            {
                numberOfTimesToGoSideway = 6;

                posY -= 1;
                myTransform.position = new Vector3(posX, posY, 0);
                
                StartCoroutine(OnCoroutine(1f));
                
                if (isGoingRight)
                {
                    isGoingRight = false;
                }
                else isGoingRight = true;
            }
        }
    }
    
    IEnumerator OnCoroutine(float timeToWait)
    {
        //Debug.Log("Started Coroutine at timestamp in StartWave : " + Time.time);

        isInCoroutine = true;

        yield return new WaitForSeconds(timeToWait);

        canContinue = true;
        isInCoroutine = false;

        //Debug.Log("Finished Coroutine at timestamp in StartWave: " + Time.time);
    }

    IEnumerator OnStartCoroutine(float timeToWait)
    {
        isInCoroutine = true;
        
        yield return new WaitForSeconds(timeToWait);

        hasWaitedAtStart = true;
        
        isInCoroutine = false;
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
