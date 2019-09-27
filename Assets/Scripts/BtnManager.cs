using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using URControl;
using URDate;
using System.Xml;

public class BtnManager : MonoBehaviour
{
    IOManager leftIO, rightIO;
    [SerializeField] Toggle[] Toggles = new Toggle[2];
    [SerializeField] float sleepTime = 3.0f;
    [SerializeField] string Target_IP = "192.168.1.3";
    [SerializeField] GameObject URStore;
    private int Control_Port = 30003;
    private double SpeedRate;
    private double AccelerationRate;
    float leftOnTime, rightOnTime;
    float waitTime = 3.0f;
    URDateHandle URDateCollector = new URDateHandle();
    URControlHandle URController = new URControlHandle();
    public int DataRefreshRate = 50;
    public double BasicSpeed = 0.15;
    public double BasicAcceleration = 0.15;
    AxisManager[] axises = new AxisManager[6];
    string[] temp_Pos = new string[12];
    double[] current_Pos = new double[12];
    bool flag_IOClose = false;

    //任务队列测试
    [SerializeField] bool inTest = false;//左臂参加测试
    XMLRead xmlRead;
    List<Mission>mission_List=new List<Mission>();
    int index=-1;
    [SerializeField] GameObject URMissionList;
    Text txIndex;
    Text txLog;

    //剥线测试
    int bxIndex = 500;
    // Start is called before the first frame update
    void Start()
    {
        //Do_Initilize();
        leftIO = new IOManager(Toggles.Length);
        leftIO.ToggleSet = Toggles;
        leftIO.Init();
        leftIO.sleepTimeSet = sleepTime;
        for(int i=0;i<6;i++)
            axises[i] = new AxisManager(URStore.transform.Find("Axis"+i.ToString()).gameObject);
        URDateCollector.OnGetPositionSuccess += new URDateHandle.GetPositionSuccess(UpdatePositionsValue);
        URDateCollector.OnGetAngleSuccess += new URDateHandle.GetAngleSuccess(UpdateAnglesValue);
        for(int i=0;i<6;i++)
        {
            temp_Pos[i] = "0.0";
        }

        //任务队列测试
        if (inTest)
        {
            txIndex = URMissionList.transform.Find("Texts").transform.Find("Index").gameObject.GetComponent<Text>();
            txLog= URMissionList.transform.Find("Texts").transform.Find("Log").gameObject.GetComponent<Text>();
            xmlRead = new XMLRead("Boxian.xml");
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
    }

    // Update is called once per frame
    void Update()
    {
        if(!leftIO.Monitor())
        {
            if (!flag_IOClose)
            {
                //print(CommandScripts.IO(leftIO.chosenIndex,false));
                URController.Send_command(CommandScripts.IO(leftIO.chosenIndex, false));
                flag_IOClose = true;
             }
        }
        else
        {
            flag_IOClose = false;
        }
        //rightIO.Monitor();
        for(int i=0;i<6;i++)
        {
            axises[i].PosSet = temp_Pos[i];
        }
        Button btnConnect = GameObject.Find("btnConnect").GetComponent<Button>();
        if(URController.isConnect)
            btnConnect.interactable = false;
    }

    public void btnWrite()
    {
        XMLRead bxXMLRead = new XMLRead("Boxian.xml");
        bxXMLRead.Read();
        XmlDocument doc = bxXMLRead.XMLdoc;
        XmlNode root = doc.SelectSingleNode("Missions");
        XmlElement Mission = doc.CreateElement("Mission_"+ bxIndex.ToString());
        XmlElement Log = doc.CreateElement("Log");
        Log.InnerText = "第" + (bxIndex+1).ToString() + "步";
        XmlElement AxisAngles = doc.CreateElement("AxisAngles");
        for(int i=0;i<6;i++)
        {
            XmlElement Axisangle = doc.CreateElement("Axis_" + i.ToString() + "_angle");
            Axisangle.InnerText = temp_Pos[i].ToString();
            AxisAngles.AppendChild(Axisangle);
        }
        XmlElement IO = doc.CreateElement("IO");
        IO.InnerText = "-1";
        Mission.AppendChild(Log);
        Mission.AppendChild(AxisAngles);
        Mission.AppendChild(IO);
        root.AppendChild(Mission);
        doc.Save(bxXMLRead.path);
        bxIndex++;

    }
    #region UR通讯
    public void Do_Initilize()
    {
        URDateHandle.ScanRate = DataRefreshRate;
        URDateCollector.InitialMoniter(Target_IP);
        //初始化URControlHandle，生成一个clientSocket，方便从30001-30003端口直接发送指令
        URController.Creat_client(Target_IP, Control_Port);

        //初始化速度和加速度(基准速度0.15 最高变成2倍即0.2，最低变成0.1倍即0.01)
        SpeedRate = BasicSpeed;
        AccelerationRate = BasicAcceleration;
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
    public void btnAxisMove(int index)
    {
        if(index%10==0)
            URController.Send_command(CommandScripts.MoveAxis(index/10-1, -1,AccelerationRate,SpeedRate, current_Pos));
        else
            URController.Send_command(CommandScripts.MoveAxis(index/10-1, 1, AccelerationRate, SpeedRate, current_Pos));
    }
    public void btnAxisStop()
    {
        URController.Send_command(CommandScripts.MoveStop()); 
    }
    public void toggleIO(int index)
    {
        //print(CommandScripts.IO(index,true));
        URController.Send_command(CommandScripts.IO(index,true));
    }
    public void btnHome()
    {
        //print(CommandScripts.Home(AccelerationRate, SpeedRate));
        URController.Send_command(CommandScripts.Home(AccelerationRate, SpeedRate));
    }
    #endregion

    #region 任务队列测试
    public void btnNextMission()
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
                toggleIO(mission_List[index].IOindex);
            }
                
        }
        else
        {
            txLog.text = "任务执行完毕";
            Button btnNext = URMissionList.transform.Find("btnNextMission").GetComponent<Button>();
            btnNext.interactable = false;
        }
    }
    #endregion
}
