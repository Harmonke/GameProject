using UnityEngine;

public class ScatterTrees : MonoBehaviour
{

    public GameObject spawnObjectPrefab;
    [SerializeField] float planetRadius;
    [SerializeField] int amount;
    [SerializeField] float scaleMin;
    [SerializeField] float scaleMax;
    [ContextMenu("Scatter Objects")]
    void Scatter()
    {
        //Gets and deletes all clones that are parented to the planet we're spawning on.
        GameObject[] oldSpawnedObjects = GameObject.FindGameObjectsWithTag(spawnObjectPrefab.tag);
        for (int oldSpawnedObjectsIndex = 0; oldSpawnedObjectsIndex < oldSpawnedObjects.Length; oldSpawnedObjectsIndex++)
        {
            GameObject objectToBeDestroyed = oldSpawnedObjects[oldSpawnedObjectsIndex];
            if (objectToBeDestroyed.transform.parent == gameObject.transform && objectToBeDestroyed.name != spawnObjectPrefab.name)
                DestroyImmediate(objectToBeDestroyed);
        }

        //Spawns clones of an object based on the given amount and given prefab and places them around the planet randomly with a small variable scale.
        for (int spawnedObjects = 0; spawnedObjects < amount; spawnedObjects++)
        {
            Vector3 planetPosition = transform.position;
            GameObject spawnObject = Instantiate(spawnObjectPrefab);
            
            float spawnObjectScale = Random.Range(scaleMin, scaleMax);
            spawnObject.transform.localScale = new Vector3(spawnObjectScale,spawnObjectScale,spawnObjectScale);
            Vector3 randomSpherePosition = planetPosition + Random.onUnitSphere * planetRadius;
            spawnObject.transform.position = randomSpherePosition;
            Vector3 planetToSpawnObjectDir = (spawnObject.transform.position - transform.position).normalized;
            
            spawnObject.transform.up = planetToSpawnObjectDir;
            spawnObject.transform.parent = gameObject.transform;
        }
        
        
    }
}
