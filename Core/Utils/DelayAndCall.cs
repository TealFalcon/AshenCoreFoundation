using UnityEngine;
using AshenCore.Core;
using VContainer;

public class DelayAndCall : MonoBehaviour
{


    public float Delay = 1f;
    public int sceneId = 1;
    private bool _isLoaded = false;

    private ACSceneManager _sceneManager;
    private ACSpawnerService _spawnerService;
    private IACPersistenceSystem _persistenceSystem;

    private AshenCoreServices _services;

    public float currentTime = 0f;

    public GameObject prefab;


    [Inject]
    void Construct(AshenCoreServices services)
    {
        this._services = services;

    }


    void Start()
    {

    }


    void Update()
    {
        currentTime += Time.deltaTime;
        

        if (currentTime >= Delay && !_isLoaded)
        {
            if (_services.Spawner == null)
                Debug.Log("NO HAY MANAGER");
            else
                Debug.Log("SI HAY MANAGER");

            _services.Scenes?.LoadScene(sceneId);

            if(_services.Persistence != null)
                _services.Persistence.SaveAsync(0);

            _isLoaded = true;
        }
    
    }
}
