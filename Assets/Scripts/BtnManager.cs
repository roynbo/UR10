using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BtnManager : MonoBehaviour
{
    IOManager leftIO, rightIO;
    [SerializeField] Toggle[] leftToggles = new Toggle[2];
    [SerializeField] Toggle[] rightToggles = new Toggle[4];
    [SerializeField] float sleepTime = 3.0f;
    float leftOnTime, rightOnTime;
    float waitTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        leftIO = new IOManager(2);
        rightIO = new IOManager(4);
        leftIO.ToggleSet = leftToggles;
        rightIO.ToggleSet = rightToggles;
        leftIO.Init();
        rightIO.Init();
        leftIO.sleepTimeSet = sleepTime;
        rightIO.sleepTimeSet = sleepTime;
    }

    // Update is called once per frame
    void Update()
    {
        leftIO.Monitor();
        rightIO.Monitor();
    }
}
