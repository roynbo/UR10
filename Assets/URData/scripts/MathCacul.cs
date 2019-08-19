using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathCacul : MonoBehaviour
{
    public GameObject imaghWarning;
    public Text txStatus;
    public List<Transform> rhsObject;
    public List<Transform> lhsObject;
    double minDistance = 500f;
    int minIndexLeft = 0;
    int minIndexRight = 0;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        minDistance = 500f;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                double tmp = SqureDistanceSegmentToSegment(rhsObject[i].position.x, rhsObject[i].position.y, rhsObject[i].position.z,
                                                 rhsObject[i + 1].position.x, rhsObject[i + 1].position.y, rhsObject[i + 1].position.z,
                                                 lhsObject[j].position.x, lhsObject[j].position.y, lhsObject[j].position.z,
                                                 lhsObject[j + 1].position.x, lhsObject[j + 1].position.y, lhsObject[j + 1].position.z);
                if (tmp < minDistance)
                {
                    minDistance = tmp;
                    minIndexLeft =j+1;
                    minIndexRight = i + 1;

                }
            }
        }
        if(minDistance<1)
        {
            minDistance *= 30;
            imaghWarning.SetActive(true);
            canvasGroup = imaghWarning.GetComponent<CanvasGroup>();
            StartCoroutine(Change());
            txStatus.text = "左" + minIndexLeft + "轴和右" + minIndexRight + "轴相距最近" + minDistance.ToString("0.00")+"cm";
        }
        else
        {
            txStatus.text = "安全";
            imaghWarning.SetActive(false);
        }
        print("左" + minIndexLeft + "轴和右" + minIndexRight + "轴相距最近" + minDistance);
    }

    public bool IsEqual(float d1, float d2)
    {
        if (Mathf.Abs(d1 - d2) < 1e-7)
            return true;
        return false;
    }

    public double SqureDistanceSegmentToSegment(float x1, float y1, float z1,
                                                float x2, float y2, float z2,
                                                float x3, float y3, float z3,
                                                float x4, float y4, float z4)
    {
        // 解析几何通用解法，可以求出点的位置，判断点是否在线段上
        // 算法描述：设两条无限长度直线s、t,起点为s0、t0，方向向量为u、v
        // 最短直线两点：在s1上为s0+sc*u，在t上的为t0+tc*v
        // 记向量w为(s0+sc*u)-(t0+tc*v),记向量w0=s0-t0
        // 记a=u*u，b=u*v，c=v*v，d=u*w0，e=v*w0——(a)；
        // 由于u*w=、v*w=0，将w=-tc*v+w0+sc*u带入前两式得：
        // (u*u)*sc - (u*v)*tc = -u*w0  (公式2)
        // (v*u)*sc - (v*v)*tc = -v*w0  (公式3)
        // 再将前式(a)带入可得sc=(be-cd)/(ac-b2)、tc=(ae-bd)/(ac-b2)——（b）
        // 注意到ac-b2=|u|2|v|2-(|u||v|cosq)2=(|u||v|sinq)2不小于0
        // 所以可以根据公式（b）判断sc、tc符号和sc、tc与1的关系即可分辨最近点是否在线段内
        // 当ac-b2=0时，(公式2)(公式3)独立，表示两条直线平行。可令sc=0单独解出tc
        // 最终距离d（L1、L2）=|（P0-Q0)+[(be-cd)*u-(ae-bd)v]/(ac-b2)|
        float ux = x2 - x1;
        float uy = y2 - y1;
        float uz = z2 - z1;
        float vx = x4 - x3;
        float vy = y4 - y3;
        float vz = z4 - z3;
        float wx = x1 - x3;
        float wy = y1 - y3;
        float wz = z1 - z3;
        float a = (ux * ux + uy * uy + uz * uz); //u*u
        float b = (ux * vx + uy * vy + uz * vz); //u*v
        float c = (vx * vx + vy * vy + vz * vz); //v*v
        float d = (ux * wx + uy * wy + uz * wz); //u*w 
        float e = (vx * wx + vy * wy + vz * wz); //v*w
        float dt = a * c - b * b;
        float sd = dt;
        float td = dt;
        float sn = 0.0f;//sn = be-cd
        float tn = 0.0f;//tn = ae-bd
        if (IsEqual(dt, 0.0f))
        {
            //两直线平行
            sn = 0.0f;    //在s上指定取s0
            sd = 1.00f;   //防止计算时除0错误
            tn = e;      //按(公式3)求tc
            td = c;
        }
        else
        {
            sn = (b * e - c * d);
            tn = (a * e - b * d);
            if (sn < 0.0)
            {
                //最近点在s起点以外，同平行条件
                sn = 0.0f;
                tn = e;
                td = c;
            }
            else if (sn > sd)
            {
                //最近点在s终点以外(即sc>1,则取sc=1)
                sn = sd;
                tn = e + b; //按(公式3)计算
                td = c;
            }
        }
        if (tn < 0.0)
        {
            //最近点在t起点以外
            tn = 0.0f;
            if (-d < 0.0) //按(公式2)计算，如果等号右边小于0，则sc也小于零，取sc=0
                sn = 0.0f;
            else if (-d > a) //按(公式2)计算，如果sc大于1，取sc=1
                sn = sd;
            else
            {
                sn = -d;
                sd = a;
            }
        }
        else if (tn > td)
        {
            tn = td;
            if ((-d + b) < 0.0)
                sn = 0.0f;
            else if ((-d + b) > a)
                sn = sd;
            else
            {
                sn = (-d + b);
                sd = a;
            }
        }
        double sc = 0.0;
        double tc = 0.0;
        if (IsEqual(sn, 0.0f))
            sc = 0.0;
        else
            sc = sn / sd;
        if (IsEqual(tn, 0.0f))
            tc = 0.0;
        else
            tc = tn / td;
        double dx = wx + (sc * ux) - (tc * vx);
        double dy = wy + (sc * uy) - (tc * vy);
        double dz = wz + (sc * uz) - (tc * vz);
        return dx * dx + dy * dy + dz * dz;
    }

    IEnumerator Change()
    {
        canvasGroup.alpha = 0.3f;
        for (int i = 0; i < 7; i++)
        {
            canvasGroup.alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);     //每 0.1s 加一次
        }
        StopCoroutine(Change());
    }
}
