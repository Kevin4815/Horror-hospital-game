using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MainGame : MonoBehaviour
{
    bool m_Pause = false;

    public GameObject m_PauseMenu;

    public AudioSource m_GameSound;
    public AudioSource m_EnemyOneSound;
    public AudioSource m_EnemyTwoSound;
    public AudioSource m_MonsterSound;

    public FirstPersonController m_FPS;

    public Button m_Resume;
    public Button m_ReturnToMenu;


    void Start()
    {
        m_FPS.m_MouseLook.lockCursor = false;
        CurrentGame();
        m_PauseMenu.SetActive(false);
        m_ReturnToMenu.onClick.AddListener(ReturnToMenu);
        m_Resume.onClick.AddListener(Resume);
    }

    void Update()
    {
        //Display a pause menu if I click on "escap"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_Pause = !m_Pause;

            if(m_Pause == true)
            {
                PauseGame();
            }
            else
            {
                CurrentGame();
            }
        }
    }

    //If the game  = pause, stop sounds and images of the game
    public void SoundsOfEnemy(bool sound)
    {
        m_GameSound.Pause();
        m_EnemyOneSound.enabled = sound;
        m_MonsterSound.enabled = sound;

        if (KeepCard.m_IsAwake == true)
        {
            m_EnemyTwoSound.enabled = sound;
        }
    }

    public void PauseGame()
    {
        m_PauseMenu.SetActive(true);
        Time.timeScale = 0;
        m_FPS.enabled = false;
        m_GameSound.Pause();
        m_EnemyOneSound.Pause();
        m_MonsterSound.Pause();

        if (KeepCard.m_IsAwake == true)
        {
            m_EnemyTwoSound.Pause();
        }
    }

    public void CurrentGame()
    {
        m_PauseMenu.SetActive(false);
        Time.timeScale = 1;
        m_FPS.enabled = true;
        m_GameSound.Play();
        m_EnemyOneSound.Play();
        m_MonsterSound.Play();

        if (KeepCard.m_IsAwake == true)
        {
            m_EnemyTwoSound.Play();
        }
    }

    public void ReturnToMenu()
    {
        TestLoad.LoadMenuScene();
        Playerdeath.Rebootvariable();
    }

    public void Resume()
    {
        m_Pause = false;
        CurrentGame();
    }
}
