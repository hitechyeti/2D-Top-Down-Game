using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : Singleton<Mana>
{

    public int CurrentMana { get; private set; }

    [SerializeField] private int timeBetweenManaRefresh = 3;

    private int startingMana = 3;
    private int maxMana;
    private Slider manaSlider;

    const string MANA_SLIDER_TEXT = "Mana Slider";

    protected override void Awake()
    {
        base.Awake();

        maxMana = startingMana;
        CurrentMana = startingMana;
    }

    public void UseMana()
    {
        CurrentMana--;
        UpdateManaSlider();
        StopAllCoroutines();
        StartCoroutine(RefreshManaRoutine());
    }

    public void RefreshMana()
    {
        if (CurrentMana < maxMana && !PlayerHealth.Instance.IsDead)
        {
            CurrentMana++;
        }
        UpdateManaSlider();
    }

    public void ReplenishManaOnDeath()
    {
        CurrentMana = startingMana;
        UpdateManaSlider();
    }

    private IEnumerator RefreshManaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenManaRefresh);
            RefreshMana();
        }
    }

    private void UpdateManaSlider()
    {
        if (manaSlider == null)
        {
            manaSlider = GameObject.Find(MANA_SLIDER_TEXT).GetComponent<Slider>();
        }

        manaSlider.maxValue = maxMana;
        manaSlider.value = CurrentMana;
    }

}
