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

    private void OnBecameInvisible() //quando não aparece mais na câmera, destrói a bala
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!transform.parent.name.Equals(collision.transform.name)) //se uma bala bater em outra bala e 
        {                                                           //não for do mesmo pai, o objeto é
            Destroy(gameObject);                                    //destruído

            if(collision.CompareTag("Player"))                      //se uma bala bater em um jogador, e ele
            {                                                       //não for o pai, recebe dano
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
