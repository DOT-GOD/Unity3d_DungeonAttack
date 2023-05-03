using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject _controlButton;             //지정필요 : 컨트롤버튼그룹(이동,시점,대쉬,공격)
    public GameObject _settingWindow;             //지정필요 : 설정메뉴
    public GameObject _settingButton;             //지정필요 : 설정버튼

    public void ActiveSettingWindow()
    {
        _settingWindow.SetActive(true);
        _settingButton.SetActive(false);
        _controlButton.SetActive(false);
    }


    public void QuitButton()
    {
        _settingWindow.SetActive(false);
        _settingButton.SetActive(true);
        _controlButton.SetActive(true);
    }

    public void LoadButton()
    {
        JsonManager _json = new JsonManager();
        _json.Load();
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ResetButton()
    {
        JsonManager _json = new JsonManager();
        _json.ResetGame();
        Application.Quit();
    }
}
