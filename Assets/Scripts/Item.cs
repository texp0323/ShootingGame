using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItmeType
    {
        weapon, use
    }

    public ItmeType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
