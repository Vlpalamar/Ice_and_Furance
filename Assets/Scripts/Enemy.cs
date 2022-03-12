using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private List<Transform> transforms;
    private int  transformIndex=0;

    private float navigationTime =0f;
    private float navigationUpdate= 0f;
    [SerializeField] private float myltiply=1f;
    private Transform EnemyPosition;
    void Start()
    {
        this.EnemyPosition= GetComponent<Transform>();
        //print(transforms.Count);
    }

    // Update is called once per frame
    void Update()
    {
        navigationTime+=Time.deltaTime*myltiply;
        if(navigationTime>= navigationUpdate)
        {
            EnemyPosition.position= Vector2.MoveTowards(EnemyPosition.position,transforms[transformIndex].position,navigationTime);

            
           
            navigationTime = 0;
          
        }
         
            
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag==TagManager.ENEMY_POINT)
        {
               transformIndex++;
            if(transformIndex>=transforms.Count) transformIndex=0;
           
 
        }
    }
}
