using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{

    public int CurrentStamina { get; private set; }

    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    private int startingStamina = 3;
    private int maxStamina;
    private Slider staminaSlider;

    const string STAMINA_SLIDER_TEXT = "Stamina Slider";

    protected override void Awake()
    {
        base.Awake();

        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }

    private void Start()
    {

    }

    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaSlider();
        StopAllCoroutines();
        StartCoroutine(RefreshStaminaRoutine());
    }

    public void RefreshStamina()
    {
        if (CurrentStamina < maxStamina && !PlayerHealth.Instance.IsDead)
        {
            CurrentStamina++;
        }
        UpdateStaminaSlider();
    }

    public void ReplenishStaminaOnDeath()
    {
        CurrentStamina = startingStamina;
        UpdateStaminaSlider();
    }

    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

    private void UpdateStaminaSlider()
    {
        if (staminaSlider == null)
        {
            staminaSlider = GameObject.Find(STAMINA_SLIDER_TEXT).GetComponent<Slider>();
        }

        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = CurrentStamina;
    }

}
