using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{

    public float speed;
    private bool movingRight = true;
    public Transform groundDetection;
    public int energy;
    public RuntimeAnimatorController runAnim;
    public RuntimeAnimatorController damAnim;
    public RuntimeAnimatorController deathAnim;
    public Rigidbody2D enemy;
    bool Inv;

    // Start is called before the first frame update
    void Start()
    {
        Inv = false;
    }

    public void TakeDamage(int dano){
        energy = energy-dano;
        if(Inv == false){
            enemy.AddForce(new Vector2(0.22f * 20f, -0.22f * 20f), ForceMode2D.Impulse);
            Inv = true;
            Invoke("frameInv", 0.5f);
        }
    }

    void frameInv(){
        Inv = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);

        if(energy<=0){
            GetComponent<Animator>().runtimeAnimatorController = deathAnim as RuntimeAnimatorController;
            Invoke("GameOver",0.3f);
        }else{
            if(Inv==true){
                GetComponent<Animator>().runtimeAnimatorController = damAnim as RuntimeAnimatorController;
            }else{
                GetComponent<Animator>().runtimeAnimatorController = runAnim as RuntimeAnimatorController;
            }
        }


        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.2f);
        if(groundInfo.collider == false){
            if(movingRight==true){
                transform.eulerAngles = new Vector3(0,-180,0);
                movingRight=false;
            }else{
                transform.eulerAngles = new Vector3(0,0,0);
                movingRight=true;
            }
        }
    }

    void GameOver(){
        Destroy(gameObject);
    }
}
