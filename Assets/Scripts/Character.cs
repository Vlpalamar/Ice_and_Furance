using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    #region references  
    private Rigidbody2D rb;
    #endregion
    
    private float horizontalMove;
    Vector2 targetVelocity= new Vector2();
   
    
    [SerializeField] private float speed;

    void Start()
    {
        this.rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove= Input.GetAxis("Horizontal")*speed*10;
        
    }

    void FixedUpdate()
    {
       
        targetVelocity.x= horizontalMove*Time.fixedDeltaTime;
        targetVelocity.y= rb.velocity.y;
        rb.velocity=targetVelocity;
        
    }
}
