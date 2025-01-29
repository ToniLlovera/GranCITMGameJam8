using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GunSystem;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource shootingSoundPistol;
    public AudioSource emptyMagazineSound;
    public AudioSource reloadingSoundPistol;

    public AudioSource shootingSoundHeavy;
    public AudioSource reloadingSoundHeavy;

    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                shootingSoundPistol.Play();
                break;
            case WeaponModel.GunHeavy:
                //play GunHeavy Sound
                break;
        }
    }
    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                reloadingSoundPistol.Play();
                break;
            case WeaponModel.GunHeavy:
                //play GunHeavy Sound
                break;
        }
    }


}
