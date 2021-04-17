using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Game objects
    public GameObject _player;
    private GameObject instance_player; //GameObject that it'll instantiate the prefab Player.
    public GameObject _coin;
    GameObject[] coinsInLevel; 
    public GameObject _secondCamera;
    public GameObject mainCamera;
    
    [SerializeField] GameObject[] levels; //array que contendra los niveles.
    private GameObject _currentLevel; //nivel actual. Se define en LOADLEVEL del BeginState()


    //User Interface elements
    [SerializeField] GameObject panelPlay;
    [SerializeField] GameObject panelMenu;
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelLevelCompleted;
    public Text livesText;
    public Text coinsText;

    private Vector3 startingPoint = new Vector3 (-3.5f, 1.1f, 0f); //it'll storage the initial position of the Player.



    //State machine que enumera los gamestates
    public enum State {MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER} 
    State _state; //variable del state
    bool isSwitchingState;

    
    public static GameManager Instance { get; private set;} //para acceder desde otro script.

    public int coins;
    //propiedad de C#. mas info abajo. 
    public int Coins         
    {
        get {return coins;}
        set {coins = value;
            coinsText.text = "COINS: " + coins; //cada vez que cambie el valor, se actualizara solo.
        }
    }

    public int lives;
    public int Lives         
    {
        get {return lives;}
        set {
            lives = value;
            livesText.text = "LIVES: " + lives; //cada vez que cambie el valor, se actualizara solo.
            }   
    }

    public int _level;
    public int Level         
    {
        get {return _level;}
        set {_level = value;}
    }


    //Funcion para el boton START. Al ser publico se puede arrastrar al inspector. Mas info abajo
    public void StartClicked()
    {
        SwitchState(State.INIT);
    }


    void Start()
    {
        Instance = this; //para el getter y setter
        SwitchState(State.MENU);
    }

    //metodo para que el GameManager pueda cambiar de estados
    public void SwitchState(State newState, float delay = 0)  //Cuando llama a SwitchState, termina el estado actual y comienza el nuevo con un delay
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay) 
    {
        isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        isSwitchingState = false;
    }

    void BeginState(State newState) //funcion con switch para los States. en parentesis tipo de variable "State" y palabra especial "newState"
    {
        switch(newState)
        {
            case State.MENU:
                    _secondCamera.SetActive(true);
                    panelMenu.SetActive(true); //al iniciar el estado MENU el panel se vuelve verdadero
                break;
            case State.INIT:
                    mainCamera.SetActive(true);
                    panelPlay.SetActive(true);
                    Coins = 0;
                    Lives = 3;
                    Level = 0;
                    instance_player = Instantiate(_player);
                    SwitchState(State.LOADLEVEL);  
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                    panelLevelCompleted.SetActive(true);
                    Level++;
                    SwitchState(State.LOADLEVEL, 2f); //El delay float, fue introducido al crear el metodo SwitchState()
                break;
            case State.LOADLEVEL:
                    if(Level >= levels.Length)
                    {
                        SwitchState(State.GAMEOVER);        
                    }
                    else
                    {
                        _currentLevel = Instantiate(levels[Level]); //instancia los niveles.
                        GameObject.FindWithTag("Player").transform.position = startingPoint;
                        SwitchState(State.PLAY);        
                    }
                break;         
            case State.GAMEOVER:
                    panelGameOver.SetActive(true);
                    _secondCamera.SetActive(true);
                    mainCamera.SetActive(false);
                    Destroy(instance_player);
                break;         
        }
    }

    void Update()
    {
        switch(_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                    coinsInLevel = GameObject.FindGameObjectsWithTag("Coin"); //Obtiene la cantidad de GameObjects Coin
                    if(Lives <= 0)
                    {
                        SwitchState(State.GAMEOVER);
                    }
                    if(coinsInLevel.Length == 0 && _currentLevel != null && !isSwitchingState)
                    {
                        SwitchState(State.LEVELCOMPLETED);
                    }
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;         
            case State.GAMEOVER:
                break;         
        }

    }

    void EndState() //En este caso, el switch de la funcion EndState se va a aplicar sobre la variable _state que contiene el state actual.
    {
        switch(_state)
        {
            case State.MENU:
                    _secondCamera.SetActive(false);
                    panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                    panelLevelCompleted.SetActive(false);
                    Destroy(_currentLevel);
                break;
            case State.LOADLEVEL:
                break;         
            case State.GAMEOVER:
                    panelPlay.SetActive(false);
                    panelGameOver.SetActive(false);
                    _secondCamera.SetActive(false);
                break;         
        }
    }
    

}

/*
    La forma de acceder a la variable es: 
    "variable GameObject".GetComponent<"NombreDeScriptQueLoContiene">."NombreDeVariable"; El resto es para imprimir en pantalla. La variable a la que se quiere
    acceder hay que declararla como static

     player = GameObject.FindWithTag("Player"); //Busca al objeto a partir de su tag. Se coloca en la funcion Awake.
     En la funcion Start se define una variable que contendra la variable o funcion del script del otro gameobject.

     Para que funcione los botones, hay que definir una funcion publica y que cambie al estado que querramos. 
     Luego en la config del boton hay que configurar un evento en "on click", seleccionar el game manager y luego la funcion.

     Para instanciar niveles, antes crear prefabs de los mismos. Para definir los niveles usar un array. Despues arrastrar los prefabs al inspector.

     Para usar un Getter y un Setter: crear una variable publica y luego la propiedad con mismo nombre pero mayuscula. una vez creado todo, se puede acceder a la
     variable desde otro script, en este caso "Player". Para eso hay que definir un "public static GameManager" (aunque no es recomendado pero para empezar va)
     con nombre mayuscula de esta manera ---> public static GameManager Instance { get; private set;} y en el metodo Start(), escribir:
     "Instance = this;" luego en el otro script escribir "GameManager.Instance.Coins += coins;" "GameManager."variablestaticInstance"."nombredepropiedad" (operacion
     logica) "variabledentro de la propiedad".
     La variable Instance solo hace falta declararla una vez y se puede llamar a otras variables.

    -Para saber cuantos objetos de un mismo tag hay en el nivel. Crear un GameObject[] e inicializarlo como: 
    "GameObject.FindGameObjectsWithTag("Coin");" en este caso Coin. Y para acceder a la cantidad usar el nombre de la variable GameObject[] elegida, y poner
    .Length para recibir un int para todas las posiciones del array.

    -El metodo Invoke("nameOfMethod", floatDelay); solo puede llamar metodos que retornen void y que no tengan parametros (o sea lo que esta entre parentesis).

    -Para destruir un clone de gameobject, hay que crear una variable privada GameObject usarla para instanciar el prefab deseado y luego destruir ese instanciador
    ej: private GameObject instancePlayer;  y para instanciarlo --> InstancePlayer = Instantiate(_player); y luego Destroy(InstancePlayer);
*/