using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SecondEnemy : MonoBehaviour
{
    public Animator m_SecondEnemy;
    public AudioClip m_Scream;
    public GameObject m_SecondMonster;
    public Collider m_ArmCollider;
    public Transform m_Target;
    public static NavMeshAgent m_Agent;
    AudioSource m_Audio;
    public Collider m_Collider;

    public static bool m_Walk;
    public bool m_Attack;
    public static float distance;

    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
        m_Agent = GetComponent<NavMeshAgent>();

        m_SecondMonster.GetComponent<AudioSource>().enabled = true;
        StartCoroutine(TestScream());
    }

    public void Update()
    {
        if (Playerdeath.m_Death == false)
        {
            m_Walk = true;
            StartCoroutine(Walk());
            distance = Vector3.Distance(m_SecondEnemy.transform.position, m_Target.transform.position);
        }
        else
        {
            //If the player is dead, the enemy stop his attack
            StopAllCoroutines();
            m_SecondEnemy.SetBool("Attack", false);
        }
    }

    public IEnumerator Walk()
    {
        //The enemy run in player direction for kill him
        if (m_Walk == true && m_Attack == false)
        {
            if (distance > 2)
            {
                m_SecondEnemy.SetBool("Attack", false);
                m_ArmCollider.enabled = false;
                yield return new WaitForSeconds(3f); ;
                m_Agent.destination = m_Target.position;
                m_SecondEnemy.SetBool("Run", true);
                m_Agent.speed = 5f;
            }

            //If the enemy is near the player, he stop himself and attack the playe
            if (distance <= 2f)
            {
                StartCoroutine(Attack());
                m_Attack = false;
                m_SecondEnemy.SetBool("Attack", true);
                m_SecondEnemy.SetBool("Run", false);
                StartCoroutine(DestinationStop());
            }
        }
    }

    public IEnumerator DestinationStop()
    {
        yield return new WaitForSeconds(1f);
        m_Agent.isStopped = false;
    }

    public IEnumerator Attack()
    {
        m_Attack = true;
        m_SecondEnemy.SetBool("Attack", true);
        if (m_Attack == true)
        {
            m_Agent.isStopped = true;
        }
        yield return new WaitForSeconds(0.5f);
        m_ArmCollider.enabled = true;
        yield return new WaitForSeconds(1.2f);
        m_ArmCollider.enabled = false;
    }

    public IEnumerator TestScream()
    {
        yield return new WaitForSeconds(5f);
        m_Audio.spatialBlend = 1f;
        m_Audio.maxDistance = 50f;
        m_Audio.clip = m_Scream;
        m_Audio.Play();
        m_Audio.loop = true;
    }
}

