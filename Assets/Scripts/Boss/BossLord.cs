using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLord : MonoBehaviour
{
    private WaveManager waveManager;
    private Enemy enemy;
    private Animator anim;
    private SpriteRenderer spr;

    [Header("Muzzle")]
    [SerializeField] Transform[] muzzle;
    [Header("SummonWave")]
    [SerializeField] GameObject summonWave;

    private int selectPattern;
    private int lastPattern;
    public GameObject summonedWave;

    private void Start()
    {
        waveManager = GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>();
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        StartCoroutine(nextPattern(0));
    }

    IEnumerator nextPattern(float patternDealy)
    {
        yield return new WaitForSeconds(patternDealy);
        anim.Play("Idle", -1, 0f);
        for (int i = 0; i < muzzle.Length; i++)
        {
            muzzle[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(2);

        do
        {
            selectPattern = Random.Range(4, 6);
        } while (selectPattern == lastPattern);

        lastPattern = selectPattern;

        switch (selectPattern)
        {
            case 1:
                Pattern1();
                break;
            case 2:
                Pattern2();
                break;
            case 3:
                Pattern3();
                break;
            case 4:
                StartCoroutine(Pattern4());
                break;
            case 5:
                StartCoroutine(Pattern5());
                break;
        }
    }
    private void Pattern1()
    {
        StartCoroutine(nextPattern(5));
        muzzle[0].gameObject.SetActive(true);
        muzzle[1].gameObject.SetActive(true);
        muzzle[2].gameObject.SetActive(true);
        muzzle[3].gameObject.SetActive(true);
        anim.Play("Pattern1", -1, 0f);
    }
    private void Pattern2()
    {
        StartCoroutine(nextPattern(9));
        muzzle[4].gameObject.SetActive(true);
        muzzle[5].gameObject.SetActive(true);
        anim.Play("Pattern2", -1, 0f);
    }
    private void Pattern3()
    {
        StartCoroutine(nextPattern(10));
        muzzle[6].gameObject.SetActive(true);
        anim.Play("Pattern3", -1, 0f);
    }
    IEnumerator Pattern4()
    {
        StartCoroutine(nextPattern(12));
        anim.Play("Pattern4", -1, 0f);
        yield return new WaitForSeconds(0.5f);
        muzzle[7].gameObject.SetActive(true);
        yield return new WaitForSeconds(9.5f);
        muzzle[7].gameObject.SetActive(false);
    }
    IEnumerator Pattern5()
    {
        StartCoroutine(nextPattern(10));
        anim.Play("Pattern5", -1, 0f);
        muzzle[8].gameObject.SetActive(true);
        muzzle[9].gameObject.SetActive(true);
        waveManager.SetEnemyCount(17);
        summonedWave = Instantiate(summonWave);
        yield return new WaitForSeconds(10f);
        Destroy(summonedWave);
        waveManager.SetEnemyCount(1);
    }
}
