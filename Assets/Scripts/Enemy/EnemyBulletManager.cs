using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    public List<GameObject>[] enemyShootedBullets = new List<GameObject>[2];

    private void Awake()
    {
        enemyShootedBullets[0] = new List<GameObject>();
        enemyShootedBullets[1] = new List<GameObject>();
    }
}
