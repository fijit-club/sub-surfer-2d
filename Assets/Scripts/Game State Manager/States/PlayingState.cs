using UnityEngine;

public class PlayingState : MonoBehaviour, IState
{
    [SerializeField] private GameObject[] goToEnableAndDisable;
    [SerializeField] private MonoBehaviour[] mbToEnableAndDisable;
    
    public void OnEnter()
    {
        foreach (var go in goToEnableAndDisable)
            go.SetActive(true);
        
        foreach (var mb in mbToEnableAndDisable)
            mb.enabled = true;
    }

    public void OnExit()
    {
        foreach (var go in goToEnableAndDisable)
            go.SetActive(false);
        
        foreach (var mb in mbToEnableAndDisable)
            mb.enabled = false;
    }
}
