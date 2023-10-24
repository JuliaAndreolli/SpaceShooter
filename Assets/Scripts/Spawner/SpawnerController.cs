using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    private float queueTime;
    private float time = 0;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        queueTime = 2f; //quando o jogo inicia os enemigos nascem todos juntos a cada 2 segundos
    }

    // Update is called once per frame
    void Update()
    {
        //change queue time to 5 seconds
        queueTime = 5f;

        if(time > queueTime)
        {
            GameObject gameObject = Instantiate(enemy);
            gameObject.transform.position = transform.position + new Vector3(0, 0, 0);

            time = 0;

            Destroy(gameObject, 18); //destroi o inimigo após 18 segundos, caso o player não o mate
        }                            //mais ou menos o tempo de atravessar a tela, poupa memória

        time += Time.deltaTime;

    }
}
