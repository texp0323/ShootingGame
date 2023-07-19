using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyDestroyParticles;
    [SerializeField] GameObject[] projectileHitParticles;

    public void SummonEnemyDestroyParticle(Vector2 summonPos)
    {
        foreach (GameObject enemyDestroyParticle in enemyDestroyParticles)
        {
            if (!enemyDestroyParticle.activeSelf)
            {
                enemyDestroyParticle.transform.position = summonPos;
                enemyDestroyParticle.SetActive(true);
                enemyDestroyParticle.GetComponent<ParticleSystem>().Play();
                break;
            }
        }
    }

    public void SummonProjectileHitParticle(Vector2 summonPos)
    {
        foreach (GameObject projectileHitParticle in projectileHitParticles)
        {
            if (!projectileHitParticle.activeSelf)
            {
                projectileHitParticle.transform.position = summonPos;
                projectileHitParticle.SetActive(true);
                projectileHitParticle.GetComponent<ParticleSystem>().Play();
                break;
            }
        }
    }
}
