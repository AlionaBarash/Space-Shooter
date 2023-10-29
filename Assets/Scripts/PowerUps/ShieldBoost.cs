using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoost : PowerUps, IDestroyablePowerUp
{
    [SerializeField]
    private GameObject _playerShieldPrefab;

    private GameObject _playerShieldClone; 

    public bool IsBoostActive { get; set; } 

    void Start()
    {
        Player.onBoostDeactivation += DeactivateBoost;
    }

    public void ActivateBoost()
    {
        IsBoostActive = true;

        DisplayPlayerShield();
    }

    private void DisplayPlayerShield()
    {
        _playerShieldClone = Instantiate(_playerShieldPrefab);
    }

    public void DeactivateBoost()
    {
        IsBoostActive = false;

        Destroy(_playerShieldClone);
        Destroy(gameObject);
    }

    public void AfterCollectingBoost()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    void OnDestroy()
    {
        Player.onBoostDeactivation -= DeactivateBoost;
    }
}
