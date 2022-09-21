using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class changeScore : MonoBehaviour
{

    [SerializeField]
    private TMP_Text _title;

    public GameObject player;
    public Movement playerMovement;
    int score;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Convert.ToInt32(player.transform.position.y-2.9361f)>score){
            score = Convert.ToInt32(player.transform.position.y-2.9361f);
        }
        if(playerMovement.ScoreValue>score){
            score = playerMovement.ScoreValue;
        }

        _title.text = "Score: "+score;
    }
}
