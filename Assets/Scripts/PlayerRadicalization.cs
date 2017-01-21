using System.Collections.Generic;
using UnityEngine;

public class PlayerRadicalization : MonoBehaviour
{
    public int NumberOfNpcCanBeRadicalizaed = 1;

    public float TimeNeededForRadicalize = 2f;

    private bool _isRadicalize = false;
    private float _timeSinceBeginRadicalize = 0f;

    public List<GameObject> _npcsOnTrigger = new List<GameObject>();

    public void OnTriggerEnterNpc(GameObject npc)
    {
        if (!_npcsOnTrigger.Contains(npc))
        {
            _npcsOnTrigger.Add(npc);
        }
    }

    public void OnTriggerExitNpc(GameObject npc)
    {
        if (_npcsOnTrigger.Contains(npc))
        {
            _npcsOnTrigger.Remove(npc);
        }
    }

    private void Update()
    {
        if (_npcsOnTrigger.Count >= 1 && _npcsOnTrigger.Count <= NumberOfNpcCanBeRadicalizaed)
        {
            if (_isRadicalize)
            {
                _timeSinceBeginRadicalize += Time.deltaTime;
            }

            if (Input.GetButtonDown("Radicalize"))
            {
                _isRadicalize = true;
            }

            if (Input.GetButtonUp("Radicalize"))
            {
                Debug.Log("TIME SINCE BEGIN : " + _timeSinceBeginRadicalize);
                if (_timeSinceBeginRadicalize <= TimeNeededForRadicalize)
                {
                    foreach (GameObject npc in _npcsOnTrigger)
                    {
                        GameManager.Instance.AddRadicalized();
                        npc.GetComponent<NpcStats>().Radicalize();
                    }
                }
                else
                {
                    // TODO : Radicalize NOT OK
                }
            }
        }
        _isRadicalize = false;
        _timeSinceBeginRadicalize = 0;
    }
}