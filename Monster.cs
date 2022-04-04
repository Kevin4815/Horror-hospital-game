using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator m_Monster;

    void Start()
    {
        m_Monster.SetBool("Idle", true);
    }

    //The monster attack the player if the player is too close to him
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "RigidBodyFPSController")
        {
            m_Monster.SetTrigger("Attack");
        }
    }
}
