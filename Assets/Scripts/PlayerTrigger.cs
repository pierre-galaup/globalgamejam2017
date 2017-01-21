using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IA"))
        {
            GameManager.Instance.PlayerRadicalization.OnTriggerEnterNpc(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("IA"))
        {
            GameManager.Instance.PlayerRadicalization.OnTriggerExitNpc(other.gameObject);
        }
    }
}