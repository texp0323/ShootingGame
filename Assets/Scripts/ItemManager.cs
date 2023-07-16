using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject[] items;

    public void SummonItem(Vector2 pos, int type, float dropPer)
    {
        if(Random.Range(1,1001) <= dropPer * 10)
        {
            if (type == 1)
            {
                Instantiate(items[Random.Range(0, 3)], pos, Quaternion.Euler(0,0,45));
            }
            if (type == 2)
            {
                Instantiate(items[Random.Range(3, 6)], pos, Quaternion.identity);
            }
        }
    }
}
