using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob : MonoBehaviour
{
    public RobotHit robotHit;

    public int robotDamage;

    private void Start()
    {
        robotHit.damage = robotDamage;
    }
}
