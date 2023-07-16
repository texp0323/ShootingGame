using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Update()
    {
        transform.position = target.position;
    }
}
