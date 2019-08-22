using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IOManager
{
    private int m_IO_num;
    private Toggle[] m_toggles;
    private float m_on_time;
    private float m_sleep_time;
    private Toggle m_chosen_toggle;
    private bool m_can_chose;
    private int m_chosen_index;
    public IOManager(int num)
    {
        m_IO_num = num;
        m_toggles = new Toggle[m_IO_num];
        m_on_time = 0;
        m_sleep_time = 3.0f;
        m_can_chose = true;
    }
    public Toggle[] ToggleSet
    {
        set
        {
            m_toggles = value;
        }
    }
    public float sleepTimeSet
    {
        set
        {
            m_sleep_time = value;
        }
    }
    public void Init()
    {
        foreach (Toggle toggle in m_toggles)
        {
            //toggle.isOn = false;
        }
    }
    public bool Monitor()
    {
        for(int i=0;i<m_IO_num;i++)
        {
            if (m_can_chose)
            {
                if (m_toggles[i].isOn)
                {
                    m_chosen_toggle = m_toggles[i];
                    m_chosen_index = i;
                    m_on_time = Time.time;
                    m_can_chose = false;
                }
            }
            else
            {
                m_toggles[i].interactable = false;
            }
        }
        if(m_on_time+m_sleep_time<Time.time)
        {
            if (m_chosen_toggle != null)
            {
                m_chosen_toggle.isOn = false;
                m_can_chose = true;
                for(int i=0;i<m_IO_num;i++)
                {
                    m_toggles[i].interactable = true;
                }
            }
            return false;
        }
        return true;
    }
    public int chosenIndex
    {
        get
        {
            return m_chosen_index;
        }
    }
}
