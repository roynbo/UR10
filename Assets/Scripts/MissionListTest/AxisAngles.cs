using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class AxisAngles
{
    private double[] m_Angles = new double[6];
    int m_axis_num = 6;
    public AxisAngles(XmlElement xmlElement)
    {
        int i = 0;
        foreach(XmlElement ele in xmlElement)
        {
            m_Angles[i++] =Convert.ToDouble(ele.InnerText);
        }
    }
    public double[] Angles
    {
        get
        {
            return m_Angles;
        }
    }
}
