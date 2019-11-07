using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URDate;
public class CommandScripts
{
    public static string MoveStop()
    {
        string StopCommand = "stopl(1)";
        //CustomCommand.Text = StopCommand;
        return StopCommand;
    }
    public static string MoveAxis(int index,int direction,double AccelerationRate, double SpeedRate,double[] CurrentPos)
    {
        //不管怎么样都要获取当前的坐标值
        double new_X = CurrentPos[0] / 180 * 3.14;
        double new_Y = CurrentPos[1] / 180 * 3.14;
        double new_Z = CurrentPos[2] / 180 * 3.14;
        double new_U = CurrentPos[3] / 180 * 3.14;
        double new_V = CurrentPos[4] / 180 * 3.14;
        double new_W = CurrentPos[5] / 180 * 3.14;

        //然后根据点动的按钮，判断要改哪个值(这里不是旋转，只有X,Y,Z三种可能)，直接覆盖到真实的当前XYZ值
        if (index == 0)
        {
            new_X = (new_X + 1.57 * direction);
        }
        else if (index == 1)
        {
            new_Y = (new_Y + 1.57 * direction);
        }
        else if (index == 2)
        {
            new_Z = (new_Z + 1.57 * direction);
        }
        else if (index == 3)
        {
            new_U = (new_U + 1.57 * direction);
        }
        else if (index == 4)
        {
            new_V = (new_V + 1.57 * direction);
        }
        else if (index == 5)
        {
            new_W = (new_W + 1.57 * direction);
        }
        else
        {
            //也有可能我不要移动，只是要看指令
        }


        //最后把方向运动的指令发送出去
        string command = "movej([" + new_X.ToString("0.0000") + "," + new_Y.ToString("0.0000") + "," + new_Z.ToString("0.0000") + "," + new_U.ToString("0.0000") + "," + new_V.ToString("0.0000") + "," + new_W.ToString("0.0000") + "], a = " + AccelerationRate.ToString() + ", v = " + SpeedRate.ToString() + ")";
        //CustomCommand.Text = command;
        return command;
    }

    public static string IO(int index,bool flag)
    {
        string command = "set_digital_out(" + index.ToString() + "," + flag.ToString() + ")";
        return command;
    }
    public static string Home(double AccelerationRate, double SpeedRate)
    {
        double[] homePos = new double[6];
        homePos[0] = 0/180*3.14 ;
        homePos[1] = -90.0 / 180 * 3.14;
        homePos[2] = 0 / 180 * 3.14;
        homePos[3] = -90.0 / 180 * 3.14;
        homePos[4] = 0 / 180 * 3.14;
        homePos[5] = 0 / 180 * 3.14;
        string command= "movej([" + homePos[0].ToString("0.0000") + "," + homePos[1].ToString("0.0000") + "," + homePos[2].ToString("0.0000") + "," + homePos[3].ToString("0.0000") + "," + homePos[4].ToString("0.0000") + "," + homePos[5].ToString("0.0000") + "], a = " + AccelerationRate.ToString() + ", v = " + SpeedRate.ToString() + ")";
        return command;
    }

    public static string MissionDo(Mission mission, double AccelerationRate, double SpeedRate)
    {
        double[] Pos = new double[6];
        for (int i = 0; i < 6; i++)
            Pos[i] = mission.Angles[i] / 180 * 3.14;
        string command = "movej([" + Pos[0].ToString("0.0000") + "," + Pos[1].ToString("0.0000") + "," + Pos[2].ToString("0.0000") + "," + Pos[3].ToString("0.0000") + "," + Pos[4].ToString("0.0000") + "," + Pos[5].ToString("0.0000") + "], a = " + AccelerationRate.ToString() + ", v = " + SpeedRate.ToString() + ")";
        return command;
    }

    public static string MoveTCP(string whatAxis,int direction, double AccelerationRate, double SpeedRate)
    {
        //不管怎么样都要获取当前的坐标值
        double new_X = URDateHandle.Positions_X;
        double new_Y = URDateHandle.Positions_Y;
        double new_Z = URDateHandle.Positions_Z;
        double new_U = URDateHandle.Positions_U;
        double new_V = URDateHandle.Positions_V;
        double new_W = URDateHandle.Positions_W;

        //然后根据点动的按钮，判断要改哪个值(这里不是旋转，只有X,Y,Z三种可能)，直接覆盖到真实的当前XYZ值
        if (whatAxis == "X")
        {
            new_X = ((new_X + 10) * direction);
        }
        else if (whatAxis == "Y")
        {
            new_Y = ((new_Y + 10) * direction);
        }
        else if (whatAxis == "Z")
        {
            new_Z = ((new_Z + 10) * direction);
        }
        else
        {
            //也有可能我不要移动，只是要看指令
        }


        //最后把方向运动的指令发送出去
        string command = "movel(p[" + new_X.ToString("0.0000") + "," + new_Y.ToString("0.0000") + "," + new_Z.ToString("0.0000") + "," + new_U.ToString("0.0000") + "," + new_V.ToString("0.0000") + "," + new_W.ToString("0.0000") + "], a = " + AccelerationRate.ToString() + ", v = " + SpeedRate.ToString() + ")";
        //CustomCommand.Text = command;
        return command;
    }
}
