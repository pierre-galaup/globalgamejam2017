using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAiStreet : MonoBehaviour
{
    public List<string> TagsToKill;

    private void OnCollisionEnter(Collision other)
    {
        var spawnOrigin = other.gameObject.GetComponentInParent<SpawnOrigin>();
        if (spawnOrigin == null)
            return;
        var tag = spawnOrigin.SpawnTag;
        if (!this.TagsToKill.Contains(tag))
            return;
        if (other.transform.parent != null)
            Destroy(other.transform.parent.gameObject);
        else
            Destroy(other.gameObject);
    }
}
