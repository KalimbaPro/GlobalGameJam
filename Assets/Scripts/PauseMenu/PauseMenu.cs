using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUi;
    public AudioSource levelMusic;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Paused()
    {
        levelMusic.Pause();
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    private IEnumerator playMusicDelay()
    {
        yield return new WaitForSeconds(0.40f);
    }

    public void Resume()
    {
        levelMusic.UnPause();
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
