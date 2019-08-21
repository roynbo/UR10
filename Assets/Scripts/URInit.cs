using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class URInit : MonoBehaviour
{
    private XMLRead positionRead;
    private XmlNode root;
    private XmlElement left_UR_position_ele, right_UR_position_ele;
    private Transform left_UR_trans,right_UR_trans;
    enum ItemName
    {
        LeftUR,
        RightUR,
        Zhijia,
        Shouzhua,
        Boxianqi
    };
    // Start is called before the first frame update
    void Start()
    {
        positionRead = new XMLRead("URPositions.xml");
        if(positionRead.Read())
        {
            root = positionRead.XMLdoc.SelectSingleNode("Positions");
            PositionSet(ItemName.LeftUR);
            PositionSet(ItemName.RightUR);
            PositionSet(ItemName.Zhijia);
            print("文件读取正确");
        }
        else
        {
            print("文件路径不对");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PositionSet(ItemName itemName)
    {
        Transform trans = null;
        XmlElement UR_position_ele = null;
        if (itemName == ItemName.LeftUR)
        {
            trans = GameObject.Find("Left_UR10").GetComponent<Transform>();
            UR_position_ele = (XmlElement)root.SelectSingleNode("Left_UR_Position");
        }
        else if(itemName == ItemName.RightUR)
        {
            trans = GameObject.Find("Right_UR10").GetComponent<Transform>();
            UR_position_ele = (XmlElement)root.SelectSingleNode("Right_UR_Position");
        }
        else if (itemName == ItemName.Zhijia)
        {
            trans = GameObject.Find("Zhijia").GetComponent<Transform>();
            UR_position_ele = (XmlElement)root.SelectSingleNode("Zhijia_Position");
        }
        int i = 0;
        float[] transList = new float[6];
        XmlElement position_ele = (XmlElement)UR_position_ele.SelectSingleNode("Position");
        foreach(XmlNode xmlNode in position_ele.ChildNodes)
        {
            transList[i++] = (float)Convert.ToDouble(xmlNode.InnerText);
        }
        XmlElement rotation_ele = (XmlElement)UR_position_ele.SelectSingleNode("Rotation");
        foreach (XmlNode xmlNode in rotation_ele.ChildNodes)
        {
            transList[i++] = (float)Convert.ToDouble(xmlNode.InnerText);
        }
        trans.position = new Vector3(transList[0], transList[1], transList[2]);
        trans.eulerAngles = new Vector3(transList[3], transList[4], transList[5]);
    }
}
