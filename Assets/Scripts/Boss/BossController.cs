using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    private int life = 25;
    public Transform skin;

    public GameObject bulletPreFab;
    public Transform shoot;
    public Transform shoot1;
    public float shootVelocity = 5f;
    private float moveSpeed = 0.8f;

    private float fireDelay;
    private float time = 0;

    private PlayerController playerController;
    public GameObject victoryScreen;

    public AudioSource audioSource;
    public AudioClip shootingClip;
    public AudioClip explodeClip;

    // Start is called before the first frame update
    void Start()
    {
        fireDelay = 1.3f;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        Move();

        if (time > fireDelay)
        {
            Fire(shoot);
            Fire(shoot1);
            time = 0;
        }

        time += Time.deltaTime;
    }

    private void Move()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    public void Fire(Transform shoot)
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

        if (life <= 0)
        {
            audioSource.PlayOneShot(explodeClip, 1);
            this.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            skin.GetComponent<Animator>().Play("die", -1);
            Invoke("OnExplosionAnimationFinished", 1.0f);
            victoryScreen.SetActive(true);
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
