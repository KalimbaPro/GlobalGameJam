using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public string toLoad;
    public void loadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(toLoad);
    }
}
