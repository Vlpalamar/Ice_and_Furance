using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    Animator animator;
    [SerializeField] private float jumpForce;
    public float JumpForce
    {
        get { return jumpForce; }
    }

   
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoAnim()
    {
        print("anim");
        animator.SetTrigger("Use");
        //anim
    }
}
