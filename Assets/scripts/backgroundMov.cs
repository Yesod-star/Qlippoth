using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMov : MonoBehaviour {
    public float FollowSpeed = 2f;
    public Transform target;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //Segue target
        Vector3 newPos = new Vector3(target.position.x, target.position.y, 0);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        float y = transform.eulerAngles.y;
        float x = transform.position.z;
    }
}
