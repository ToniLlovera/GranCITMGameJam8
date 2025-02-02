using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GunSystem;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;

    public AudioClip PistolShot;
    public AudioClip HeavyGunShot;

    public AudioSource reloadingSoundPistol;
    public AudioSource reloadingSoundHeavy;

    public AudioSource emptyMagazineSound;


    public AudioClip robotWalking;
    public AudioClip robotChase;
    public AudioClip robotAttack;
    public AudioClip robotHurt; //pinyau?
    public AudioClip robotDeath;

    public AudioSource robotChannel;
    public AudioSource playerChannel;
    public AudioClip playerHurt;
    public AudioClip playerDie;

    public AudioClip gameOverMusic;
    private void Awake()
    {
        if (Instance != null && Instance != this)
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
                ShootingChannel.PlayOneShot(PistolShot);
                break;
            case WeaponModel.GunHeavy:
                ShootingChannel.PlayOneShot(HeavyGunShot);
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
                reloadingSoundHeavy.Play();
                break;
        }
    }
}
