using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItmeType
    {
        weapon, use
    }

    private Rigidbody2D rigid;

    public ItmeType type;
    [SerializeField] private int weaponNum;
    [SerializeField] private int useItemCode;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.down * 15,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (type == ItmeType.weapon)
            {
                collision.GetComponent<PlayerInfo>().TakeWeapon(weaponNum);
            }
            if (type == ItmeType.use)
            {
                switch (useItemCode)
                {
                    case 0:
                        collision.GetComponent<PlayerInfo>().Heal(0.2f);
                        break;
                    case 1:
                        collision.GetComponent<PlayerInfo>().Heal(1);
                        break;
                    case 2:
                        collision.GetComponent<PlayerInfo>().UseInvincibility(5);
                        break;
                    default:
                        break;
                }
            }
        }
        Destroy(gameObject);
    }
}
