using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeUI : MonoBehaviour
{
    Button btnMode;
    GameObject imgChosen;
    public ModeUI(Button btn, GameObject img)
    {
        btnMode = btn;
        imgChosen = img;
    }
    public void BeChosen(bool flag)
    {
        imgChosen.SetActive(flag);
    }
    public void BtnBeChosen(bool flag)
    {
        btnMode.interactable = flag;
    }
}
