using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInformation : MonoBehaviour
{
    public GameObject m_Infos;
    public TextMeshProUGUI m_Text;
    public GameObject m_DeadName;
    public bool m_FirstTimeInfos = true;

    private void Start()
    {
        m_Infos.SetActive(false);
    }
    public void OnTriggerStay(Collider other)
    {
        //Display a information text when the player is in a strategic location
        if (other.name == "RigidBodyFPSController")
        {
            if(m_DeadName.name == "FirstDeadZombie")
            {
                m_Infos.SetActive(true);
                m_Text.text = "Il y a du sang... Il a été tué et traîner jusqu'ici";
            }
            if (m_DeadName.name == "derrick" && KeepCard.m_IsCatch == false)
            {
                m_Infos.SetActive(true);
                m_Text.text = "Le gardien est mort... Sa carte pourrait peut-être servir";
            }
            if (m_DeadName.name == "SecondeDeadZombie")
            {
                m_Infos.SetActive(true);
                m_Text.text = "Qui est-ce qui a bien pu leur arriver...";
            }
            if (m_DeadName.name == "ZombieWithoutHead")
            {
                if(m_FirstTimeInfos == true)
                {
                    m_Infos.SetActive(true);
                    m_Text.text = "On dirait que quelque chose à tué tous les résidents...";
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "RigidBodyFPSController")
        {
            m_Infos.SetActive(false);
            m_FirstTimeInfos = false;
        }
    }
}
