using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPowerUp 
{
    bool IsBoostActive { get; set;}

    void ActivateBoost();

    void AfterCollectingBoost();
}
