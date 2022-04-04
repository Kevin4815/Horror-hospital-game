using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Button m_Play;
    public Button m_Exit;
    public Image m_LoadBar;
    public Image m_Loading;

    public void Start()
    {
        m_Play.onClick.AddListener(ClickToPlay);
        m_Exit.onClick.AddListener(ClickToExit);
        StartCoroutine(StartScaleCoroutine());
    }

    public void ClickToPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickToExit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }

    //Animation scale of the button Play
    public IEnumerator StartScaleCoroutine()
    {
        StartCoroutine(ButtonScale(1f, new Vector3(0.75f, 0.75f, 0.75f)));
        yield return new WaitForSeconds(1f);
        StartCoroutine(ButtonScale(1f, new Vector3(0.85f, 0.85f, 0.85f)));
        yield return new WaitForSeconds(1f);
        StartCoroutine(StartScaleCoroutine());
    }

    public IEnumerator ButtonScale(float duration, Vector3 end)
    {
        Vector3 startRotation = m_Play.transform.localScale;
        Vector3 endRotation = end;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            m_Play.transform.localScale = Vector3.Lerp(startRotation, endRotation, elapsedTime / duration);

            yield return null;
        }
    }
}
