using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private int life = 1;
    public Transform skin;

    public GameObject bulletPreFab;
    public Transform shoot;
    public float shootVelocity = 5f;
    private float moveSpeed = 0.8f;

    private float fireDelay;
    private float time = 0;

    private PlayerController playerController;

    public AudioSource audioSource;
    public AudioClip shootingClip;
    public AudioClip explodeClip;

    // Start is called before the first frame update
    void Start()
    {
        fireDelay = Random.Range(1f, 2.4f);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>(); //usado para instaciar
    }                                                                                  //o script do player

    // Update is called once per frame
    void Update()
    {
        Move();

        if (time > fireDelay)
        {
            Fire();
            time = 0;
        }

        time += Time.deltaTime; //pega o tempo de cada frame ao ser gerado e vai somar, ao passar do delay
    }                           //o inimigo atira, zera a contagem e inicia novamente

    private void Move()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    public void Fire()
    {
        audioSource.PlayOneShot(shootingClip, 1);
        GameObject bullet = Instantiate(bulletPreFab, shoot.position, shoot.rotation);
        bullet.transform.parent = transform;
        bullet.GetComponent<Rigidbody2D>().AddForce(-shoot.up * shootVelocity, ForceMode2D.Impulse);
    }

    public void Damage(int damage)
    {
        life -= damage;
        playerController.scoreUp(10);

        if(life <= 0)
        {
            audioSource.PlayOneShot(explodeClip, 1);
            this.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            skin.GetComponent<Animator>().Play("Die", -1);
            Invoke("OnExplosionAnimationFinished", 1.0f);
        }
    }

    void OnExplosionAnimationFinished()
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
