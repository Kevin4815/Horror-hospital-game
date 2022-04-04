using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Playerdeath : MonoBehaviour
{
    public Transform m_PlayerTransform;
    public Collider m_PlayerCollider;
    public FirstPersonController m_ScriptController;
    public GameObject m_CanvasGameOver;
    public static bool m_Death;

    void Start()
    {
        m_ScriptController = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        //If the player is dead
        if(ArmCollider.m_IsDead == true)
        {
            StartCoroutine(Death(0.5f));
            m_PlayerCollider.enabled = false;
            m_Death = true;
            m_ScriptController.enabled = false;
            StartCoroutine(GameOver());
        }
    }

    //When the player dies, he lies on his back
    IEnumerator Death(float duration)
    {
        float startRotation = m_PlayerTransform.rotation.x;
        float endRotation = startRotation - 80f;
        float t = 0.0f; // elapsedTime

        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration);
            transform.eulerAngles = new Vector3(yRotation, transform.eulerAngles.y,
            transform.eulerAngles.z);

            yield return null;
        }
    }

    //A new canvas with "GAME OVER" appears when the player is dead
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.5f);
        m_CanvasGameOver.SetActive(true);
        Rebootvariable();
    }

    //All variables are cleaned
    public static void Rebootvariable()
    {
        m_Death = false;
        EnemyOne.m_Walk = false;
        KeepCard.m_IsKey = false;
        KeepCard.m_IsCard = false;
        ArmCollider.m_IsDead = false;
        KeepCard.m_IsCatch = false;
        KeepCard.m_NearByObject = false;
        OpenDoorLvRoom.m_NearByDoor = false;
        OpenDoorLvRoom.m_Open = false;
    }
}
