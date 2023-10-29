using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShield : Movement
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Move(); 
    }

    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, _speed);
    }

}
