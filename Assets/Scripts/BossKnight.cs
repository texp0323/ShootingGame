using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnight : MonoBehaviour
{
    private Animator anim;

    [Header("Muzzle")]
    [SerializeField] Transform[] muzzle;

    private int selectPattern;
    private int lastPattern;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
            selectPattern = Random.Range(0, 6);
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
                Pattern5();
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
    private void Pattern5()
    {
        StartCoroutine(nextPattern(3));
        anim.Play("Pattern5", -1, 0f);
    }
}
