using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class XMLRead
{
    private string m_file_path = "E:\\UR10\\Assets\\XMLS\\Missions";
    private string m_XML_name;
    private string m_XML_path;
    private XmlDocument m_xmldoc;
    public XMLRead()
    {
        m_XML_name = "Init.XML";
        m_XML_path = CreatePath();
        m_xmldoc = new XmlDocument();
    }
    public XMLRead(string xml_name)
    {
        m_XML_name = xml_name;
        m_XML_path = CreatePath();
        m_xmldoc = new XmlDocument();
    }
    string CreatePath()
    {
        string path= m_file_path+"\\"+ m_XML_name;
        return path;
    }
    public bool Read()
    {
        try
        {
            m_xmldoc.Load(m_XML_path);
            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }
    public XmlDocument XMLdoc
    {
        get
        {
            return m_xmldoc;
        }
    }
}
