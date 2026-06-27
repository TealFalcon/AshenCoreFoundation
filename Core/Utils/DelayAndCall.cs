using UnityEngine;
using AshenCore.Core;
using VContainer;

public class DelayAndCall : MonoBehaviour
{


    public float Delay = 1f;
    public int sceneId = 1;
    public bool _isLoaded = false;

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

            if(_services.UI != null)
            {
        /*        GUIHelper test = _services.UI.OpenWindow<BaseDialog>();
                if (test.windowComponent is BaseDialog gui)

                    gui.Setup("Prueba de Dialog",
                    () => {
                        _services.UI.CloseWindow<BaseDialog>();
                    }
                    , "Aceptar"
                    , () => {
                        _services.Debug.Log("Aceptado", ConsoleMessageType.Info);
                    },"Cancelar", () => {
                        _services.Debug.Log("Cancelado", ConsoleMessageType.Info);
                    }, "¿Desea usted hacer una prueba de dialogo?");

                 */

                /*   GUIHelper test = _services.UI.OpenWindow<MessageBox>();
                if (test.windowComponent is MessageBox gui)
                {
                    //TODO Cargar ICONO
                    gui.Setup("",null, "HOLA AMORSHETE TE AMO MUCHISISISISISISIMO","Info","Aceptar", () => {
                        _services.Debug.Log("Aceptado", ConsoleMessageType.Info);
                        _services.UI.CloseWindow<MessageBox>();
                    });
                } */

                GUIHelper test = _services.UI.OpenWindow<InputDialog>();
                if (test.windowComponent is InputDialog gui)
                    gui.Setup("Aceptar",
                    (value) => {
                        _services.Debug.Log(value + " Captured", ConsoleMessageType.Info);
                        _services.UI.CloseWindow<InputDialog>();
                    }
                    , "Cancelar"
                    , () => {
                        _services.Debug.Log("Cancelado", ConsoleMessageType.Info);
                        _services.UI.CloseWindow<InputDialog>();
                    },"Inserte el nombre de su prima.","^[a-zA-Z ]+$");


            }


            _isLoaded = true;
        }
    
    }
}
