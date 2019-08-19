using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using URSDK.RobController.Communication;
using URSDK.RobController.Datatype;

public class URSocket1 : MonoBehaviour
{
    public List<Text> Pos = new List<Text>();
    List<double> pd = new List<double>();
    public List<Transform> TPos = new List<Transform>();
    public static double PI = 3.1415927;
    public string AdrIP = "192.168.1.4";
    public int AdrPort = 30001;
    RTClient aRTClient = null;
    DashBoard aDashBoard = null;
    PrimaryInterface primaryInt = null;
    PrimaryInterface demoInstance = null;
    Vector6<double> CurTCPPos = null;
    Vector6<double> TarTCPPos = null;
    Vector6<double> CurJointPos = null;
    Vector6<double> TarJointPos = null;
    Vector6<double> j = null;
    RTDE aRTDE = null;
    void URConnect()
    {
        aRTClient.startRTClient(); //启动RTClient server
        demoInstance.startPrimary();//启动同步
        //timer1.Start();
        //timer2.Start();
        //StartCoroutine(timer1_Tick());
        StartCoroutine(timer2_Tick());
    }

    void URDisConnect()
    {
        aRTClient.stopRTClient(); //停止RTClient server释放资源.
        StopCoroutine(timer2_Tick());
    }
    /// <summary>
    /// 计时器触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private IEnumerator timer1_Tick()
    {
        while (true)
        {
            #region 读取机器人状态
            Vector6<double> l4 = null;
            //Vector6<double> j = null;
            if (aRTClient.Status == RTClientStatus.Syncing)
            {
                RTClientObj aRTClientObj = aRTClient.getRTClientObj(); //读取aRTClientObj
                double currenttime = aRTClientObj.timestamp;
                Vector6<double> a = aRTClientObj.actual_current;
                ulong b = aRTClientObj.actual_digital_input_bits;
                ulong c = aRTClientObj.actual_digital_output_bits;
                double d = aRTClientObj.actual_execution_time;
                Vector6<double> e1 = aRTClientObj.actual_joint_voltage;//真实关节电压
                double f = aRTClientObj.actual_main_voltage;
                double g = aRTClientObj.actual_momentum;
                double h = aRTClientObj.actual_momentum_internal1;
                double i = aRTClientObj.actual_momentum_internal2;
                j = aRTClientObj.actual_q;//关节真实位置
                Vector6<double> k = aRTClientObj.actual_qd;//关节真实速度
                double l = aRTClientObj.actual_robot_current;//机器人真实电流
                double l2 = aRTClientObj.actual_robot_voltage;//机器人真实市电电压
                Vector6<double> l3 = aRTClientObj.actual_TCP_force;//tcp的真实受力
                l4 = aRTClientObj.actual_TCP_pose;//TCP的真实位置
                Vector6<double> l5 = aRTClientObj.actual_TCP_speed;//tcp的真实速度
                Vector3<double> l6 = aRTClientObj.actual_tool_accelerometer;
                Vector6<double> l7 = aRTClientObj.actual_tool_accelerometer_internal;
                double l8 = aRTClientObj.elbow_position;
                double l9 = aRTClientObj.elbow_velocity;
                Vector6<double> l10 = aRTClientObj.joint_control_output;
                Vector6<int> l11 = aRTClientObj.joint_mode;
                Vector6<double> l12 = aRTClientObj.joint_temperatures;
                double l13 = aRTClientObj.program_state;
                double l14 = aRTClientObj.robot_mode;
                double l15 = aRTClientObj.safety_mode;
                Vector6<double> l16 = aRTClientObj.safety_mode_internal;
                double l17 = aRTClientObj.speed_scaling;
                Vector6<double> l18 = aRTClientObj.target_current;
                Vector6<double> l19 = aRTClientObj.target_moment;
                Vector6<double> l20 = aRTClientObj.target_q; //关节目标位置，单位是弧度
                Vector6<double> l21 = aRTClientObj.target_qd;//关节目标速度
                Vector6<double> l22 = aRTClientObj.target_qdd;//关节目标加速度
                Vector6<double> l23 = aRTClientObj.target_TCP_pose;//TCP目标位姿
                Vector6<double> l24 = aRTClientObj.target_TCP_speed;//TCP目标速度
                double l25 = aRTClientObj.test_value;


                //使用aRTClientObj中的数据
                //aRTClient.sendScript("movel(p[" + j1.ToString() + ",0.2,0.3,0,0,0],a=0.8,v=0.5)"); //发送脚本给机器人；
                //aRTClient.sendScript("rv=[1,2,1]"); //发送脚本给机器人
            }
            //TCP当前位置
            if (l4 != null)
            {
                //ActTCPTB_X.Text = (l4.X * 1000).ToString("0.00");
                //ActTCPTB_Y.Text = (l4.Y * 1000).ToString("0.00");
                //ActTCPTB_Z.Text = (l4.Z * 1000).ToString("0.00");
                //ActTCPTB_RX.Text = (l4.Rx * 1000).ToString("0.00");
                //ActTCPTB_RY.Text = (l4.Ry * 1000).ToString("0.00");
                //ActTCPTB_RZ.Text = (l4.Rz * 1000).ToString("0.00");
            }
            //关节当前位置
            if (j != null)
            {
                Pos[0].text = (j.X * 180 / PI).ToString("0.00");
                Pos[1].text = (j.Y * 180 / PI).ToString("0.00");
                Pos[2].text = (j.Z * 180 / PI).ToString("0.00");
                Pos[3].text = (j.Rx * 180 / PI).ToString("0.00");
                Pos[4].text = (j.Ry * 180 / PI).ToString("0.00");
                Pos[5].text = (j.Rz * 180 / PI).ToString("0.00");
            }
            yield return new WaitForSeconds(0.1f);
            #endregion
        }
     }
    private IEnumerator timer2_Tick()
    {
        while (true)
        {
            try
            {
                aRTDE.startSync(); //启动 RTDE server 同步
            }
            catch
            {
                throw;
            }
            if (aRTDE.isSynchronizing)
            {
                RTDEOutputObj outobj = aRTDE.getOutputObj(); //读取 RTDE output 数据
                CurTCPPos = outobj.actual_TCP_pose;
                //CurPX = outobj.actual_TCP_pose.X;
                //CurPY = outobj.actual_TCP_pose.Y;
                //CurPZ = outobj.actual_TCP_pose.Z;
                //CurRX = outobj.actual_TCP_pose.Rx;
                //CurRY = outobj.actual_TCP_pose.Ry;
                //CurRZ = outobj.actual_TCP_pose.Rz;

                CurJointPos = outobj.actual_q;
                //CurJ1 = outobj.actual_q.X;//机座
                //CurJ2 = outobj.actual_q.Y;//肩部
                //CurJ3 = outobj.actual_q.Z;//肘部
                //CurJ4 = outobj.actual_q.Rx;//手腕1
                //CurJ5 = outobj.actual_q.Ry;//手腕2
                //CurJ6 = outobj.actual_q.Rz;//手腕3

                string tcppose = outobj.actual_TCP_pose.ToString();
                RTDEMessage aMsg = new RTDEMessage("Content of message", "Message source",
                RTDEMessage.INFO_MESSAGE); //创建 RTDEMessage 对象， Warning Level 为 Info
                aRTDE.sendMessage(aMsg); //发送 RTDEMessage
                RTDEInputObj inobj = new RTDEInputObj(); //创建 RTDE input 对象
                                                         // 将数字输出置为 0b00000011,RTDEInputObj 同步数据一定要是 RTDEConfig.xml 文件中已经配置好的 input
                inobj.standard_digital_output = 3;
                aRTDE.setInputObj(inobj); //发送 input
            }

            //TCP当前位置
            if (CurTCPPos != null)
            {
                //ActTCPTB_X.Text = (CurTCPPos.X * 1000).ToString("0.00");
                //ActTCPTB_Y.Text = (CurTCPPos.Y * 1000).ToString("0.00");
                //ActTCPTB_Z.Text = (CurTCPPos.Z * 1000).ToString("0.00");
                //ActTCPTB_RX.Text = (CurTCPPos.Rx).ToString("0.00");
                //ActTCPTB_RY.Text = (CurTCPPos.Ry).ToString("0.00");
                //ActTCPTB_RZ.Text = (CurTCPPos.Rz).ToString("0.00");
            }
            //关节当前位置
            if (CurJointPos != null)
            {

                Pos[0].text = (CurJointPos.X * 180 / PI).ToString("0.00");
                Pos[1].text = (CurJointPos.Y * 180 / PI).ToString("0.00");
                Pos[2].text = (CurJointPos.Z * 180 / PI).ToString("0.00");
                Pos[3].text = (CurJointPos.Rx * 180 / PI).ToString("0.00");
                Pos[4].text = (CurJointPos.Ry * 180 / PI).ToString("0.00");
                Pos[5].text = (CurJointPos.Rz * 180 / PI).ToString("0.00");
            }
            aRTDE.stopSync(); //关闭 RTDE server 同步，释放资源
            yield return new WaitForSeconds(1f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        aRTClient = new RTClient(AdrIP);
        aDashBoard = new DashBoard(AdrIP);
        primaryInt = new PrimaryInterface(AdrIP, AdrPort);
        demoInstance = new PrimaryInterface(AdrIP);
        aRTDE = new RTDE("file:///F:/XC/Assets/UR10/URSDK/RTDEConfig.xml", AdrIP);
        URConnect();
        for(int i=0;i<6;i++)
            pd.Add(0);
    }
    // Update is called once per frame
    void Update()
    {
        TPos[0].localEulerAngles = new Vector3(TPos[0].localEulerAngles.x, TPos[0].localEulerAngles.y, -(float)(CurJointPos.X * 180 / PI));
        TPos[1].localEulerAngles = new Vector3(TPos[1].localEulerAngles.x, -(float)(CurJointPos.Y * 180 / PI + 90), TPos[1].localEulerAngles.z);
        TPos[2].localEulerAngles = new Vector3(TPos[2].localEulerAngles.x, -(float)(CurJointPos.Z * 180 / PI), TPos[2].localEulerAngles.z);
        TPos[3].localEulerAngles = new Vector3(TPos[3].localEulerAngles.x, TPos[3].localEulerAngles.z, -(float)(CurJointPos.Rx * 180 / PI-90));
        print(new Vector3(TPos[3].localEulerAngles.x, TPos[3].localEulerAngles.z, -(float)(CurJointPos.Rx * 180 / PI)));
        TPos[4].localEulerAngles = new Vector3(TPos[4].localEulerAngles.x, -(float)(CurJointPos.Ry * 180 / PI + 180), TPos[4].localEulerAngles.z);
        TPos[5].localEulerAngles = new Vector3(TPos[5].localEulerAngles.x, -(float)(CurJointPos.Rz * 180 / PI), TPos[5].localEulerAngles.z);
    }
    public void BtnLeftMove(int index)
    {
        for (int i = 0; i < 6; i++)
        {
            pd[i] = 0;
        }
        pd[index] = -10.0f / 180 * PI;
        string strPD = "[";
        for (int i = 0; i < 5; i++)
            strPD += (Convert.ToString(pd[i]) + ",");
        strPD += (Convert.ToString(pd[5]) + "]");
        aRTClient.sendScript("speedj(" + strPD + ",0.5,100)"); //发送脚本给机器人；
        Console.WriteLine("speedj(" + strPD + ",0.5,2)");
    }
    public void BtnRightMove(int index)
    {
        for (int i = 0; i < 6; i++)
        {
            pd[i] = 0;
        }
        pd[index] = 10.0f / 180 * PI;
        string strPD = "[";
        for (int i = 0; i < 5; i++)
            strPD += (Convert.ToString(pd[i]) + ",");
        strPD += (Convert.ToString(pd[5]) + "]");
        aRTClient.sendScript("speedj(" + strPD + ",0.3,100)"); //发送脚本给机器人；
    }
    public void BtnHalt()
    {
        aRTClient.sendScript("halt");
    }
    public void BtnSetZero()
    {

    }
}
