using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticles : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Reset", 0.5f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Reset()
    {
        gameObject.SetActive(false);
    }
}
