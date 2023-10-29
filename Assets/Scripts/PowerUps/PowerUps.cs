using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUps : Movement
{
    //if player has been destroyed - disable spawning powerups in SpawnManager!
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
        Laser laser = other.GetComponent<Laser>();
        if (laser != null)
        {
            Destroy(laser.gameObject);
            //анимация поглощения бустерами лазеров
        }
    }
}
