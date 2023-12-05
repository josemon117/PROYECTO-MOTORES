using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShootingConfig", menuName = "Shooting/Shooting config", order = 0)]
public class ShootingConfig : ScriptableObject
{
    public float shootInitialWaitTime;
    public float shootCadence;
    public int damage;
    public bool autoShooting = false;
}
