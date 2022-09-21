using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Attack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool pointerDown;
    private float pointerDownTimer;


    public float requiredHoldTime;

    public UnityEvent onLongClick;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData data)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        Reset();
    }


    // Update is called once per frame
    void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer +=  Time.deltaTime;
            if (pointerDownTimer > requiredHoldTime  )
            {
                onLongClick.Invoke();
                Reset();
            }
        }
        
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
    }

}
