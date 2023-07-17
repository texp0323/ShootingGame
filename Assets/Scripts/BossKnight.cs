using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnight : MonoBehaviour
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
            selectPattern = Random.Range(1, 7);
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
            case 6:
                StartCoroutine(Pattern6());
                break;
        }
    }
    private void Pattern1()
    {
        StartCoroutine(nextPattern(10));
        muzzle[0].gameObject.SetActive(true);
        anim.Play("Pattern1", -1, 0f);
    }
    private void Pattern2()
    {
        StartCoroutine(nextPattern(5));
        muzzle[1].gameObject.SetActive(true);
        muzzle[2].gameObject.SetActive(true);
        anim.Play("Pattern2", -1, 0f);
    }
    private void Pattern3()
    {
        StartCoroutine(nextPattern(5));
        muzzle[3].gameObject.SetActive(true);
        anim.Play("Pattern3", -1, 0f);
    }
    IEnumerator Pattern4()
    {
        StartCoroutine(nextPattern(10));
        anim.Play("Pattern4", -1, 0f);
        yield return new WaitForSeconds(0.66f);
        muzzle[3].gameObject.SetActive(true);
    }
    IEnumerator Pattern5()
    {
        StartCoroutine(nextPattern(3));
        anim.Play("Pattern5", -1, 0f);
        enemy.hitColorAble = false;
        yield return new WaitForSeconds(0.5f);
        spr.color = Color.red;
        yield return new WaitForSeconds(1.4f);
        spr.color = Color.white;
        enemy.hitColorAble = true;
    }
    IEnumerator Pattern6()
    {
        StartCoroutine(nextPattern(10));
        waveManager.SetEnemyCount(13);
        muzzle[4].gameObject.SetActive(true);
        anim.Play("Pattern6", -1, 0f);
        GameObject summonedWave = Instantiate(summonWave);
        yield return new WaitForSeconds(10f);
        Destroy(summonedWave);
        waveManager.SetEnemyCount(1);
    }
}
