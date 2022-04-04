using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorKeyWithZombie : MonoBehaviour
{
    public Transform m_FirstDoorMiddleOpen;

    public void Update()
    {
        OpenDoor();
    }

    //When the player has tried to open the first door, the door with the zombie behind opens
    public void OpenDoor()
    {
        if (OpenDoorLvRoom.m_ClickEnter == true)
        {
            m_FirstDoorMiddleOpen.transform.eulerAngles = new Vector3(0, -250, 0);
        }
    }
}


