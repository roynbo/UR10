using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Mission
{
    private AxisAngles m_axisAngles;
    private IOMission m_IOMission;
    private string m_log;
    private double[] m_angles;
    public Mission(XmlElement xmlElement)
    {
        XmlElement AxisAngles_ele = (XmlElement)xmlElement.SelectSingleNode("AxisAngles");
        m_axisAngles = new AxisAngles(AxisAngles_ele);
        m_angles = m_axisAngles.Angles;
        XmlElement IO_ele = (XmlElement)xmlElement.SelectSingleNode("IO");
        m_IOMission = new IOMission(IO_ele);
        XmlElement Log_ele = (XmlElement)xmlElement.SelectSingleNode("Log");
        m_log = Log_ele.InnerText;
    }
    public string Log
    {
        get
        {
            return m_log;
        }
    }

    public double[] Angles
    {
        get
        {
            return m_angles;
        }
    }

}
