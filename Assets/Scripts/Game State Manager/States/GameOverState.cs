using UnityEngine;

public class GameOverState : MonoBehaviour, IState
{
    [SerializeField] private GameObject[] goToEnableAndDisable;
    [SerializeField] private MonoBehaviour[] mbToEnableAndDisable;
    
    [SerializeField] private Transform extraRoads;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerChild;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform spawner;
    [SerializeField] private LaneHandler laneHandler;
    [SerializeField] private PlayerElevationHandler playerElevation;
    [SerializeField] private PlayerMovement playerMovement;
    
    private Vector3 _initPlayerPos;
    private Vector3 _initCamPos;
    private Vector3 _initSpawnerPos;
    private Vector3 _initPlayerChildPos;
    
    private void Start()
    {
        _initPlayerPos = player.position;
        _initCamPos = playerCam.position;
        _initSpawnerPos = spawner.position;
        _initPlayerChildPos = playerChild.localPosition;
    }

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

        for (int i = 0; i < extraRoads.childCount; i++)
            Destroy(extraRoads.GetChild(i).gameObject);

        CoinsHandler.Coins = 0;
        player.position = _initPlayerPos;
        playerCam.position = _initCamPos;
        spawner.position = _initSpawnerPos;
        LaneHandler.CurrentLane = 1;
        playerElevation.ResetState();
        playerMovement.ResetSpeed();
        laneHandler.UpdateLane();
        playerChild.localPosition = _initPlayerChildPos;
        var pos = playerChild.GetChild(0).localPosition;
        pos.x = 0f;
        playerChild.GetChild(0).localPosition = pos;
    }
}
