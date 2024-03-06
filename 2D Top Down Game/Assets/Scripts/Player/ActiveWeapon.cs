using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    private PlayerControls playerControls;
    private float timeBetweenAttacks;
    //set below to private later
    public int swordComboNum = 1;
    private bool isSwordCombo = false;

    private bool attackButtonDown, attackAltButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        playerControls.Combat.AltAttack.started += _ => StartAltAttacking();
        playerControls.Combat.AltAttack.canceled += _ => StopAltAttacking();

        AttackCooldown();
    }

    private void Update()
    {
        Attack();
        AltAttack();
    }

    public void NewWeapon (MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        //Should I protect this var?
        PlayerController.Instance.WeaponEquipped = true;
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
        //Should I protect this var?
        PlayerController.Instance.WeaponEquipped = false;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
        StartCoroutine(ResetComboRoutine());
    }

    private IEnumerator ResetComboRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        swordComboNum = 1;
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void StartAltAttacking()
    {
        attackAltButtonDown = true;
    }

    private void StopAltAttacking()
    {
        attackAltButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            if (ActiveInventory.Instance.activeSlotIndexNum == 1)
            {
                if (swordComboNum == 1)
                {
                    swordComboNum++;
                    isSwordCombo = true;
                    Debug.Log("Swinging sword 1");
                    (CurrentActiveWeapon as IWeapon).Attack();
                }
                else if (swordComboNum == 2)
                {
                    swordComboNum++;
                    isSwordCombo = true;
                    Debug.Log("Swinging sword 2");
                    (CurrentActiveWeapon as IWeapon).Attack();
                }
                else if (swordComboNum == 3)
                {
                    swordComboNum++;
                    isSwordCombo = true;
                    Debug.Log("Swinging sword 3");
                    (CurrentActiveWeapon as IWeapon).AltAttack();
                }
            }
            else
            {
                (CurrentActiveWeapon as IWeapon).Attack();
            }

            AttackCooldown();

        }
    }

    private void AltAttack()
    {
        if (attackAltButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            AttackCooldown();

            (CurrentActiveWeapon as IWeapon).AltAttack();

        }
    }
}
