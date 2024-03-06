using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInventory : Singleton<ActiveInventory>
{
    public int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    [SerializeField] private Sprite woodenSwordImage, stoneSwordImage;
    [SerializeField] private Sprite shortBowImage, longBowImage;
    [SerializeField] private Sprite electricStaffImage, fireStaffImage;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach(Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if (PlayerHealth.Instance.IsDead) { return; }

        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        

        if(weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = weaponInfo.weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

    //NEW INVENTORY SECTION
    public void SetWoodenSword()
    {
        Transform childTransform = transform.GetChild(1);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        Image image = inventorySlot.GetComponentInChildren<Image>();

        image.sprite = woodenSwordImage;
        inventorySlot.SetWoodenSword();
    }

    public void SetStoneSword()
    {
        Transform childTransform = transform.GetChild(1);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        Image image = inventorySlot.GetComponentInChildren<Image>();

        image.sprite = stoneSwordImage;
        inventorySlot.SetStoneSword();
    }

    public void SetShortBow()
    {
        Transform childTransform = transform.GetChild(2);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        Image image = inventorySlot.GetComponentInChildren<Image>();

        image.sprite = shortBowImage;
        inventorySlot.SetShortBow();
    }

    public void SetLongBow()
    {
        Transform childTransform = transform.GetChild(2);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        Image image = inventorySlot.GetComponentInChildren<Image>();

        image.sprite = longBowImage;
        inventorySlot.SetLongBow();
    }

    public void SetElectricStaff()
    {
        Transform childTransform = transform.GetChild(3);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        Image image = inventorySlot.GetComponentInChildren<Image>();

        image.sprite = electricStaffImage;
        inventorySlot.SetElectricStaff();
    }

    public void SetFireStaff()
    {
        Transform childTransform = transform.GetChild(3);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        Image image = inventorySlot.GetComponentInChildren<Image>();

        image.sprite = fireStaffImage;
        inventorySlot.SetFireStaff();
    }

    //END NEW INVENTORY SECTION

}
