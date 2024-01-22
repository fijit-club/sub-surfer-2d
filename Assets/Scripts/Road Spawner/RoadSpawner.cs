using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ElevationChanges
{
    public float upElevationTranslationX;
    public float upElevationTranslationY;
    public float downElevationTranslationX;
    public float downElevationTranslationY;
    public float upTranslationAfterElevationX;
    public float upTranslationAfterElevationY;
    public float downTranslationAfterElevationX;
    public float downTranslationAfterElevationY;
}

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject road;
    [SerializeField] private GameObject upElevation;
    [SerializeField] private GameObject downElevation;
    [SerializeField] private ElevationChanges elevation;
    
    private bool _lastWasElevation;

    public void Spawn()
    {
        if (Random.Range(0, 5) == 1 && !_lastWasElevation)
        {
            if (Random.Range(0, 2) == 0)
            {
                transform.Translate(elevation.upElevationTranslationX, elevation.upElevationTranslationY, 0f);
                var elev = Instantiate(upElevation, transform.position, upElevation.transform.rotation);
                transform.Translate(elevation.upTranslationAfterElevationX, elevation.upTranslationAfterElevationY, 0f);
                elev.GetComponent<ElevationRoadData>().elevation = transform.position.y;
            }
            else
            {
                transform.Translate(elevation.downElevationTranslationX, elevation.downElevationTranslationY, 0f);
                var elev = Instantiate(downElevation, transform.position, downElevation.transform.rotation);
                transform.Translate(elevation.downTranslationAfterElevationX, elevation.downTranslationAfterElevationY, 0f);
                elev.GetComponent<ElevationRoadData>().elevation = transform.position.y;
            }

            _lastWasElevation = true;
        }
        else
        {
            if (!_lastWasElevation)
                transform.Translate(6f, 0f, 0f);
            _lastWasElevation = false;
            Instantiate(road, transform.position, Quaternion.identity);
        }
    }
}
