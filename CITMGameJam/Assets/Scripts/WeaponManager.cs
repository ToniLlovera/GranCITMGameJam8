using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GunSystem;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; set; }

    public List<GameObject> weaponSlots;

    public GameObject activeWeaponSlot;

    private Quaternion originalWeaponRotation;

    [Header("Ammo")]
    public int totalRifleAmmo = 0;
    public int totalPistolAmmo = 0;
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

    private void Start()
    {
        activeWeaponSlot = weaponSlots[0];
    }

    private void Update()
    {
        foreach (GameObject weaponSlot in weaponSlots)
        {
            if (weaponSlot == activeWeaponSlot)
            {
                weaponSlot.SetActive(true);
            }
            else
            {
                weaponSlot.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchActiveSlot(2);
        }
    }

    public void PickupWeapon(GameObject pickedupWeapon)
    {
        AddWeaponIntoActiveSlot(pickedupWeapon);
    }

    private void AddWeaponIntoActiveSlot(GameObject pickedUpWeapon)
    {
        originalWeaponRotation = pickedUpWeapon.transform.localRotation;

        DropCurrentWeapon(pickedUpWeapon);

        pickedUpWeapon.transform.SetParent(activeWeaponSlot.transform, false);

        GunSystem weapon = pickedUpWeapon.GetComponent<GunSystem>();

        pickedUpWeapon.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z);
        pickedUpWeapon.transform.localRotation = Quaternion.Euler(weapon.spawnRotation.x, weapon.spawnRotation.y, weapon.spawnRotation.z);

        weapon.isActiveWeapon = true;
        weapon.animator.enabled = true;
    }

    internal void PickupAmmo(AmmoBox ammo)
    {
        switch (ammo.ammoType)
        {
            case AmmoBox.AmmoType.PistolAmmo:
                totalPistolAmmo += ammo.ammoAmount;
                break;
            case AmmoBox.AmmoType.RiffleAmmo:
                totalRifleAmmo +=ammo.ammoAmount;
                break;
        }
    }

    private void DropCurrentWeapon(GameObject pickedupWeapon)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;

            weaponToDrop.GetComponent<GunSystem>().isActiveWeapon = false;
            weaponToDrop.GetComponent<GunSystem>().animator.enabled = false;

            weaponToDrop.transform.SetParent(pickedupWeapon.transform.parent);
            weaponToDrop.transform.localPosition = pickedupWeapon.transform.localPosition;

            weaponToDrop.transform.localRotation = originalWeaponRotation; 
        }
    }

    public void SwitchActiveSlot(int slotNumber)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            GunSystem currentWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<GunSystem>();
            currentWeapon.isActiveWeapon = false;
        }

        activeWeaponSlot = weaponSlots[slotNumber];

        if (activeWeaponSlot.transform.childCount > 0)
        {
            GunSystem newWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<GunSystem>();
            newWeapon.isActiveWeapon = true;
        }
    }

    internal void DecreaseTotalAmmo(int bulletsToDecrease, GunSystem.WeaponModel thisWeaponModel)
    {
        switch (thisWeaponModel)
        {
            case GunSystem.WeaponModel.Pistol:
                totalPistolAmmo -= bulletsToDecrease;
                break;
            case GunSystem.WeaponModel.GunHeavy:
                totalRifleAmmo -= bulletsToDecrease;
                break;
                       
        }
    }
    public int CheckAmmoLeftFor(GunSystem.WeaponModel thisWeaponModel)
    {
        switch (thisWeaponModel)
        {
            case GunSystem.WeaponModel.Pistol:
                return totalPistolAmmo;

            case GunSystem.WeaponModel.GunHeavy:
                return totalRifleAmmo;

            default:
                return 0;
        }
    }
}
