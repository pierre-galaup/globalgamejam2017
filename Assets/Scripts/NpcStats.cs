using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class NpcStats : MonoBehaviour
{
    public bool IsRadicalized = false;

    [SerializeField]
    private MeshRenderer _scarf1;

    [SerializeField]
    private MeshRenderer _scarf2;

    [SerializeField]
    private MeshRenderer _scarf3;

    [SerializeField]
    private MeshRenderer _scarf4;

    [SerializeField]
    private MeshRenderer _oldEyeR;

    [SerializeField]
    private MeshRenderer _oldEyeL;

    [SerializeField]
    private MeshRenderer _newEyeR;

    [SerializeField]
    private MeshRenderer _newEyeL;

    [SerializeField]
    private MeshRenderer _bigEyeR;

    [SerializeField]
    private MeshRenderer _bigEyeL;

    [SerializeField]
    private MeshRenderer _eyebrowR;

    [SerializeField]
    private MeshRenderer _eyebrowL;

    public float Speed;
    public float Resist;
    public bool WontStop;

    private NavMeshAgent agent;
    private UserThirdPersonCharacter character;

    private void Awake()
    {
        _scarf1 = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/m_echarpe1").GetComponent<MeshRenderer>();
        _scarf2 = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/m_echarpe2").GetComponent<MeshRenderer>();
        _scarf3 = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/m_echarpe3").GetComponent<MeshRenderer>();
        _scarf4 = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/m_echarpe4").GetComponent<MeshRenderer>();

        _oldEyeR = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/j_cou/j_tete/j_oeil_D/m_oeil_D").GetComponent<MeshRenderer>();
        _oldEyeL = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/j_cou/j_tete/j_oeil_G/m_oeil_G").GetComponent<MeshRenderer>();

        _newEyeR = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/j_cou/j_tete/j_oeil_D/m_oeil_D1").GetComponent<MeshRenderer>();
        _newEyeL = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/j_cou/j_tete/j_oeil_G/m_oeil_G1").GetComponent<MeshRenderer>();

        _bigEyeR = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/j_cou/j_tete/j_oeil_D/m_oeil_endoctrined_D").GetComponent<MeshRenderer>();
        _bigEyeL = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/j_cou/j_tete/j_oeil_G/m_oeil_endoctrined_G").GetComponent<MeshRenderer>();

        _eyebrowR = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/j_cou/j_tete/j_oeil_D/m_sourcil_D").GetComponent<MeshRenderer>();
        _eyebrowL = gameObject.transform.Find("skeleton/j_pelvis/j_tronc/j_cou/j_tete/j_oeil_G/m_sourcil_G").GetComponent<MeshRenderer>();
    }

    public void Radicalize()
    {
        IsRadicalized = true;

        _bigEyeL.enabled = false;
        _bigEyeR.enabled = false;
    }

    private void InitStats()
    {
        this.agent = this.GetComponent<NavMeshAgent>();
        this.character = this.GetComponent<UserThirdPersonCharacter>();
        if (this.agent == null || this.character == null)
            return;
        if (this.Speed < 0)
            return;
        this.Speed = Random.Range(1f, 5f);
        this.agent.speed = this.Speed;
        this.character.AnimSpeedMultipliter = this.Speed * 3;
        
    }

    private void Start()
    {
        this.InitStats();
        Animator animator = GetComponent<Animator>();
        if (animator.runtimeAnimatorController.animationClips[2].events.Length == 0)
        {
            AnimationEvent animationEvent = new AnimationEvent
            {
                time = 2.02f,
                functionName = "ClothesChoose"
            };

            AnimationClip clip = animator.runtimeAnimatorController.animationClips[2];
            clip.AddEvent(animationEvent);

            animationEvent = new AnimationEvent
            {
                time = 0.15f,
                functionName = "DisplayWhiteEyes"
            };

            clip = animator.runtimeAnimatorController.animationClips[2];
            clip.AddEvent(animationEvent);

            animationEvent = new AnimationEvent
            {
                time = 2.00f,
                functionName = "HideWhiteEyes"
            };

            clip = animator.runtimeAnimatorController.animationClips[2];
            clip.AddEvent(animationEvent);
        }
    }

    public void ClothesChoose()
    {
        _scarf1.enabled = true;
        _scarf2.enabled = true;
        _scarf3.enabled = true;
        _scarf4.enabled = true;

        _newEyeL.enabled = true;
        _newEyeR.enabled = true;

        _oldEyeL.enabled = false;
        _oldEyeR.enabled = false;

        _eyebrowL.enabled = true;
        _eyebrowR.enabled = true;
    }

    public void DisplayWhiteEyes()
    {
        _bigEyeL.enabled = true;
        _bigEyeR.enabled = true;

        //_oldEyeL.enabled = false;
        //_oldEyeR.enabled = false;
    }

    public void HideWhiteEyes()
    {
        _bigEyeL.enabled = false;
        _bigEyeR.enabled = false;

        //_oldEyeL.enabled = true;
        //_oldEyeR.enabled = true;
    }
}