using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutLineUR : MonoBehaviour
{
    public List<GameObject> LeftUR = new List<GameObject>();
    public List<GameObject> RightUR = new List<GameObject>();
    public List<GameObject> quan = new List<GameObject>();
    public List<Button> Leftbuttons = new List<Button>();
    public List<Button> Rightbuttons = new List<Button>();
    int index_left, index_right;
    bool flag_left, flag_right;
    float speed_left, speed_right;
    // Start is called before the first frame update
    void Start()
    {
        index_left = 0;
        index_right = 0;
        flag_left = false;
        flag_right = false;
        speed_left = 15.0f;
        speed_right = 15.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(flag_left)
        {
            if(index_left==0|| index_left==3|| index_left==4)
                LeftUR[index_left].transform.Rotate(new Vector3(0, 0, 1), speed_left * Time.deltaTime);
            else
                LeftUR[index_left].transform.Rotate(new Vector3(0, 1, 0), -speed_left * Time.deltaTime);
        }
        if(flag_right)
        {
            if (index_right == 0|| index_right == 3|| index_right == 4)
                RightUR[index_right].transform.Rotate(new Vector3(0, 0, 1), speed_right * Time.deltaTime);
            else
                RightUR[index_right].transform.Rotate(new Vector3(0, 1, 0), speed_right * Time.deltaTime);
        }
        for(int i=0;i<6;i++)
        {
            Leftbuttons[i].GetComponent<Image>().color = Color.white;
            Rightbuttons[i].GetComponent<Image>().color = Color.white;
        }
        Leftbuttons[index_left].GetComponent<Image>().color = Color.red;
        Rightbuttons[index_right].GetComponent<Image>().color = Color.red;
    }
    public void LeftCtrl(bool dir)
    {
        quan[0].SetActive(true);
        flag_left = true;
        if(dir)
        {
            speed_left = 20f;
        }
        else
        {
            speed_left = -20f;
        }

    }
    public void RightCtrl(bool dir)
    {
        quan[1].SetActive(true);
        flag_right = true;
        if (dir)
        {
            speed_right = 20f;
        }
        else
        {
            speed_right = -20f;
        }
    }
    public void LeftHalt()
    {
        flag_left = false;
        quan[0].SetActive(false);
    }
    public void RightHalt()
    {
        flag_right = false;
        quan[1].SetActive(false);
    }
    public void BtnLeftIndex(int val)
    {
        index_left = val;
    }
    public void BtnRightIndex(int val)
    {
        index_right = val;
    }
}
