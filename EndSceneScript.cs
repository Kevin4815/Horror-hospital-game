using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(LoadMenuScene());
    }

    public static IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }
}
