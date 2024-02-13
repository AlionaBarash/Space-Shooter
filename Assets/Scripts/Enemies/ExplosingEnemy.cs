using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    void Start()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        if (_isSelfDestroyed)
        {
            yield return new WaitForSeconds(Random.Range(1f, 1.3f));

            base.Damage();
        }
    }
}
