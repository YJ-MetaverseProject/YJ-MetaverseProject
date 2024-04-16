using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject option_Background;
    [SerializeField] GameObject option_Btn;
    [SerializeField] GameObject voice_Btn;
    [SerializeField] GameObject option;
    [SerializeField] GameObject voice;
    [SerializeField] GameObject aim;
    [SerializeField] GameObject manual;
    [SerializeField] GameObject mic_on;
    [SerializeField] GameObject mic_off;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Option"))
        {
            Input_ESC();
        }else if (Input.GetButtonDown("Manual"))
        {
            Input_Manual();
        }else if (Input.GetButtonDown("Mic"))
        {
            Input_Mic();
        }
    }
    private void Input_ESC()
    {
        if(option_Background.activeSelf)
        {
            option_Background.SetActive(false);
            aim.SetActive(true);
        }
        else if(option_Background.activeSelf == false)
        {
            option_Background.SetActive(true);
            aim.SetActive(false);
        }
    }
    private void Input_Manual()
    {
        if (manual.activeSelf)
        {
            manual.SetActive(false);
        }
        else if (manual.activeSelf == false)
        {
            manual.SetActive(true);
        }
    }

    private void Input_Mic()
    {
        if(mic_off.activeSelf)
        {
            mic_off.SetActive(false);
            mic_on.SetActive(true);
        }
        else if (mic_on.activeSelf)
        {
            mic_off.SetActive(true);
            mic_on.SetActive(false);
        }
    }

    public void Option_Btn()
    {
        option.SetActive(true);
        voice.SetActive(false);
    }
    public void Voice_Btn()
    {
        option.SetActive(false);
        voice.SetActive(true);
    }
}
