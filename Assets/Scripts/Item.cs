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
    [SerializeField] private int weaponNum;
    [SerializeField] private float lifeTime;

    private void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(type == ItmeType.weapon)
        {
            collision.GetComponent<PlayerInfo>().takeWeapon(weaponNum);
        }
        Destroy(gameObject);
    }
}
