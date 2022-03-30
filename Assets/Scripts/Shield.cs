using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private EdgeCollider2D myCollider2D;
    public int actualLifePoints = 3;
    private SpriteRenderer mySprite;
    [SerializeField] public Sprite[] spriteArray;


    // Start is called before the first frame update
    void Awake()
    {
        myCollider2D = GetComponent<EdgeCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
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
