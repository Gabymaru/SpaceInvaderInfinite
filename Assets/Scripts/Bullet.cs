using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Object = System.Object;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private GameObject myGameObjectparent;
    private InvaderSpawner spawner;
    
    private string myTag;

    [SerializeField] public int playerVerticalSpeed = 10;
    [SerializeField] public int enemyVerticalSpeed = 5;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        var spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
        spawner = spawnerObject.GetComponent<InvaderSpawner>();
        
        rb2D = GetComponent<Rigidbody2D>();
        myTag = transform.parent.gameObject.tag;
        Debug.Log(myTag);

    }

    // Update is called once per frame
    void Update()
    {
        switch (myTag)
        {
            case "Player" : rb2D.velocity = new Vector2(0, playerVerticalSpeed);
                break;
            case "Enemy" : rb2D.velocity = new Vector2(0, enemyVerticalSpeed);
                break;
        }

        if (gameObject.transform.position.y >= 5.25f) OnDestroy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (myTag == "Player" && other.CompareTag("Player") || myTag == "Enemy" && other.CompareTag("Enemy")) return;
        else if (other.CompareTag("Bullet")) OnDestroy();

        other.SendMessage("OnDestroy");

        spawner.activeInvaderList.Remove(other.GetComponent<Invader>());
        
        OnDestroy();
        
        //print(other.name);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}