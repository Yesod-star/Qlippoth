using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infinityLevel : MonoBehaviour
{
    public GameObject phase1;
    public GameObject phase2;
    public GameObject phase3;
    private int level;
    private float height;
    private float heightPhase;
    public Rigidbody2D player;


    // Start is called before the first frame update
    void Start()
    {
        level = 2;
        height = phase1.GetComponent<SpriteRenderer>().bounds.size.y;
        heightPhase = height;
    }

    // Update is called once per frame
    void Update()
    {
        if(level==4){
            level = 1;
        }
        if(player.transform.position.y>=height-5f){
            newPart();
            height = height+heightPhase+2f;
        }
    }

    void newPart(){
        switch(level){
            case 1:
                Instantiate(phase1,new Vector3(0.0704f,player.position.y+player.GetComponent<SpriteRenderer>().bounds.size.y+9f), new Quaternion(0, 0, 0, 0));
                level++;
                break;
            case 2:
                Instantiate(phase2,new Vector3(0.0704f,player.position.y+player.GetComponent<SpriteRenderer>().bounds.size.y+9f), new Quaternion(0, 0, 0, 0));
                level++;
                break;
            case 3:
                Instantiate(phase3,new Vector3(0.0704f,player.position.y+player.GetComponent<SpriteRenderer>().bounds.size.y+9f), new Quaternion(0, 0, 0, 0));
                level++;
                break;
        }
    }
}
