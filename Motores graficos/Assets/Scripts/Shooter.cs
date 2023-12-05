using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private ShootingConfig config;
    public ShootingConfig ShootingConfig
    {
        get
        {
            return config;
        }
    }
    public bool IsEnabled = true;

    public List<GameObject> allShoots = new List<GameObject>();

    private void Start()
    {
        if (config == null)
        {
            return;
        }
        if (config.autoShooting)
        {
            StartCoroutine(AutoShoot());
        }
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            DoShoot();
            yield return new WaitForSeconds(config.shootCadence);
        }
    }

    public void DoShoot()
    {
        if (IsEnabled && shootOrigin != null)
        {
            var shoot = Instantiate(shootPrefab, shootOrigin.position, shootOrigin.rotation);
            allShoots.Add(shoot);
        }
    }

    public void DestroyAllShootedShoots()
    {
        for (int i = 0; i < allShoots.Count; i++)
        {
            if (allShoots[i] == null)
            {
                continue;
            }
            Destroy(allShoots[i]);
        }
        allShoots.Clear();
    }

    public void EnableShooter(bool shouldEnable)
    {
        IsEnabled = shouldEnable;
    }
}
