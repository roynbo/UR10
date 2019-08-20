using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class URInit : MonoBehaviour
{
    private XMLRead positionRead;
    private XmlNode root;
    private XmlElement left_UR_position_ele;
    private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = this.transform;
        positionRead = new XMLRead("URPositions.xml");
        if(positionRead.Read())
        {
            root = positionRead.XMLdoc.SelectSingleNode("Positions");
            left_UR_position_ele = (XmlElement)root.SelectSingleNode("Left_UR_Position");
            PositionSet();
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
    void PositionSet()
    {
        int i = 0;
        float[] transList = new float[6];
        XmlElement position_ele = (XmlElement)left_UR_position_ele.SelectSingleNode("Position");
        foreach(XmlNode xmlNode in position_ele.ChildNodes)
        {     
            transList[i++] = (float)Convert.ToDouble(xmlNode.InnerText);
        }
        XmlElement rotation_ele = (XmlElement)left_UR_position_ele.SelectSingleNode("Rotation");
        foreach (XmlNode xmlNode in rotation_ele.ChildNodes)
        {
            transList[i++] = (float)Convert.ToDouble(xmlNode.InnerText);
        }
        trans.position = new Vector3(transList[0], transList[1], transList[2]);
        trans.eulerAngles = new Vector3(transList[3], transList[4], transList[5]);
    }
}
