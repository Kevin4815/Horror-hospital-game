using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmCollider : MonoBehaviour
{
    public Image m_Health;
    public AudioClip m_PlayerHurt;
    public AudioClip m_GameOver;
    AudioSource m_Audio;
    public static bool m_IsDead;

    public void Start()
    {
        m_Audio = GetComponent<AudioSource>();
        m_Health.fillAmount = 1f;
        m_IsDead = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        //If the arm of the EnemyOne touch the player, m_Health -= 0.34f
        if(collider.gameObject.name == "RigidBodyFPSController")
        {
            m_Health.fillAmount -= 0.34f;
            m_Audio.PlayOneShot(m_PlayerHurt);

            if(m_Health.fillAmount <= 0f)
            {
                m_IsDead = true;
                m_Audio.PlayOneShot(m_GameOver);
                StartCoroutine(ReturnToMenu());
            }
        }
    }

    public IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(4f);
        TestLoad.LoadMenuScene();
    }
}
