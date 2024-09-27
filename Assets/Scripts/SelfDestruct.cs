using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeUntillDestruction = 2f;
    void Start()  
    {
        Destroy(gameObject, timeUntillDestruction);
    }
}
