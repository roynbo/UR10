using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShanShuo : MonoBehaviour
{
    public CanvasGroup cg;
    public float speed=0.05f;
    private bool flag = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            cg.alpha -= speed * Time.deltaTime;
            if (cg.alpha < 0.001)
                flag = false;
        }
        else
        {
            cg.alpha+= speed * Time.deltaTime;
            if (1-cg.alpha < 0.001)
                flag = true;
        }
    }
}
