using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using URControl;
using URDate;
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
}
