using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoad : MonoBehaviour
{
    public static void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadEndScene()
    {
        SceneManager.LoadScene(3);
    }

}
