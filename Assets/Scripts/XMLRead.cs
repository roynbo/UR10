using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class XMLRead
{
    private string m_file_path = "K:\\高压带电\\UR10\\Assets\\XMLS";
    private string m_XML_name;
    private string m_XML_path;
    private XmlDocument xmldoc;
    public XMLRead()
    {
        m_XML_name = "Init.XML";
        m_XML_path = CreatePath();
        xmldoc = new XmlDocument();
    }
    public XMLRead(string xml_name)
    {
        m_XML_name = xml_name;
        m_XML_path = CreatePath();
        xmldoc = new XmlDocument();
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
            xmldoc.Load(m_XML_path);
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
            return xmldoc;
        }
    }
}
