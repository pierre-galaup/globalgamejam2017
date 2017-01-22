using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonAnim : MonoBehaviour
{

    void Start()
    {
        this.ScalingDown();
    }

    void ScalingUp()
    {
        this.transform.DOScale(1.1f, 1).OnComplete(this.ScalingDown);
    }

    void ScalingDown()
    {
        this.transform.DOScale(0.9f, 1).OnComplete(this.ScalingUp);

    }

    void OnEnable()
    {

    }
}
