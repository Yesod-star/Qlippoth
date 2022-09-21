using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMov : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Segue movimento target
        Vector3 newPos = new Vector3(0, target.position.y + 2.25f, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        float y = transform.eulerAngles.y;
        transform.Rotate(0, -y, 0);
    }
}
