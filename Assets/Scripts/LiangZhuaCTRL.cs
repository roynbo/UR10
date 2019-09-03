using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiangZhuaCTRL : MonoBehaviour
{
    [SerializeField] bool openFlag = false;
    Animation anim;
    bool playOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (openFlag)
        {
            if (!playOnce)
            {
                anim.Play("zhuaOpen");
                playOnce = true;
            }
        }
        else
        {
            if (playOnce)
            {
                anim.Play("zhuaClose");
                playOnce = false;
            }
        }
    }
}
