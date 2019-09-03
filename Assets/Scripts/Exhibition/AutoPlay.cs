using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlay : MonoBehaviour
{
    public Animation ani;
    public GameObject btn;
    public float spd = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ani.isPlaying)
            btn.GetComponent<Button>().interactable = false;
        else
            btn.GetComponent<Button>().interactable = true;
    }
    public void btnAutoPlay()
    {
        ani["Exhibition_Xianjia"].speed = spd;
        ani.Play("Exhibition_Xianjia");
    }
}
