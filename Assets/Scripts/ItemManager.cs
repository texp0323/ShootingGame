using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject[] items;

    public void SummonItem(Vector2 pos, int type, int dropPer)
    {
        if(Random.Range(1,101) <= dropPer)
        {
            if (type == 1)
            {
                Instantiate(items[Random.Range(0, 3)], pos, Quaternion.Euler(0,0,45));
            }
            if (type == 2)
            {
                Instantiate(items[Random.Range(3, 6)], pos, Quaternion.identity);
            }
            if (type == 3)
            {
                Instantiate(items[Random.Range(0, 3)], pos - Vector2.right * 10, Quaternion.Euler(0, 0, 45));
                Instantiate(items[Random.Range(0, 3)], pos, Quaternion.Euler(0, 0, 45));
                Instantiate(items[4], pos + Vector2.right * 10, Quaternion.identity);
            }
        }
    }
}
