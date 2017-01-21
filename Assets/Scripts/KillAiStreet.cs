using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAiStreet : MonoBehaviour
{
    public List<string> TagsToKill;

    private void OnTriggerEnter(Collider other)
    {
        if (!this.TagsToKill.Contains(other.tag))
            return;
        if (other.transform.parent != null)
            Destroy(other.transform.parent.gameObject);
        else
            Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        var tag = other.gameObject.GetComponentInParent<SpawnOrigin>().SpawnTag;
        if (!this.TagsToKill.Contains(tag))
            return;
        if (other.transform.parent != null)
            Destroy(other.transform.parent.gameObject);
        else
            Destroy(other.gameObject);
    }
}
