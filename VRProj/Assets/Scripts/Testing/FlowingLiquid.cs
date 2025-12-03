using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingLiquid : MonoBehaviour
{

    public void Flow()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
    }

    public void StopFlow()
    {
        gameObject.GetComponent<ParticleSystem>().Stop();
    }
}
