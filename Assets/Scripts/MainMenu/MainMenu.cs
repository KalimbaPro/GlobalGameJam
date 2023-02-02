using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad;

    public GameObject buttons;
    public GameObject settingsWindow;

    public GameObject mainMenuCanvas;

    public void Start()
    {
        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, true);
    }

    private IEnumerator startDelay()
    {
        yield return new WaitForSeconds(0.40f);
        SceneManager.LoadScene(levelToLoad);
    }

    public void StartGame()
    {
        StartCoroutine(startDelay());
    }

    private IEnumerator settingDelay()
    {

        yield return new WaitForSeconds(0.40f);
        settingsWindow.SetActive(true);
        buttons.SetActive(false);
    }

    public void SettingButton()
    {

        StartCoroutine(settingDelay());
    }

    private IEnumerator closeSettingDelay()
    {
        yield return new WaitForSeconds(0.40f);
        settingsWindow.SetActive(false);
        Cursor.visible = true;
        buttons.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        StartCoroutine(closeSettingDelay());
    }
    private IEnumerator quitDelay()
    {

        yield return new WaitForSeconds(0.40f);
        Application.Quit();
    }

    public void QuitGame()
    {
        StartCoroutine(quitDelay());
    }
}
