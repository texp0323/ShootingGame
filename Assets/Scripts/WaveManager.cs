using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] waves;
    private int waveCount = 0;
    private int summonedEnemysCount;

    private void Start()
    {
        StartCoroutine(nextWave());
    }

    public void KillEnemy()
    {
        summonedEnemysCount--;
        if (summonedEnemysCount <= 0)
            StartCoroutine(nextWave());
    }

    public void SetEnemyCount(int a)
    {
        summonedEnemysCount = a;
    }

    IEnumerator nextWave()
    {
        yield return new WaitForSeconds(3);
        summonedEnemysCount = waves[waveCount].transform.childCount;
        Instantiate(waves[waveCount]);
        waveCount++;
    }
}
