using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    public List<GameObject>[] EnemyShootedBullets = new List<GameObject>[2];

    private void Awake()
    {
        EnemyShootedBullets[0] = new List<GameObject>();
        EnemyShootedBullets[1] = new List<GameObject>();
    }
}
