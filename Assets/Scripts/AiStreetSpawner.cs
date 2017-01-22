using Pools;
using UnityEngine;

public class AiStreetSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform[] targetsLocation;
    public float MinTimeBeforeSpawn = 2f;
    public float MaxTimeBeforeSpawn = 10f;
    public string SpawnTag;

    private float timer;
    private float currentTimeBeforeSpawn;

	// Use this for initialization
	void Start ()
    {
        this.currentTimeBeforeSpawn = Random.Range(this.MinTimeBeforeSpawn, this.MaxTimeBeforeSpawn);
        if (this.objectsToSpawn == null || this.objectsToSpawn.Length == 0)
        {
            Debug.LogError("Add at least 1 item in spawner array");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer < this.currentTimeBeforeSpawn)
            return;
        this.timer = 0;
        this.currentTimeBeforeSpawn = Random.Range(this.MinTimeBeforeSpawn, this.MaxTimeBeforeSpawn);
        
        var objectToSpawn = Instantiate(this.GetRandomObjectInArray(this.objectsToSpawn)) as GameObject;
        objectToSpawn.transform.SetParent(this.transform, false);
        var charControl = objectToSpawn.GetComponentInChildren<UserAICharacterControl>();
        charControl.SetTarget(this.GetRandomObjectInArray(this.targetsLocation) as Transform);
        objectToSpawn.transform.position = this.transform.position;
        var component = objectToSpawn.AddComponent<SpawnOrigin>();
        component.SpawnTag = this.SpawnTag;
	}

    private Object GetRandomObjectInArray(System.Array array)
    {
        if (array == null || array.Length == 0)
        {
            Debug.LogError("Empty array given for GetRandomIndex");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        return array.GetValue(Random.Range(0, array.Length - 1)) as Object;
    }
}
