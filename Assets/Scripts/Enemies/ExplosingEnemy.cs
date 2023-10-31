using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    void Start()
    {
        _isSelfDestroyedEnemy = true;

        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.3f));

        int explodingEnemyID = Random.Range(1, 4);

        if (explodingEnemyID == 2) 
        {
            base.Damage();
        }
    }
}
