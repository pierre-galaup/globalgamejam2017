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
    private float _animBeginTime = 0;

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

    private void Start()
    {
        _animBeginTime = TimeNeededForRadicalize - 1.05f;
    }

    private void Update()
    {
        if (_npcsOnTrigger.Count >= 1 && _npcsOnTrigger.Count <= NumberOfNpcCanBeRadicalizaed) // can radicalize
        {
            GameManager.Instance.GuiManager.ActionPanel.SetActive(true);
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

            if (_timeSinceBeginRadicalize >= _animBeginTime && !_animLaunched)
            {
                _animLaunched = true;
                foreach (GameObject npc in _npcsOnTrigger)
                {
                    npc.GetComponent<Animator>().SetTrigger("Radicalized");
                    npc.GetComponentInChildren<Light>().DOIntensity(6.32f, 2.5f);
                }
            }

            if (_timeSinceBeginRadicalize >= TimeNeededForRadicalize || Input.GetButtonUp("Radicalize"))
            {
                if (_timeSinceBeginRadicalize >= TimeNeededForRadicalize) // is radicalized
                {
                    GetComponent<AudioSource>().Play();
                    GameManager.Instance.GuiManager.ActionPanel.SetActive(false);
                    foreach (GameObject npc in _npcsOnTrigger)
                    {
                        if (!npc.GetComponent<NpcStats>().IsRadicalized)
                        {
                            GameManager.Instance.AddRadicalized();
                            npc.GetComponent<NpcStats>().Radicalize();
                            npc.GetComponentInChildren<Light>().DOIntensity(2.25f, 1.5f);
                            GameObject npc1 = npc;
                            DOTween.To(() => npc1.GetComponentInChildren<Light>().spotAngle, x => npc1.GetComponentInChildren<Light>().spotAngle = x, 63.17f, 1.5f);
                            StartCoroutine(WaitForWalk(npc));
                        }
                    }
                    _npcsOnTrigger.Clear();
                }
                else // need more time (cancelled)
                {
                    GameManager.Instance.GuiManager.ActionPanel.SetActive(false);
                    foreach (GameObject npc in _npcsOnTrigger)
                    {
                        var roaming = npc.GetComponent<RoamingAi>();
                        if (roaming != null)
                            roaming.StopListening();
                        else
                            npc.GetComponent<NavMeshController>().ResumeNavigation();
                        npc.GetComponent<NpcStats>().HideWhiteEyes();
                        npc.GetComponent<Animator>().SetTrigger("StopRadicalized");
                        DOTween.Kill(npc.GetComponentInChildren<Light>());
                        npc.GetComponentInChildren<Light>().DOIntensity(0f, 1.5f);
                    }
                }
                GetComponent<Animator>().SetTrigger("StopShowBook");
                _isRadicalizing = false;
                _timeSinceBeginRadicalize = 0;
                _animLaunched = false;
            }
        }
        else if (_isRadicalizing) // interrup by an AI or move
        {
            GameManager.Instance.GuiManager.ActionPanel.SetActive(false);
            _isRadicalizing = false;
            _timeSinceBeginRadicalize = 0;
            if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("showbookLoop") ||
                this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("showBook"))
                this.GetComponent<Animator>().SetTrigger("StopShowBook");
        }
        else
            GameManager.Instance.GuiManager.ActionPanel.SetActive(false);
    }

    private IEnumerator WaitForWalk(GameObject npc)
    {
        yield return new WaitForSeconds(1.5f);
        var roaming = npc.GetComponent<RoamingAi>();
        if (roaming != null)
        {
            roaming.StopListening();
            npc.GetComponent<UserAICharacterControl>().target = transform;
            //roaming.Target = transform;
        }
        else
        {
            npc.GetComponent<NavMeshController>().ResumeNavigation();
            npc.GetComponent<UserAICharacterControl>().target = transform;
        }
    }
}