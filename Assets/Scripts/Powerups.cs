using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    [SerializeField]
    private int _powerupID;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.y <= -6.6f)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
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
