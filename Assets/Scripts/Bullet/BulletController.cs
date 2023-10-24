using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible() //quando n�o aparece mais na c�mera, destr�i a bala
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!transform.parent.name.Equals(collision.transform.name)) //se uma bala bater em outra bala e 
        {                                                           //n�o for do mesmo pai, o objeto �
            Destroy(gameObject);                                    //destru�do

            if(collision.CompareTag("Player"))                      //se uma bala bater em um jogador, e ele
            {                                                       //n�o for o pai, recebe dano
                collision.GetComponent<PlayerController>().Damage(1);
            }

            if(collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyController>().Damage(1);
            }
            if (collision.CompareTag("Boss"))
            {
                collision.GetComponent<BossController>().Damage(1);
            }
        }
    }
}
