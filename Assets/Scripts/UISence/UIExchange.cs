using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExchange : MonoBehaviour
{
    enum Mode
    {
      Auto,
      Manual
    };
    Mode mode;
    [SerializeField] Animation aniModeExchange;
    [SerializeField] GameObject buttonMode;
    Text txMode;
    // Start is called before the first frame update
    void Start()
    {
        mode = Mode.Auto;
        txMode = buttonMode.GetComponentInChildren<Text>();
        txMode.text = "切换至\n手动模式";
    }

    // Update is called once per frame
    void Update()
    {
        if(aniModeExchange.isPlaying)
        {
            txMode.text = "模式切换中";
            buttonMode.GetComponent<Button>().interactable = false;
        }
        else
        {
            buttonMode.GetComponent<Button>().interactable = true;
            if (mode==Mode.Auto)
            {
                txMode.text = "切换至\n手动模式";
            }
            else
            {
                txMode.text = "切换至\n自动模式";
            }
        }
    }
    public void btnModeExchange()
    {
        switch (mode)
        {
            case Mode.Auto:
                {
                    aniModeExchange["ModeExchange"].time = 0;
                    aniModeExchange["ModeExchange"].speed = 1;
                    aniModeExchange.Play("ModeExchange");
                    mode = Mode.Manual;
                    break;
                }
            case Mode.Manual:
                {
                    aniModeExchange["ModeExchange"].time = aniModeExchange["ModeExchange"].clip.length;
                    aniModeExchange["ModeExchange"].speed = -1;
                    aniModeExchange.Play("ModeExchange");
                    mode = Mode.Auto;
                    break;
                }
            default:
                break;
        }
    }
}
