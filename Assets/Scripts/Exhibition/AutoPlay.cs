using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlay : MonoBehaviour
{
    public Animation ani;
    public GameObject btn;
    public float spd = 1.0f;
    public string aniName;
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
        ani[aniName].speed = spd;
        ani.Play(aniName);
    }
}
