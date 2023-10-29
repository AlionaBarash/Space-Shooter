using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField]
    protected float _speed;

    protected Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    abstract protected void Move();

    protected virtual void DestroyAfterPassingLimits()
    {
        if (transform.position.y <= -6.6f)
        {
            Destroy(this.gameObject);
        }
    }
}
