using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PowerUps, ITimerPowerUp
{
    [SerializeField]
    private float _durationTime;
    public float DurationTime { get { return _durationTime; } }

    public bool IsBoostActive { get; set; }

    public void ActivateBoost()
    {
        IsBoostActive = true;

        StartCoroutine(DeactivationRoutine());
    }

    public IEnumerator DeactivationRoutine()
    {
        yield return new WaitForSeconds(DurationTime);

        IsBoostActive = false;

        Destroy(gameObject);
    }

    public void AfterCollectingBoost()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
    }
}
