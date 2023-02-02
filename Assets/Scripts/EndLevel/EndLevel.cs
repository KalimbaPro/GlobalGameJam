using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public string levelToLoad;
    public string MainMenuToLoad;

    private IEnumerator MenuDelay()
    {
        yield return new WaitForSeconds(0.40f);
        SceneManager.LoadScene(MainMenuToLoad);
    }

    public void MainMenu()
    {
        StartCoroutine(MenuDelay());
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
