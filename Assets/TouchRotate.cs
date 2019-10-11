using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchRotate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject MainCamera;
    public Transform target;
    float speed = 50.0f;
    public GameObject[] UIShow = new GameObject[2];
    Vector3 zeroV;
    Quaternion zeroQ;
    public GameObject btnZero;
    void Start()
    {
        zeroV = MainCamera.transform.position;
        zeroQ = MainCamera.transform.rotation;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("开始拖拽");
        UIShow[0].SetActive(false);
        UIShow[1].SetActive(false);
        btnZero.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 获得鼠标当前位置的X和Y

        float mouseX = Input.GetAxis("Mouse X") ;
        float mouseY = Input.GetAxis("Mouse Y") ;
        if (Mathf.Abs(mouseX) > Mathf.Abs(mouseY))
        {
                MainCamera.transform.RotateAround(new Vector3(0,0,300), Vector3.up, speed * mouseX * Time.deltaTime);
        }
        else
        {
            if (Mathf.Abs(MainCamera.transform.eulerAngles.y) < 45)
                MainCamera.transform.RotateAround(new Vector3(0, 0, 300), Vector3.left, speed * mouseY * Time.deltaTime);
            else if (Mathf.Abs(MainCamera.transform.eulerAngles.y) > 135)
                MainCamera.transform.RotateAround(new Vector3(0, 0, 300), Vector3.left, -speed * mouseY * Time.deltaTime);
            else
                MainCamera.transform.RotateAround(new Vector3(0, 0, 300), Vector3.forward, speed * mouseY * Time.deltaTime);
        }


        }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("结束拖拽");
        UIShow[0].SetActive(true);
        UIShow[1].SetActive(true);

    }
    public void BackToZero()
    {
        MainCamera.transform.position = zeroV;
        MainCamera.transform.rotation = zeroQ;
        btnZero.SetActive(false);
    }
}
