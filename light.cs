using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public GameObject m_Light;
    public GameObject m_Lighttwo;
    float m_TimeLight;

    //Animation for flashing one of the lamps
    public IEnumerator Start()
    {
        while (true)
        {
           m_TimeLight = Random.Range(0.05f, 0.3f);

           StartCoroutine(LightOff(m_TimeLight));
           yield return new WaitForSeconds(m_TimeLight);
           StartCoroutine(LightOn(m_TimeLight));
           yield return new WaitForSeconds(m_TimeLight);
        }
    }

    public IEnumerator LightOff(float t)
    {
        yield return new WaitForSeconds(t);
        m_Light.SetActive(false);
        m_Lighttwo.SetActive(false);
    }
    public IEnumerator LightOn(float t)
    {
        yield return new WaitForSeconds(t);
        m_Light.SetActive(true);
        m_Lighttwo.SetActive(true);
    }
}
