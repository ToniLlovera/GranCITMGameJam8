using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 60;
    public AmmoType ammoType;

    public enum AmmoType
    {
        RiffleAmmo,
        PistolAmmo
    }
}
