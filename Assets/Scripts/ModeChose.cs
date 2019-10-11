using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChose : MonoBehaviour
{
    Button[] buttons = new Button[3];
    GameObject[] imgs = new GameObject[3];
    ModeUI[] modes = new ModeUI[3];
    bool OnceFlag = false;
    Animation animations;
    // Start is called before the first frame update
    void Start()
    {
        buttons[0] = this.transform.Find("按钮：离线任务").GetComponent<Button>();
        buttons[1] = this.transform.Find("按钮：末端移动").GetComponent<Button>();
        buttons[2] = this.transform.Find("按钮：单轴点动").GetComponent<Button>();
        animations = this.GetComponent<Animation>();
        for (int i=0;i<3;i++)
        {
            imgs[i] = this.transform.Find("Image" + i.ToString()).gameObject;
        }
        for(int i=0;i<3;i++)
        {
            modes[i] = new ModeUI(buttons[i], imgs[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ModeIn(int index)
    {
        if (!OnceFlag)
        {
            for (int i = 0; i < 3; i++)
            {
                modes[i].BeChosen(false);
                modes[i].BtnBeChosen(false);
            }
            modes[index].BeChosen(true);
            modes[index].BtnBeChosen(true);
            OnceFlag = true;
            animations["ModeShow" + index.ToString()].time = 0;
            animations["ModeShow" + index.ToString()].speed = 1;
            animations.Play("ModeShow" + index.ToString());

        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                modes[i].BeChosen(false);
                modes[i].BtnBeChosen(true);
            }
            OnceFlag = false;
            animations["ModeShow" + index.ToString()].time = animations["ModeShow" + index.ToString()].clip.length;
            animations["ModeShow" + index.ToString()].speed = -1;
            animations.Play("ModeShow" + index.ToString());
        }
    }
}
