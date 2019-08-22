using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisManager
{
    private Text m_axis_Position;
    public AxisManager(GameObject gameObject)
    {
        m_axis_Position = gameObject.transform.Find("Pos").gameObject.GetComponent<Text>();
    }
    public string PosSet
    {
        set
        {
            m_axis_Position.text = value;
        }
    }
    
}
