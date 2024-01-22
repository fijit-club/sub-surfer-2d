using UnityEngine;

public class SpawnCollision : MonoBehaviour
{
    [SerializeField] private RoadSpawner roadSpawner;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Spawn Trigger"))
        {
            roadSpawner.Spawn();
            if (col.transform.parent.GetComponent<ObstaclesSpawner>() != null) 
                if (col.transform.parent.GetComponent<ObstaclesSpawner>().enabled)
                    Destroy(col.transform.parent.gameObject, 3f);
        }
    }
}
