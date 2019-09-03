using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Exhibition : MonoBehaviour
{
    XMLRead xmlRead;
    [SerializeField] Transform[] Axises = new Transform[6];
    [SerializeField] GameObject jiatou;
    [SerializeField] GameObject jiatouInJiaZi;
    [SerializeField] GameObject jiatouOnXian;
    [SerializeField] Animation anim;
    float[] axisAngles = new float[6];
    int missionIndex = -1;
    List<Mission> mission_List = new List<Mission>();
    // Start is called before the first frame update
    void Start()
    {
        xmlRead = new XMLRead("Missions.xml");
        print(xmlRead.CreatePath());
        if (xmlRead.Read())
        {
            print("文件读取正确");
            foreach (XmlElement xmlEle in xmlRead.XMLdoc.SelectSingleNode("Missions").ChildNodes)
            {
                Mission mission = new Mission(xmlEle);
                mission_List.Add(mission);
            }
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
    public void btnNextMission()
    {
        missionIndex++;
        if(missionIndex>=mission_List.Count)
        {
            missionIndex = -1;
            Axises[0].localEulerAngles = new Vector3(Axises[0].localEulerAngles.x, -(0 + 90), Axises[0].localEulerAngles.z);
            Axises[1].localEulerAngles = new Vector3(Axises[1].localEulerAngles.x, -(-90 + 90), Axises[1].localEulerAngles.z);
            Axises[2].localEulerAngles = new Vector3(Axises[2].localEulerAngles.x, -(0), Axises[2].localEulerAngles.z);
            Axises[3].localEulerAngles = new Vector3(Axises[3].localEulerAngles.x, Axises[3].localEulerAngles.z, -(-90 - 90));
            Axises[4].localEulerAngles = new Vector3(Axises[4].localEulerAngles.x, -(0 + 180), Axises[4].localEulerAngles.z);
            Axises[5].localEulerAngles = new Vector3(Axises[5].localEulerAngles.x, -(0 + 90), Axises[5].localEulerAngles.z);
            
        }
        if (missionIndex >= 0 && missionIndex < mission_List.Count)
        {
            if (missionIndex == 0)
            {
                jiatouInJiaZi.SetActive(true);
                jiatouOnXian.SetActive(false);
            }
            for (int i = 0; i < 6; i++)
            {
                axisAngles[i] = (float)mission_List[missionIndex].Angles[i];
            }
            Axises[0].localEulerAngles = new Vector3(Axises[0].localEulerAngles.x, -(axisAngles[0]+90),Axises[0].localEulerAngles.z );
            Axises[1].localEulerAngles = new Vector3(Axises[1].localEulerAngles.x, -(axisAngles[1] + 90), Axises[1].localEulerAngles.z);
            Axises[2].localEulerAngles = new Vector3(Axises[2].localEulerAngles.x, -(axisAngles[2]), Axises[2].localEulerAngles.z);
            Axises[3].localEulerAngles = new Vector3(Axises[3].localEulerAngles.x, Axises[3].localEulerAngles.z, -(axisAngles[3] - 90));
            Axises[4].localEulerAngles = new Vector3(Axises[4].localEulerAngles.x, -(axisAngles[4] + 180), Axises[4].localEulerAngles.z);
            Axises[5].localEulerAngles = new Vector3(Axises[5].localEulerAngles.x, -(axisAngles[5]+90), Axises[5].localEulerAngles.z);
            print((axisAngles[0] + 90));
            if(mission_List[missionIndex].IOindex!=-1)
            {
                if(mission_List[missionIndex].IOindex==0)
                {
                    anim.Play("zhuaClose");
                    jiatou.SetActive(true);
                    jiatouInJiaZi.SetActive(false);
                }
                else if(mission_List[missionIndex].IOindex==1)
                {
                    anim.Play("zhuaOpen");
                    jiatou.SetActive(false);
                    jiatouOnXian.SetActive(true);
                }
            }
        }
    }
}
