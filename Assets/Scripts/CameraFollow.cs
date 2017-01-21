using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private Vector3 _cameraPosition;

    private void Awake()
    {
        _cameraPosition = transform.position;
    }

    private void Update()
    {
        Vector3 playerPos = _target.position;
        Camera.main.transform.position = new Vector3(
            (playerPos.x - _target.parent.GetComponent<Transform>().position.x) + _cameraPosition.x,
            (playerPos.y - _target.parent.GetComponent<Transform>().position.y) + _cameraPosition.y,
            (playerPos.z - _target.parent.GetComponent<Transform>().position.z) + _cameraPosition.z);
    }
}