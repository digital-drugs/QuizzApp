using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartGame(int sceneIndex)//void ~ nothing/
    {
        SceneManager.LoadScene(sceneIndex);

    }
}