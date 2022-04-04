using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class KeepCard : MonoBehaviour
{
    public ArmCollider m_ArmCollider;
    public GameObject m_Card;
    public AudioClip m_Keep;
    AudioSource m_audio;
    public Animator m_PlayerAnimator;
    public float m_Inclination;
    public SecondEnemy m_SecondEnemy;

    public Transform m_Player;
    public Transform m_Teleporte;
    public Transform m_Camera;
    public CharacterController m_CharacterController;
    public GameObject m_FPS;

    public static bool m_IsCatch = false;
    public static bool m_NearByObject = false;
    public static bool m_IsKey = false;
    public static bool m_IsCard = false;
    public static bool m_IsFirstAid = false;
    public static bool m_HealthInfo = false;
    public static bool m_ClickEnter = false;
    public static bool m_IsAwake = false;

    public void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "RigidBodyFPSController")
        {
            if (m_IsCatch == false)
            {
                m_NearByObject = true;
            }

            if (m_Card.name == "key")
            {
                //If the player is near the key and push the enter button, the animation for catch the key will start
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if(OpenDoorLvRoom.m_ActionFinish == true)
                    {
                        m_IsKey = true;
                        StartCoroutine(FacingOfTheObject());
                        m_PlayerAnimator.SetTrigger("Catch");
                        m_audio.PlayOneShot(m_Keep);
                        m_NearByObject = false;
                        m_IsKey = false;
                        m_IsCatch = true;
                        m_IsKey = true;
                    }
                }
            }
            else if (m_Card.name == "card")
            {
                //If the player is near the card and push the enter button, the animation for catch the card will start
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if(OpenDoorLvRoom.m_ActionFinish == true)
                    {
                        m_IsCard = true;
                        StartCoroutine(FacingOfTheObject());
                        m_PlayerAnimator.SetTrigger("Catch");
                        m_audio.PlayOneShot(m_Keep);
                        m_NearByObject = false;
                        m_IsCatch = true;
                        m_IsKey = false;
                        m_SecondEnemy.enabled = true;
                        m_IsAwake = true;
                    }
                }
            }
            else if(m_Card.name == "firstAid")
            {
                m_IsFirstAid = true;

                //If the player is near the first aid box and push the enter button, the animation for catch the first ai box will start
                //The health of the player += 33%
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    m_ClickEnter = true;

                    if(OpenDoorLvRoom.m_ActionFinish == true)
                    {
                        if (m_ArmCollider.m_Health.fillAmount < 1)
                        {
                            StartCoroutine(FacingOfTheObject());
                            m_PlayerAnimator.SetTrigger("Catch");
                            m_audio.PlayOneShot(m_Keep);
                            m_NearByObject = false;
                            m_IsCatch = true;
                            m_ArmCollider.m_Health.fillAmount += 0.34f;
                        }
                        else
                        {
                            StartCoroutine(CheckHealth());
                        }
                    }
                }
            }
            m_PlayerAnimator.SetBool("Idle", true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "RigidBodyFPSController")
        {
            m_NearByObject = false;
        }
    }

    //When the player catch an object, he sits in front of the object to catch it
    public IEnumerator FacingOfTheObject()
    {
        OpenDoorLvRoom.m_ActionFinish = false;
        MixamoPlayer.m_StopRun = true;
        m_Camera.transform.localRotation = Quaternion.Euler(m_Inclination, 0f, 0f);
        m_FPS.GetComponent<FirstPersonController>().enabled = false;
        m_Player.transform.rotation = new Quaternion(m_Teleporte.rotation.x, m_Teleporte.rotation.y, m_Teleporte.rotation.z, m_Teleporte.rotation.w);
        m_Player.transform.position = m_Teleporte.position;
        yield return new WaitForSeconds(1f);
        m_Card.SetActive(false);
        m_FPS.GetComponent<FirstPersonController>().enabled = true;
        MixamoPlayer.m_StopRun = false;
        OpenDoorLvRoom.m_ActionFinish = true;
    }

    public IEnumerator CheckHealth()
    {
        m_HealthInfo = true;
        yield return new WaitForSeconds(1f);
        m_HealthInfo = false;
        m_IsCatch = false;
    }
}
