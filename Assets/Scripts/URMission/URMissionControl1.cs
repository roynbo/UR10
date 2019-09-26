using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using URControl;
using URDate;
using System.Xml;

public class URMissionControl1 : MonoBehaviour
{
    public string IP;
    public Image status;
    URDateHandle URDateCollector = new URDateHandle();
    URControlHandle URController = new URControlHandle();
    int DataRefreshRate = 50;
    double BasicSpeed = 0.15;
    double BasicAcceleration = 0.15;
    private int Control_Port = 30003;
    private double SpeedRate;
    private double AccelerationRate;
    string[] temp_Pos = new string[12];
    double[] current_Pos = new double[12];

    XMLRead xmlRead;
    List<Mission> mission_List = new List<Mission>();
    int index = -1;
    public Text txLog;
    public Text txIndex;
    public Button btnNext;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (URController.isConnect)
        {
            status.color = Color.green;
        }
        else
            status.color = Color.red;
    }
    public void Connect()
    {
        URDateHandle.ScanRate = DataRefreshRate;
        URDateCollector.InitialMoniter(IP);
        //初始化URControlHandle，生成一个clientSocket，方便从30001-30003端口直接发送指令
        URController.Creat_client(IP, Control_Port);

        //初始化速度和加速度(基准速度0.15 最高变成2倍即0.2，最低变成0.1倍即0.01)
        SpeedRate = BasicSpeed;
        AccelerationRate = BasicAcceleration;
        URDateCollector.OnGetPositionSuccess += new URDateHandle.GetPositionSuccess(UpdatePositionsValue);
        URDateCollector.OnGetAngleSuccess += new URDateHandle.GetAngleSuccess(UpdateAnglesValue);
    }
    void UpdatePositionsValue(float[] Positions)
    {
        for (int i = 6; i < 12; i++)
        {
            current_Pos[i] = Positions[i - 6];
            temp_Pos[i] = current_Pos[i].ToString("0.0");
        }
    }
    void UpdateAnglesValue(double[] Angles)
    {
        for (int i = 0; i < Angles.Length; i++)
        {
            if (Angles[i] > 180)
                Angles[i] -= 360;
        }
        for (int i = 0; i < 6; i++)
        {
            current_Pos[i] = Angles[i];
            temp_Pos[i] = current_Pos[i].ToString("0.00");
        }
    }
    public void MissionChoose(int xml_index)
    {
        string fileName;
        switch (xml_index)
        {
            case 0:
                fileName = "ningluoshuan.xml";
                break;
            case 1:
                fileName = "boxian.xml";
                break;
            case 2:
                fileName = "fangxianjia.xml";
                break;
            case 3:
                fileName = "jiaxian.xml";
                break;
            default:
                fileName = "init.xml";
                break;
        }
        mission_List.Clear();
        xmlRead = new XMLRead(fileName);
        xmlRead.Read();
        foreach (XmlElement xmlEle in xmlRead.XMLdoc.SelectSingleNode("Missions").ChildNodes)
        {
            Mission mission = new Mission(xmlEle);
            mission_List.Add(mission);
        }
        index = -1;
        btnNext.interactable = true;
    }
    public void NextMission()
    {
        index++;
        if (index < mission_List.Count)
        {
            txIndex.text = (index + 1).ToString();
            txLog.text = mission_List[index].Log;
            //print(CommandScripts.MissionDo(mission_List[index], AccelerationRate, SpeedRate));
            if (mission_List[index].IOindex == -1)
                URController.Send_command(CommandScripts.MissionDo(mission_List[index], AccelerationRate, SpeedRate));
            else
            {
                StartCoroutine(IOProcess(mission_List[index].IOindex));
            }
        }
        else
        {
            txLog.text = "任务执行完毕";
            btnNext.interactable = false;
        }
    }
    public void JinTing()
    {
        URController.Send_command(CommandScripts.MoveStop());
    }
    IEnumerator IOProcess(int IO_index)
    {
        URController.Send_command(CommandScripts.IO(IO_index, true));
        yield return new WaitForSeconds(2);
        URController.Send_command(CommandScripts.IO(IO_index, false));
    }
}
