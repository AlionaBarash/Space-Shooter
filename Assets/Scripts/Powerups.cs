using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Powerups : Movement
{
    [SerializeField]
    private int _powerupID;

    void Update()
    {
        base.DestroyAfterPassingLimits();
    }

    void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + Vector2.down * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            switch (_powerupID)
            {
                case 1: player.ActivateShield(); break;

                case 2: player.ActivateSpeedBoost(); break;

                case 3: player.ActivateTripleShot(); break;
            }

            Destroy(this.gameObject);
        }

        Laser laser = other.GetComponent<Laser>();
        {
            if (laser != null) 
            {
                Destroy(this.gameObject);

                Destroy (laser.gameObject);
            }
        }
    }
}
