using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private SpriteRenderer mySpriteRenderer;

    [SerializeField] float screenWidthLimit = 4;

    private bool goingRight;
    
    [HideInInspector] public float direction;
    
    [Header(("Bullet Prefab"))] 
    public GameObject ammo;

    [Header(("Child Anchor"))] 
    public Transform childTransform;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<float>();
        
        if (obj.canceled)
        {
            rb2D.velocity = new Vector2(0, 0);
        }

        if (direction >= 0.1)
        {
            goingRight = true;
            
        }else if (direction <= 0.1)
        {
            goingRight = false;
        }
    }

    public void onShoot(InputAction.CallbackContext obj)
    {

        if (obj.performed)
        {
            Instantiate(ammo, childTransform.position, childTransform.rotation, childTransform.parent.transform);

        }
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalSpeed = 5;
        
        if (direction != 0)
        {
            rb2D.velocity = new Vector2(horizontalSpeed * direction, 0);
        }

        if (rb2D.position.x >= screenWidthLimit && goingRight)
        {
            
            rb2D.velocity = new Vector2(0, 0);

        }else if (rb2D.position.x <= -screenWidthLimit && !goingRight)
        {
            
            rb2D.velocity = new Vector2(0, 0);
            
        }
    }
}
