using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    public GameObject m_BtnContinue;
    public Button m_ClickToContinue;
    public Image m_LoadBar;
    public Image m_Loading;

    void Start()
    {
        StartCoroutine(DisplayButton());
        StartCoroutine(StartScaleCoroutine());

        m_LoadBar.fillAmount = 0f;
        m_ClickToContinue.onClick.AddListener(StartTheGame);
    }

    IEnumerator DisplayButton()
    {
        yield return new WaitForSeconds(7f);
        m_BtnContinue.SetActive(true);
    }

    public void StartTheGame()
    {
        StartCoroutine(LoadAsync());
        m_Loading.gameObject.SetActive(true);
        m_BtnContinue.SetActive(false);
    }

    //If I click on the "continue" button, the loading bar start
    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            m_LoadBar.fillAmount = progress;

            yield return null;
        }
    }

    //Animation for the progress bar until the game is loaded
    public IEnumerator FillAmountBar(float duration)
    {
        float startRotation = 0f;
        float endRotation = 1f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float yRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / duration);
            m_LoadBar.fillAmount = yRotation;

            yield return null;
        }
    }

    //Repeat the coroutine
    public IEnumerator StartScaleCoroutine()
    {
        StartCoroutine(ButtonScale(1f, new Vector3(0.70f, 0.70f, 0.70f)));
        yield return new WaitForSeconds(1f);
        StartCoroutine(ButtonScale(1f, new Vector3(0.80f, 0.80f, 0.80f)));
        yield return new WaitForSeconds(1f);
        StartCoroutine(StartScaleCoroutine());
    }

    //Animation of the button play in the menu
    public IEnumerator ButtonScale(float duration, Vector3 end)
    {
        Vector3 startRotation = m_ClickToContinue.transform.localScale;
        Vector3 endRotation = end;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            m_ClickToContinue.transform.localScale = Vector3.Lerp(startRotation, endRotation, elapsedTime / duration);

            yield return null;
        }
    }
}
