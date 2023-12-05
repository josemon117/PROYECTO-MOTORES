using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnemyTriggerEnterDo : OnTriggerEnterDo
{
    [SerializeField] private UnityEvent unignoredActions;
    [SerializeField] List<string> tagsToIgnore;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsEnabled)
        {
            return;
        }
        alwaysActions.Invoke();
        foreach (var ignoreTag in tagsToIgnore)
        {
            if (collision.tag == ignoreTag)
            {
                return;
            }
        }
        unignoredActions.Invoke();
    }

    public override void DestroyCollsionee()
    {
        Debug.LogFormat("Enemy shouldn't do anything against collisienee. Ignoring.");
    }
}
