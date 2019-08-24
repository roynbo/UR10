using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
public class IOMission
{
    private int m_index;
    public IOMission(XmlElement xmlElement)
    {
        m_index = Convert.ToInt32(xmlElement.InnerText);
    }
    public int index
    {
        get
        {
            return m_index;
        }
    }
}
