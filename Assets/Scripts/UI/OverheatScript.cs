using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheatScript : MonoBehaviour
{
    [SerializeField] PlayerProjectileSpawner spawner;
    [Header("BarOver")]
    [SerializeField] GameObject barObject;
    private SpriteRenderer barRenderer;

    private float defaultScale, currentScale;

    private void Awake()
    {
        barRenderer = barObject.GetComponent<SpriteRenderer>();
        defaultScale = barObject.transform.localScale.x;
    }

    private void Update()
    {
        Vector3 _newScale = barObject.transform.localScale;

        if (spawner.IsOverheat)
        {
            barRenderer.color = Color.red;
            currentScale = Mathf.Lerp(0f, defaultScale, spawner.OverheatCooldownTimerCount / spawner.OverheatCooldownTimer);
        }
        else
        {
            barRenderer.color = Color.white;
            currentScale = (spawner.OverheatStack / spawner.OverheatStackLimit) * defaultScale;           
        }
        _newScale.x = currentScale;
        barObject.transform.localScale = _newScale;
    }
}
