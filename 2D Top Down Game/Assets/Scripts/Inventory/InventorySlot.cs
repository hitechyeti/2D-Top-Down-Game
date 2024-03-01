using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;

    [SerializeField] private WeaponInfo woodenSword;
    [SerializeField] private WeaponInfo stoneSword;

    [SerializeField] private WeaponInfo shortBow;
    [SerializeField] private WeaponInfo longBow;

    [SerializeField] private WeaponInfo electricStaff;
    [SerializeField] private WeaponInfo fireStaff;

    public void SetWoodenSword()
    {
        weaponInfo = woodenSword;
    }

    public void SetStoneSword()
    {
        weaponInfo = stoneSword;
    }

    public void SetShortBow()
    {
        weaponInfo = shortBow;
    }

    public void SetLongBow()
    {
        weaponInfo = longBow;
    }

    public void SetElectricStaff()
    {
        weaponInfo = electricStaff;
    }

    public void SetFireStaff()
    {
        weaponInfo = fireStaff;
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }



}
