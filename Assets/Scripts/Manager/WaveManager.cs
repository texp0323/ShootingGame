using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] waves;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int waveCount = 0;
    private int summonedEnemysCount;
    private GameObject summonedWave;

    private void Start()
    {
        StartCoroutine("NextWave");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F7))
        {
            if (waveCount < 6)
            {
                waveCount = 6;
            }
            else
            {
                waveCount = 0;
                gameManager.BeforeStage();
            }
            StopCoroutine("NextWave");
            StartCoroutine("NextWave");
        }
    }

    public void KillEnemy()
    {
        summonedEnemysCount--;
        if (summonedEnemysCount <= 0)
            StartCoroutine("NextWave");
    }

    public void SetEnemyCount(int a)
    {
        summonedEnemysCount = a;
    }

    IEnumerator NextWave()
    {
        if (summonedWave != null)
            Destroy(summonedWave);
        if (waveCount == 5) { gameManager.ChangeBgm(0); }
        if (waveCount == 11) { gameManager.ChangeBgm(2); }
        if (waveCount == 6)
        {
            gameManager.ChangeBgm(1);
            gameManager.NextStage();
            yield return new WaitForSeconds(7);
        }
        if(waveCount == 12)
        {
            gameManager.NextStage();
            StopCoroutine("NextWave");
        }
        yield return new WaitForSeconds(3);
        summonedEnemysCount = waves[waveCount].transform.childCount;
        summonedWave = Instantiate(waves[waveCount]);
        waveCount++;
    }
}
