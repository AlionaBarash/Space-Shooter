using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ITimerPowerUp : IPowerUp
{
    float DurationTime {  get; }

    IEnumerator DeactivationRoutine();
}
