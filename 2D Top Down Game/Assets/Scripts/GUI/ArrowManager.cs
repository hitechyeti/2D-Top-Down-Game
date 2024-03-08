using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowManager : Singleton<ArrowManager>
{
    private TMP_Text arrowText;
    public int CurrentArrows { get; private set; }

    const string ARROW_AMOUNT_TEXT = "Arrows Amount Text";

    protected override void Awake()
    {
        base.Awake();

        CurrentArrows = 20;
    }

    public void UpdateCurrentArrows(int newArrows)
    {
        CurrentArrows += newArrows;

        if (arrowText == null)
        {
            arrowText = GameObject.Find(ARROW_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        arrowText.text = CurrentArrows.ToString("D2");
    }
}
