using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    //Player
    private Rigidbody2D player;
    //Mov
    private float moveSpeed;
    private float jumpSpeed;
    private float horizontalMove;
    private float verticalMove;

    //Player Action
    private bool isJumping;
    private bool cooldownJump;
    private bool isAttacking;
    private bool isMagic;
    private bool facingRight = true;
    public bool jumpStop = false;


    //Anim
    public RuntimeAnimatorController runAnim;
    public RuntimeAnimatorController idleAnim;
    public RuntimeAnimatorController jumpAnim;
    public RuntimeAnimatorController attackAnim;
    public RuntimeAnimatorController attackAnim2;
    public RuntimeAnimatorController castAnim;
    private Material playerMat;

    //Projectile
    public GameObject Blast;
    public GameObject Blast2;
    public GameObject Slash;
    public GameObject Slash2;
    public Transform armaParado;

    public  int weapon = 1;
    public int magic = 1;

    public Joystick joystick;

    //Hearts
    public Color corCheia;
    public Color corVazia;
    public Color corDano;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    private Material MHeart1;
    private Material MHeart2;
    private Material MHeart3;
    private int Imortal;

    public int Health = 3;

    public int ScoreValue;

    float larguraTela;

    //GameOver
    public GameObject Menu;

    // Start is called before the first frame update
    void Start() { 
        
        player = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 0.2f;
        jumpSpeed = 40f;
        isJumping = false;
        isMagic = false;
        isAttacking = false;
        Imortal = 0;
        ScoreValue=0;
        cooldownJump=false;

        playerMat = player.GetComponent<Renderer>().material;
        Menu.SetActive(false);

        larguraTela = Camera.main.orthographicSize*2f;

    }

    // Update is called once per frame
    void Update() {

        if(player.transform.position.x>larguraTela){
            this.transform.position = new Vector2(-9,player.transform.position.y);
        }
        if(player.transform.position.x<-larguraTela){
            this.transform.position = new Vector2(9,player.transform.position.y);
        }

        //Velocidade movimento com base no joystick
        horizontalMove = joystick.Horizontal;
        verticalMove = joystick.Vertical;

        MHeart1 = Heart1.GetComponent<Renderer>().material;
        MHeart2 = Heart2.GetComponent<Renderer>().material;
        MHeart3 = Heart3.GetComponent<Renderer>().material;

        if(Convert.ToInt32(player.transform.position.y-2.9361f)>ScoreValue){
            ScoreValue = Convert.ToInt32(player.transform.position.y-2.9361f);
            Debug.Log(ScoreValue);
        }

        if(player.transform.position.y<ScoreValue-20)
        {
            
            GameOver();
        }

        if (magic == 2)
        {
            Blast = Blast2;
        }

        if(weapon == 2)
        {
            Slash = Slash2;
        }

        switch (Health)
        {
            case 0:
                MHeart1.color = corVazia;
                MHeart2.color = corVazia;
                MHeart3.color = corVazia;
                GameOver();
                break;
            case 1:
                MHeart1.color = corCheia;
                MHeart2.color = corVazia;
                MHeart3.color = corVazia;
                break;
            case 2:
                MHeart1.color = corCheia;
                MHeart2.color = corCheia;
                MHeart3.color = corVazia;
                break;
            case 3:
                MHeart1.color = corCheia;
                MHeart2.color = corCheia;
                MHeart3.color = corCheia;
                break;
        }

    }

    public void TakeDamage()
    {
        if(Imortal == 0)
        {
            Health = Health - 1;
            Imortal = 1;
            playerMat.color = corDano;
            player.AddForce(new Vector2(0.22f * 20f, 0.22f * 20f), ForceMode2D.Impulse);
        }

        Invoke("frameInv", 2f);
    }

    public void frameInv()
    {
        Imortal = 0;
        playerMat.color = corCheia;

    }

    public void GameOver()
    {
        player.constraints = RigidbodyConstraints2D.FreezeAll;
        Menu.SetActive(true);
    }

    public void jump()
    {
        if (!isJumping&&!cooldownJump)
        {
            player.AddForce(new Vector2(0f, 0.22f * jumpSpeed), ForceMode2D.Impulse);
            isJumping = true;
            cooldownJump = true;
            jumpStop = true;
            Invoke("testjump", 1f);
            Invoke("jumpStopping", 2.5f);
            this.GetComponent<Animator>().runtimeAnimatorController = jumpAnim as RuntimeAnimatorController;
        }


    }

    public void jumpStopping(){
        jumpStop=false;
    }

    public void testjump(){
        cooldownJump = false;
    }

    public void attack()
    {

            Transform shotPoint;
            shotPoint = armaParado;

            if(isAttacking==false){
                isAttacking = true;

                GameObject projectile;


                if (facingRight)
                {
                    projectile = Instantiate(Slash, new Vector2(shotPoint.position.x+0.75f,shotPoint.position.y), new Quaternion(0, 0, 0, 0));
                }
                else
                {
                    projectile = Instantiate(Slash, new Vector2(shotPoint.position.x+0.75f,shotPoint.position.y), new Quaternion(0, 180, 0, 0));
                }
                Invoke("waitAttack", 1f);
            }


    }

    public void Magic()
    {
            Transform shotPoint;
            shotPoint = armaParado;

            GameObject projectile;



            isMagic = true;
            if (facingRight)
            {
                projectile = Instantiate(Blast, shotPoint.position, new Quaternion(0, 0, 0, 0));
            }
            else
            {
                projectile = Instantiate(Blast, shotPoint.position, new Quaternion(0, 180, 0, 0));
            }
            //Atira para a direcao do movimento
            if (facingRight)
            {
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            }
            else
            {
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            }
            Invoke("waitAttack", 1f);

    }



    void waitAttack()
    {
        isAttacking = false;
        isMagic = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Inimigo" || collision.gameObject.tag == "InimigoFlying")
        {
            TakeDamage();
        }

        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }


    }

    void FixedUpdate() {
        Vector2 moveDirection = gameObject.GetComponent<Rigidbody2D>().velocity;
        //Velocidade personagem
        if (horizontalMove > 0.1f || horizontalMove < -0.1f) {
            player.AddForce(new Vector2(horizontalMove * moveSpeed, 0f), ForceMode2D.Impulse);
            //Gira personagem
            if (horizontalMove > 0.1f) {
                this.transform.rotation = new Quaternion(0, 0, 0, 0);
                facingRight = true;
            }
            else {
                if (horizontalMove < -0.1f) {
                    this.transform.rotation = new Quaternion(0, 180, 0, 0);
                    facingRight = false;
                }
            }
            
        }

        if(player.velocity.magnitude > 10)
         {
                player.velocity = Vector2.ClampMagnitude(player.velocity, 10);
         }

         if(horizontalMove<0.1f&&horizontalMove>-0.1f&&jumpStop==false){
                player.velocity = Vector2.ClampMagnitude(player.velocity, 0);
         }

         if(jumpStop==true){

         }


        //Anim
        if (horizontalMove > 0.1f || horizontalMove < -0.1f)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = runAnim as RuntimeAnimatorController;
        }
        else
        {
            if (isJumping)
            {
                if (!isAttacking)
                {
                    this.GetComponent<Animator>().runtimeAnimatorController = jumpAnim as RuntimeAnimatorController;
                }
                else
                {
                    this.GetComponent<Animator>().runtimeAnimatorController = attackAnim2 as RuntimeAnimatorController;

                }
            }
            else
            {
                if (isAttacking)
                {
                    this.GetComponent<Animator>().runtimeAnimatorController = attackAnim as RuntimeAnimatorController;
                }
                else
                {
                    if (isMagic)
                    {
                        this.GetComponent<Animator>().runtimeAnimatorController = castAnim as RuntimeAnimatorController;
                    }
                    else
                    {
                        this.GetComponent<Animator>().runtimeAnimatorController = idleAnim as RuntimeAnimatorController;
                    }
                }
            }
        }

    }


    //PowerUps
    public void IncreaseDmg()
    {
        if (weapon == 1)
        {
            weapon = 2;
        }
        else
        {
            IncreaseLife();
        }

    }

    public void IncreaseLife()
    {
        if (Health < 3)
        {
            Health = Health + 1;
        }

    }

    public void IncreaseMagic()
    {
        if (magic == 1)
        {
            magic = 2;
        }
        else
        {
            IncreaseLife();
        }
    }


}
