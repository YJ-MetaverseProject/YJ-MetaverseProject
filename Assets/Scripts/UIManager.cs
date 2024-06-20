using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] GameObject quitGame;
    [SerializeField] GameObject quit_popup;
    [SerializeField] GameObject retryGame;
    [SerializeField] GameObject retry_popup;
    [SerializeField] GameObject setting_text;
    [SerializeField] GameObject yes_Btn;
    [SerializeField] GameObject No_Btn;
    [SerializeField] GameObject back;
    [SerializeField] GameObject Agreement;
    [SerializeField] GameObject Game_Start;
    private void Start()
    {
        Default_ESCUI();
        MouseLock();
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
            Time.timeScale = 1;
            MouseLock();
        }
        else if(option_Background.activeSelf == false)
        {
            option_Background.SetActive(true);
            Default_ESCUI();
            aim.SetActive(false);
            Time.timeScale = 0;
            MouseUnlock();
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
    public void Green_Btn_Clicked()
    {
        Game_Start.SetActive(false);
        Agreement.SetActive(true);
    }
    public void Option_Btn()
    {
        option.SetActive(true);
        voice.SetActive(false);
        quitGame.SetActive(false);
        retryGame.SetActive(false);
        back.SetActive(true);
    }
    public void Voice_Btn()
    {
        option.SetActive(false);
        voice.SetActive(true);
        quitGame.SetActive(false);
        retryGame.SetActive(false);
        back.SetActive(true);
    }

    public void QuitBtn()
    {
        voice_Btn.SetActive(false);
        option_Btn.SetActive(false);
        quitGame.SetActive(false);
        setting_text.SetActive(false);
        retryGame.SetActive(false);
        quit_popup.SetActive(true);
    }
    public void QuitYesBtn()
    {
        Application.Quit();
    }

    public void RetryBtn()
    {
        voice_Btn.SetActive(false);
        option_Btn.SetActive(false);
        quitGame.SetActive(false);
        retryGame.SetActive(false);
        setting_text.SetActive(false);
        retry_popup.SetActive(true);
    }
    public void RetryYesBtn()
    {
        PhotonManager.Instance.ReTry();
    }


    public void Default_ESCUI()
    {
        setting_text.SetActive(true);
        voice_Btn.SetActive(true);
        option_Btn.SetActive(true);
        quitGame.SetActive(true);
        retryGame.SetActive(true);
        option.SetActive(false);
        voice.SetActive(false);
        quit_popup.SetActive(false);
        retry_popup.SetActive(false);
        back.SetActive(false);
    }
    public void MouseLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void MouseUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
