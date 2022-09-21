using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private Animator animator;

    public int energia;
    private int PowerUpid;

    public GameObject Player;
    Movement movimentar = new Movement();


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        int num = Random.Range(0,3);
        PowerUpid = num;
        movimentar = Player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (energia <= 0)
        {
            //animator.SetTrigger("death");
            Invoke("DestroyBody", .0f);

        }

    }

    public void TakeDamage(int damage)
    {
        energia -= damage;
    }

    private void DestroyBody()
    {

        switch (PowerUpid)
        {
            case 0:
                movimentar.IncreaseDmg();
                break;
            case 1:
                movimentar.IncreaseLife();
                break;
            case 2:
                movimentar.IncreaseMagic();
                break;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Verifica colisao
        if (collision.gameObject.tag == "Player")
        {
            energia = energia - 1;
        }
    }
}
