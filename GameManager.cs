using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameObjects in scene
    public GameObject _player;
    private GameObject instance_player; 
    public GameObject _coin;
    public GameObject mainCamera;
    public GameObject _secondCamera;
    private GameObject[] coinsInLevel; 
    private GameObject _currentLevel; 
    [SerializeField] GameObject[] levels; 

    // UI elements
    [SerializeField] GameObject panelPlay;
    [SerializeField] GameObject panelMenu;
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelLevelCompleted;
    public Text livesText;
    public Text coinsText;

    private Vector3 startingPoint = new Vector3 (-3.5f, 1.1f, 0f); 

    public static GameManager Instance { get; private set;} 

    // State machine for the game states
    public enum State {MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER} 
    State _state; // state variable
    bool isSwitchingState;

    

    public int coins;
    public int Coins         
    {
        get {return coins;}
        set {coins = value;
            coinsText.text = "COINS: " + coins; 
        }
    }

    public int lives;
    public int Lives         
    {
        get {return lives;}
        set {
            lives = value;
            livesText.text = "LIVES: " + lives; 
            }   
    }

    public int _level;
    public int Level         
    {
        get {return _level;}
        set {_level = value;}
    }


    public void StartClicked()
    {
        SwitchState(State.INIT);
    }


    void Start()
    {
        Instance = this; 
        SwitchState(State.MENU);
    }

    // Methods for changing states
    public void SwitchState(State newState, float delay = 0)  // Cuando llama a SwitchState, termina el estado actual y comienza el nuevo con un delay
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    // This is for the couritine. It adds a delay and an order of execution
    IEnumerator SwitchDelay(State newState, float delay) 
    {
        isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        isSwitchingState = false;
    }

    void BeginState(State newState) // metodo con switch para los States. en parentesis tipo de variable "State" y palabra especial "newState"
    {
        switch(newState)
        {
            case State.MENU:
                    _secondCamera.SetActive(true);
                    panelMenu.SetActive(true); 
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
                    SwitchState(State.LOADLEVEL, 2f); // El delay float, fue introducido al crear el metodo SwitchState()
                break;
            case State.LOADLEVEL:
                    if(Level >= levels.Length)
                    {
                        SwitchState(State.GAMEOVER);        
                    }
                    else
                    {
                        _currentLevel = Instantiate(levels[Level]); 
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
                    coinsInLevel = GameObject.FindGameObjectsWithTag("Coin"); 
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

    void EndState() // En este caso, el switch de la funcion EndState se va a aplicar sobre la variable _state que contiene el state actual.
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