using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private EdgeCollider2D myCollider2D;
    private int actualLifePoints = 3;
    private SpriteRenderer mySprite;
    [SerializeField] public Sprite[] spriteArray;


    // Start is called before the first frame update
    void Awake()
    {
        myCollider2D = GetComponent<EdgeCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D bullet)
    {
        /*//Debug.Log("gotTouched");
        
        bullet.GetComponent<Bullet>().OnDestroy();
            
        actualLifePoints -= 1;
        
        //Debug.Log(actualLifePoints);
        
        if (actualLifePoints <= 0)
        {
            OnDestroy();
        } else mySprite.sprite = spriteArray[actualLifePoints - 1];*/
    }

    private void OnDestroy()
    {
        actualLifePoints -= 1;

        if (actualLifePoints <= 0)
        {
            Debug.Log("ShieldDead");
            Destroy(gameObject);
        }
        else mySprite.sprite = spriteArray[actualLifePoints - 1];
    }
}
