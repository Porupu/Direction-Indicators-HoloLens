using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DI_System : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageIndicator indicatorPrefab = null;
    [SerializeField] private RectTransform indicatorHolder = null;
    [SerializeField] private new Camera camera = null;
    [SerializeField] private Transform player = null;

    private Dictionary<Transform, DamageIndicator> Indicators = new Dictionary<Transform, DamageIndicator>();

    #region Delegates
    public static Action<Transform> CreateIndicator = delegate { };
    public static Func<Transform, bool> CheckIfObjectInSight = null;
    #endregion

    private void OnEnable()
    {
        CreateIndicator += Create;
        CheckIfObjectInSight += InSight;
    }

    private void OnDisable()
    {
        CreateIndicator -= Create;
        CheckIfObjectInSight -= InSight;
    }

    void Create(Transform target)
    {
        if (Indicators.ContainsKey(target)) //indicator already exists pointing to the target
        {
            Indicators[target].Restart(); //restart it's timer
            return;
        }
        DamageIndicator newIndicator = Instantiate(indicatorPrefab, indicatorHolder);
        newIndicator.Register(target, player, new Action( () => { Indicators.Remove(target); } ));

        Indicators.Add(target, newIndicator);
    }

    bool InSight(Transform target) //check if the target is in view of the camera
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(target.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
