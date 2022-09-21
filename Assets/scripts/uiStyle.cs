using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiStyle : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    public void SetMaxHealth(float health) {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health) {
        slider.value = health;
    }

    public float GetHealth() { 
        return slider.value;
    }

    public void LoadScene(string NewScene)
    {
        SceneManager.LoadScene(NewScene);

    }

    public void exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
