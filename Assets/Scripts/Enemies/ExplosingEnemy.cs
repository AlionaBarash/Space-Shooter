using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    void Start()
    {
        if (_isSelfDestroyed)
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        _isSelfDestroyed = false;

        yield return new WaitForSeconds(Random.Range(2f, 2.2f));

        _isSelfDestroyed = true;

        base.Damage();
    }
}
