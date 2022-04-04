using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyOne : MonoBehaviour
{
    public Animator m_EnemyOne;

    public AudioClip m_Scream;
    public AudioClip m_ZombieRun;
    public GameObject m_ZombieBreath;
    public GameObject m_Run;
    public Transform m_Zombie;
    public Transform m_FPS;
    public Transform m_Target;
    public Collider m_ColliderTrigger;
    public static NavMeshAgent m_Agent;
    AudioSource m_Audio;

    public static bool m_Walk;
    public bool m_Attack;
    public bool m_FirstAttack;

    public static float distance;

    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
        m_EnemyOne.SetBool("Idle", true);
        m_Agent = GetComponent<NavMeshAgent>();
        m_FirstAttack = true;
    }


    public void OnTriggerEnter(Collider other)
    {
        // The enemy attack the player if he is near of him
        if (other.gameObject.name == "RigidBodyFPSController" && Playerdeath.m_Death == false)
        {
            m_ZombieBreath.SetActive(false);
            m_EnemyOne.SetTrigger("Turn");
            StartCoroutine(ZombieTurn(0.5f));

            m_EnemyOne.SetTrigger("Scream");
            m_ZombieBreath.GetComponent<AudioSource>().enabled = false;
            m_Walk = true;
            StartCoroutine(PlayZombieSound());
            m_FirstAttack = false;
        }
    }

    public void ZombieAttack()
    {
        //The enemy attack the player if the player catch the key
        if (KeepCard.m_IsKey == true)
        {
            m_ZombieBreath.SetActive(false);
            m_EnemyOne.SetTrigger("Turn");
            StartCoroutine(ZombieTurn(0.5f));

            m_EnemyOne.SetTrigger("Scream");
            m_ZombieBreath.GetComponent<AudioSource>().enabled = false;
            m_Walk = true;
            StartCoroutine(PlayZombieSound());
            m_FirstAttack = false;
        }
    }
    //Animation of the zombie so that it turns
    public IEnumerator ZombieTurn(float duration)
    {
        float startRotation = m_Zombie.rotation.y;
        float endRotation = startRotation - 150f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / duration);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation,
            transform.eulerAngles.z);

            yield return null;
        }
        m_Audio.PlayOneShot(m_Scream);
    }


    public void Update()
    {
        //Check if the player is dead or not
        if(Playerdeath.m_Death == false)
        {
            if(m_FirstAttack == true)
            ZombieAttack();

            StartCoroutine(Walk());
            distance = Vector3.Distance(m_EnemyOne.transform.position, m_Target.transform.position);
        }
        else
        {
            //If the player is dead, the enemy stop his attack
            StopAllCoroutines();
            m_ZombieBreath.SetActive(false);
            m_EnemyOne.SetBool("Attack", false);
        }
     }

    public IEnumerator Walk()
    {
        //The enemy run in player direction for kill him
        if (m_Walk == true && m_Attack == false)
        {
            if(distance > 2)
            {
                m_ColliderTrigger.enabled = false;
                m_EnemyOne.SetBool("Attack", false);
                yield return new WaitForSeconds(3f);;
                m_Agent.destination = m_Target.position;
                m_ZombieBreath.SetActive(true);
                m_EnemyOne.SetBool("Running", true);
                m_Agent.speed = 5f;
            }
            //If the enemy is near the player, he stop himself and attack the player
            if (distance <= 2f)
            {
                StartCoroutine(Attack());
                m_Attack = false;
                m_EnemyOne.SetBool("Running", false);
                StartCoroutine(DestinationStop());
            }
        }
    }

    public IEnumerator DestinationStop()
    {
        yield return new WaitForSeconds(1.7f);
        m_Agent.isStopped = false;
    }

    public IEnumerator Attack()
    {
        m_Attack = true;
        m_EnemyOne.SetBool("Attack", true);
        if(m_Attack == true)
        {
            m_Agent.isStopped = true;
        }
        yield return new WaitForSeconds(1.7f);
    }

    public IEnumerator PlayZombieSound()
    {
        yield return new WaitForSeconds(4f);
        m_Run.GetComponent<AudioSource>().enabled = true;
    }
}
