using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDestroyablePowerUp : IPowerUp
{
    void DeactivateBoost();
}
