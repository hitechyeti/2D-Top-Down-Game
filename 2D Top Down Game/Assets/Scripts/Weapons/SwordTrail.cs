using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrail : Singleton<SwordTrail>
{
    [SerializeField] private TrailRenderer trailRenderer;

    protected override void Awake()
    {
        base.Awake();

        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        trailRenderer.emitting = false;
    }

    public void SwordTrailOn()
    {
        trailRenderer.emitting = true;
    }

    public void SwordTrailOff()
    {
        trailRenderer.emitting = false;
    }

}
