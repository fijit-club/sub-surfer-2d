using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles1;
    [SerializeField] private GameObject[] obstacles2;
    [SerializeField] private float x;
    
    private void Start()
    {
        if (Random.Range(0, 5) == 0)
        {
            obstacles1[3].SetActive(true);
            obstacles1[3].transform.localPosition += obstacles1[0].transform.right * Random.Range(-.3f, .3f);
        }
        else
        {
            
            var r1 = Random.Range(0, 3);
            var r2 = Random.Range(0, 3);
            obstacles1[LaneHandler.CurrentLane].SetActive(true);
            obstacles1[LaneHandler.CurrentLane].transform.localPosition +=
                obstacles1[0].transform.right * Random.Range(-x, 0f);

            obstacles2[r1].SetActive(true);
            obstacles2[r1].transform.localPosition += obstacles2[0].transform.right * Random.Range(0f, x);
        }
    }
}
