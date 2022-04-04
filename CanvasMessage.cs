using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasMessage : MonoBehaviour
{
    public TextMeshProUGUI m_Text;
    public GameObject m_TextActions;
    public static Image m_ProgressBar;

    void Update()
    {
        //Display a text when the player is near an object to do an action
        m_TextActions.gameObject.SetActive(KeepCard.m_NearByObject);

        if (KeepCard.m_NearByObject == true && KeepCard.m_IsCatch == false)
        {
            m_Text.text = "Entrer : pour ramasser";
        }

        if(KeepCard.m_NearByObject == true && KeepCard.m_IsFirstAid == true)
        {
            m_Text.text = "Entrer : pour ramasser";

            if (KeepCard.m_HealthInfo == true && KeepCard.m_ClickEnter == true)
            {
                m_Text.text = "Santé déjà à 100%";
            }
        }

        if (OpenDoorLvRoom.m_NearByDoor == true && OpenDoorLvRoom.m_ClickEnter == false && OpenDoorLvRoom.m_EnterMessage == true)
        {
            m_Text.text = "Entrer : pour ouvrir";
        }
        else if (OpenDoorLvRoom.m_NearByDoor == true && OpenDoorLvRoom.m_ClickEnter == true && KeepCard.m_IsCatch == false)
        {
            if(KeepCard.m_IsKey == false)
            {
                m_Text.text = "Clé manquante";
            }
            else if(KeepCard.m_IsCard == false)
            {
                m_Text.text = "Carte manquante";
            }
        }

        if(OpenDoorLvRoom.m_NearByDoor == true && OpenDoorLvRoom.m_ClickEnter == false && OpenDoorLvRoom.m_ClickButton == true)
        {
            m_Text.text = "Entrer : pour appuyer";
        }
    }
}
