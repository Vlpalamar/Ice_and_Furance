using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private  Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        rb.isKinematic= true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        switch (other.collider.tag)
        {
            case TagManager.PLAYER:
            other.collider.GetComponent<Character>().Death();
            break;

            case TagManager.GROUND:
            TouchTheGround();
            break;
            
            default:
            break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag)
        {
            case TagManager.PLAYER:
            DetectPlayer();
            break;
            
            default:
            break;
        }
    }
    private void DetectPlayer()
    {
        rb.isKinematic= false;

    }
    private void TouchTheGround()
    {
        //анимация 
        Destroy(this.gameObject);
    }
}
