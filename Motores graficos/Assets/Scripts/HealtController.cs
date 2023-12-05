using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public int health;
    [SerializeField] private UnityEvent onZeroHealthActions;

    public void OnDamage(int damageAmount)
    {
        health -= damageAmount;
        //StatisticsManager.SetStatisticValue(StatisticId.PlayerSuccessfulShoots, 1, true);
        Debug.LogFormat("OnDamage, current health is {0}", health);
        if (health <= 0)
        {
            OnZeroHealth();
        }
    }

    public void SetHealth(int value)
    {
        health = value;
    }

    public void OnZeroHealth()
    {
        if (onZeroHealthActions != null)
        {
            onZeroHealthActions.Invoke();
        }
    }
}
