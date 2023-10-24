using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int life = 5;
    private int playerScore = 0;
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    public Transform skin;

    Vector2 moveDirection;

    public GameObject bulletPreFab;
    public Transform shoot;
    public float shootVelocity = 15f;

    public Text playerLife;
    public Text playerScoreText;

    public GameObject gameOverScreen;

    public string currentPhase;

    public AudioSource audioSource;
    public AudioClip shootingClip;
    public AudioClip explodeClip;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject); 
        currentPhase = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentPhase.Equals(SceneManager.GetActiveScene().name)) // verifica se o nome da fase
        {                                                             // é diferente da fase inicial
            playerScore = 0;                                          // Ao mudar de fase reseta pontos e vida
            life = 5;

            currentPhase = SceneManager.GetActiveScene().name;   
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            Fire();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;

        playerLife.text = "x" + life.ToString();
        playerScoreText.text = playerScore.ToString() + " pontos";

        if(SceneManager.GetActiveScene().name.Equals("Phase1") && playerScore >= 100)
        {
            SceneManager.LoadScene("Phase2");
        }
    }

    private void FixedUpdate() //roda a cada 0.02 seg, usado p/ cálculos precisos
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void Fire()
    {
        audioSource.PlayOneShot(shootingClip, 1);
        GameObject bullet = Instantiate(bulletPreFab, shoot.position, shoot.rotation);
        bullet.transform.parent = transform;
        bullet.GetComponent<Rigidbody2D>().AddForce(shoot.up * shootVelocity, ForceMode2D.Impulse);
    }

    public void Damage(int damage)
    {
        skin.GetComponent<Animator>().Play("damage", -1);
        life -= damage;

        if (life <= 0)
        {
            audioSource.PlayOneShot(explodeClip, 1);
            this.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            skin.GetComponent<Animator>().Play("die", -1);
            Invoke("OnExplosionAnimationFinished", 0.5f);
        }
    }

    void OnExplosionAnimationFinished()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    public void scoreUp(int points)
    {
        playerScore += points;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Damage(1);
            collision.GetComponent<EnemyController>().Damage(1);
        }
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
