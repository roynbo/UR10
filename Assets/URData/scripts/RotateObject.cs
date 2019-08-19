using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateObject : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Transform target;
    public float speed = 0.3f;
    Vector3 startTrans=new Vector3(0,0,0);
    public bool comeback = false;
    public float speed_tmp = 50f;
    public static bool canControl = true;
    private void FixedUpdate()
    {
       

        //print(speed_tmp);
        if (comeback)
        {
            target.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y+Time.deltaTime * speed_tmp, target.eulerAngles.z);
            if (Vector3.Distance(target.eulerAngles, startTrans)<1f)
            {
                comeback = false;
                canControl = true;
                target.eulerAngles = Vector3.zero;
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        comeback = false;
        canControl = false;
        Vector3 Vec3rote = new Vector3(0, -eventData.delta.x,0);
        target.Rotate(Vec3rote * speed, Space.Self);
        //throw new System.NotImplementedException();
        if (target.eulerAngles.y > 0 && target.eulerAngles.y < 180)
            speed_tmp = -50f;
        else
            speed_tmp = 50f;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        comeback = true;
        //target.eulerAngles = startTrans;
    }

}
