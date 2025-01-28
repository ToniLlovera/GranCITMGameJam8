using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance { get; set; }
    //UI
    public TextMeshProUGUI ammoDisplay;
    private void Awake()
    {

    }
}
