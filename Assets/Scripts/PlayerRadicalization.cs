using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerRadicalization : MonoBehaviour
{
    public int NumberOfNpcCanBeRadicalizaed = 1;

    public float TimeNeededForRadicalize = 2f;

    private bool _isRadicalizing = false;
    private float _timeSinceBeginRadicalize = 0f;
    private bool _animLaunched = false;

    public List<GameObject> _npcsOnTrigger = new List<GameObject>();

    public void OnTriggerEnterNpc(GameObject npc)
    {
        if (!_npcsOnTrigger.Contains(npc) && !npc.GetComponent<NpcStats>().IsRadicalized)
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
            if (_isRadicalizing)
            {
                _timeSinceBeginRadicalize += Time.deltaTime;
            }

            if (Input.GetButtonDown("Radicalize"))
            {
                _isRadicalizing = true;
                _animLaunched = false;

                GetComponent<Animator>().SetTrigger("ShowBook");

                foreach (GameObject npc in _npcsOnTrigger)
                {
                    var roaming = npc.GetComponent<RoamingAi>();
                    if (roaming != null)
                        roaming.StartListening();
                    else
                        npc.GetComponent<NavMeshController>().StopNavigation();

                    transform.DOLookAt(npc.transform.position, 0.5f);
                    npc.transform.DOLookAt(transform.position, 0.5f);
                }
            }

            if (_timeSinceBeginRadicalize >= 0.95f && !_animLaunched)
            {
                _animLaunched = true;
                foreach (GameObject npc in _npcsOnTrigger)
                {
                    npc.GetComponent<Animator>().SetTrigger("Radicalized");
                }
            }

            if (_timeSinceBeginRadicalize >= 2 || Input.GetButtonUp("Radicalize"))
            {
                if (_timeSinceBeginRadicalize >= TimeNeededForRadicalize)
                {
                    foreach (GameObject npc in _npcsOnTrigger)
                    {
                        if (!npc.GetComponent<NpcStats>().IsRadicalized)
                        {
                            GameManager.Instance.AddRadicalized();
                            npc.GetComponent<NpcStats>().Radicalize();
                            StartCoroutine(WaitForWalk(npc));
                        }
                    }
                    _npcsOnTrigger.Clear();
                }
                else
                {
                    foreach (GameObject npc in _npcsOnTrigger)
                    {
                        var roaming = npc.GetComponent<RoamingAi>();
                        if (roaming != null)
                            roaming.StopListening();
                        else
                            npc.GetComponent<NavMeshController>().ResumeNavigation();
                        npc.GetComponent<NpcStats>().HideWhiteEyes();
                        npc.GetComponent<Animator>().SetTrigger("StopRadicalized");
                    }
                }
                GetComponent<Animator>().SetTrigger("StopShowBook");
                _isRadicalizing = false;
                _timeSinceBeginRadicalize = 0;
                _animLaunched = false;
            }
        }
        else if (_isRadicalizing)
        {
            _isRadicalizing = false;
            _timeSinceBeginRadicalize = 0;
            if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("showbookLoop") ||
                this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("showBook"))
                this.GetComponent<Animator>().SetTrigger("StopShowBook");
        }
    }

    private IEnumerator WaitForWalk(GameObject npc)
    {
        yield return new WaitForSeconds(1.5f);
        var roaming = npc.GetComponent<RoamingAi>();
        if (roaming != null)
            roaming.StopListening();
        else
            npc.GetComponent<NavMeshController>().ResumeNavigation();
    }
}