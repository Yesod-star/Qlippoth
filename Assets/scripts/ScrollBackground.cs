using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{

    public float vel_horiz = 0.2f;
    public float vel_vert = 0.2f;

    private Renderer re;

    // Start is called before the first frame update
    void Start()
    {
        re = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * vel_horiz, Time.time * vel_vert);
        re.material.mainTextureOffset = offset;
    }
}
