using UnityEngine;

[System.Serializable]
public class NpcStats : MonoBehaviour
{
    public bool IsRadicalized = false;

    public void Radicalize()
    {
        IsRadicalized = true;
        // TODO : Changement de couleur
        // TODO : Changement d'IA
    }
}