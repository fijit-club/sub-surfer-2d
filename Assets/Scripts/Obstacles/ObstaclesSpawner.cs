using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles1;
    [SerializeField] private GameObject[] obstacles2;
    [SerializeField] private float x;
    [SerializeField] private GameObject[] coinsRows;
    
    private GameObject _coin;
    private List<int> _positions = new List<int>();
    
    private void Start()
    {
        if (Random.Range(0, 5) == 0)
        {
            obstacles1[3].SetActive(true);
            obstacles1[3].transform.localPosition += obstacles1[0].transform.right * Random.Range(-.1f, .3f);
        }
        else
        {
            var r1 = Random.Range(0, 3);
            if (r1 == 0)
                _positions.Add(2);
            else if (r1 == 1)
                _positions.Add(0);
            else if (r1 == 2)
                _positions.Add(1);

            var r2 = LaneHandler.CurrentLane;
            _positions.Add(r2);
            
            obstacles1[r2].SetActive(true);
            obstacles1[r2].transform.localPosition +=
                obstacles1[0].transform.right * Random.Range(-x, 0f);

            _coin = obstacles1[r2].transform.GetChild(0).gameObject;

            int coinsRowLane = 0;

            if (_positions.Contains(0) && _positions.Contains(1))
                coinsRowLane = 2;
            else if (_positions.Contains(0) && _positions.Contains(2))
                coinsRowLane = 1;
            else if (_positions.Contains(1) && _positions.Contains(2))
                coinsRowLane = 0;
            else if (_positions.Contains(0))
                coinsRowLane = Random.Range(1, 3);
            else if (_positions.Contains(1))
                coinsRowLane = 0;
            else if (_positions.Contains(2))
                coinsRowLane = Random.Range(0, 2);
            
            transform.name =  r1 + " : " + r2 + " : "+ coinsRowLane;

            int rCoin = Random.Range(0, 5);
            
            if (rCoin == 1)
            {
                _coin.SetActive(true);
            }
            else if (rCoin == 0)
            {
                coinsRows[coinsRowLane].SetActive(true);
            }

            obstacles2[r1].SetActive(true);
            obstacles2[r1].transform.localPosition += obstacles2[0].transform.right * Random.Range(0f, x);
        }
    }
}
