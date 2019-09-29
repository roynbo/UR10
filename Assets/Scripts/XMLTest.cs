using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XMLTest : MonoBehaviour
{
    public Text guitext;
    public Text platform;
    string allscores = "";

    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            platform.text = "Android";
        }
        else
        {
            platform.text = "PC";
        }
    }

    // Use this for initialization
    void Start()
    {

        XMLLoad xmlLoad = new XMLLoad();
        StartCoroutine(xmlLoad.GetXML("score.xml"));
        //AddressData1.insertNode(22);

    }


    // Update is called once per frame
    void Update()
    {

    }
}
