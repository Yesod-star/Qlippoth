using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyGraphics : MonoBehaviour
{
    public AIPath aiPath;
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

    
    public void GameOver(){
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if(enemy.transform.position.x>30){
            enemy.constraints = RigidbodyConstraints2D.FreezeAll;
        }else{
            enemy.constraints = RigidbodyConstraints2D.None;
        }
        if(aiPath.desiredVelocity.x >= 0.01f){
            transform.localScale = new Vector3(-1f,1f,1f);
        }else if(aiPath.desiredVelocity.x<=-0.01f){
            transform.localScale = new Vector3(1f,1f,1f);
        }

        if(energy<=0){
            GetComponent<Animator>().runtimeAnimatorController = deathAnim as RuntimeAnimatorController;
            Invoke("GameOver",.3f);
        }else{
            if(Inv==true){
                GetComponent<Animator>().runtimeAnimatorController = damAnim as RuntimeAnimatorController;
            }else{
                GetComponent<Animator>().runtimeAnimatorController = runAnim as RuntimeAnimatorController;
            }
        }
    }
}
