using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class OpenDoorLvRoom : MonoBehaviour
{
    public Animator m_PlayerAnimator;
    public AudioClip m_Lock;
    public AudioClip m_UnLock;
    AudioSource m_Audio;

    public CharacterController m_CharacterController;
    public Collider m_DoorColliderTrigger;

    public GameObject m_FPS;
    public GameObject m_TheEndCanvas;

    public Transform m_Door;
    public Transform m_Player;
    public Transform m_Camera;
    public Transform m_Teleporte;

    public static bool m_Open;
    public static bool m_NearByDoor = false;
    public static bool m_ClickEnter = false;
    public static bool m_EnterMessage = false;
    public static bool m_ClickButton = false;
    public static bool m_ActionFinish = true;

    public void Start()
    {
        m_Audio = GetComponent<AudioSource>();
    }

    public void OnTriggerStay(Collider other)
    {
        //Check which door the player is placed
        if (other.gameObject.name == "RigidBodyFPSController")
        {
            KeepCard.m_NearByObject = true;

            if (m_Door.name == "OutsideDoor" || m_Door.name == "DoorToOpen")
            {
                m_EnterMessage = true;

                if (m_Open == false)
                {
                    m_NearByDoor = true;
                }

                //If the player has already catch the key, the door opens
                if (Input.GetKeyDown(KeyCode.Return) && KeepCard.m_IsCatch == true)
                {
                    if(m_ActionFinish == true)
                    {
                        StartCoroutine(FacingOfTheObject());
                        m_DoorColliderTrigger.enabled = false;
                        StartCoroutine(TimeBeforeOpen(m_Door.rotation.y, 1.7f, 90f));
                        KeepCard.m_NearByObject = false;
                    }
                }
                //Otherwise, it remains closed
                else if (Input.GetKeyDown(KeyCode.Return) && KeepCard.m_IsCatch == false)
                {
                    if(m_ActionFinish == true)
                    {
                        m_ClickEnter = true;
                        StartCoroutine(FacingOfTheObject());
                        m_PlayerAnimator.SetTrigger("Open");
                        KeepCard.m_NearByObject = false;
                        StartCoroutine(WaitForSound());
                    }
                }
            }

            if (m_Door.name == "DoorMiddleOpen")
            {
                m_EnterMessage = true;

                if (m_Open == false)
                {
                    m_NearByDoor = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    StartCoroutine(FacingOfTheObject());
                    m_DoorColliderTrigger.enabled = false;
                    StartCoroutine(TimeBeforeOpen(15f, 1.7f, 80f));
                    KeepCard.m_NearByObject = false;
                    m_NearByDoor = false;
                }
            }

            if (m_Door.name == "LastDoor")
            {
                m_EnterMessage = true;

                if (m_Open == false)
                {
                    m_NearByDoor = true;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    m_PlayerAnimator.SetTrigger("Open");
                    StartCoroutine(FacingOfTheObject());
                    StartCoroutine(TimeBeforeOpen(m_Door.rotation.y, 2f, 90f));
                    StartCoroutine(TheEnd());
                }
            }

            if (m_Door.name == "GridToOpen")
            {
                m_ClickButton = true;

                if (m_Open == false)
                {
                    m_NearByDoor = true;
                }

                if (Input.GetKeyDown(KeyCode.Return) && KeepCard.m_IsCatch == true)
                {
                    StartCoroutine(FacingOfTheObject());
                    m_DoorColliderTrigger.enabled = false;
                    StartCoroutine(TimeBeforeOpen());
                    KeepCard.m_NearByObject = false;
                    KeepCard.m_IsKey = false;
                    KeepCard.m_IsCatch = false;
                    m_ClickButton = false;
                    m_Open = true;
                }
                else if (Input.GetKeyDown(KeyCode.Return) && KeepCard.m_IsCatch == false)
                {
                    if(m_ActionFinish == true)
                    {
                        m_ClickEnter = true;
                        StartCoroutine(FacingOfTheObject());
                        m_PlayerAnimator.SetTrigger("Push");
                        KeepCard.m_NearByObject = false;
                        StartCoroutine(WaitForSound());
                    }
                }
            }
            m_PlayerAnimator.SetBool("Idle", true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //The message is off when the player leave the trigger
        if (other.gameObject.name == "RigidBodyFPSController")
        {
            KeepCard.m_NearByObject = false;
            m_EnterMessage = false;
            m_ClickButton = false;
        }
    }

    //Animation for open the door
    public IEnumerator Rotate(float startRotation, float duration, float addToEnd)
    {
        float endRotation = startRotation + addToEnd;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / duration);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation,
            transform.eulerAngles.z);

            yield return null;
        }
    }
    //Wait a while until the player's action is done before opening the door
    public IEnumerator TimeBeforeOpen(float startRotation, float duration, float addToEnd)
    {
        m_PlayerAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        StartCoroutine(Rotate(startRotation, duration, addToEnd));
        m_Audio.PlayOneShot(m_UnLock);
    }

    public IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(0.5f);
        m_Audio.PlayOneShot(m_Lock);
    }

    public IEnumerator FacingOfTheObject()
    {
        m_ActionFinish = false;
        MixamoPlayer.m_StopRun = true;
        m_Camera.transform.localRotation = Quaternion.Euler(40f, 0f, 0f);
        m_FPS.GetComponent<FirstPersonController>().enabled = false;
        m_Player.transform.rotation = new Quaternion(m_Teleporte.rotation.x, m_Teleporte.rotation.y, m_Teleporte.rotation.z, m_Teleporte.rotation.w);
        m_Player.transform.position = m_Teleporte.position;
        yield return new WaitForSeconds(1.5f);
        m_FPS.GetComponent<FirstPersonController>().enabled = true;
        MixamoPlayer.m_StopRun = false;
        m_ClickEnter = false;
        KeepCard.m_IsCatch = false;
        m_ActionFinish = true;
    }

    public IEnumerator TheEnd()
    {
        //when the player open the last door before returning to the menu
        yield return new WaitForSeconds(1f);
        m_Audio.PlayOneShot(m_UnLock);
        yield return new WaitForSeconds(1f);
        TestLoad.LoadEndScene();
        Playerdeath.Rebootvariable();
    }


    public IEnumerator TimeBeforeOpen()
    {
        m_PlayerAnimator.SetTrigger("Push");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Rotate(1.7f));
        StartCoroutine(Position(1.5f));
        m_Audio.PlayOneShot(m_UnLock);
    }

    public IEnumerator Rotate(float duration)
    {
        float startRotation = transform.rotation.x;
        float endRotation = startRotation - 80f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / duration); // 90f
            transform.eulerAngles = new Vector3(yRotation, transform.eulerAngles.y,
            transform.eulerAngles.z);

            yield return null;
        }
    }

    public IEnumerator Position(float duration)
    {
        float startRotation = transform.position.z;
        float endRotation = startRotation - 1.1f;
        float t = 0.0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float zposition = Mathf.Lerp(startRotation, endRotation, t / duration); // 90f
            transform.position = new Vector3(transform.position.x, transform.position.y,
            zposition);

            yield return null;
        }
    }
}
