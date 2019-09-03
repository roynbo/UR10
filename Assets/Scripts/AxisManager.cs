using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisManager
{
    private Text m_axis_localPosition;
    public AxisManager(GameObject gameObject)
    {
        m_axis_localPosition = gameObject.transform.Find("Pos").gameObject.GetComponent<Text>();
    }
    public string PosSet
    {
        set
        {
            m_axis_localPosition.text = value;
        }
    }
    
}
