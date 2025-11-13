using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingLiquid : MonoBehaviour
{
    public GameObject liquid;
    public GameObject cap;

    GameObject instant;

    public void Flow()
    {
        if (liquid != null)
           instant = Instantiate(liquid, cap.transform.position, cap.transform.rotation);
    }

    public void StopFlow()
    {
        Destroy(instant);
    }
}
