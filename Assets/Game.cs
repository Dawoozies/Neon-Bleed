using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityHFSM;
public class Game : MonoBehaviour
{
    public static Game GameInstance;
    void Awake()
    {
        GameInstance = this;
    }
    public Player player;
    public TMP_Text screenText;
    public StateMachine<GameState> gameStateMachine;

    public Location currentLocation;

    //home state
        // build
        // rest
        // inventory
        // explore
        // 
        // options
            //option requirement
            //DoOption

    //exploring state
            
    //battle state
        //player side turn
        //enemy side turn
    //fishing state
    //woodcutting state
    void Start()
    {
        gameStateMachine = new StateMachine<GameState>();
        gameStateMachine.SetStartState(GameState.Home);
        screenText.text = "";
        screenText.text += $"<size=200%><align=left>{currentLocation.GetName()}";
    }
    public enum GameState
    {
        Home,
        Exploring,
        Battle,
        Fishing, //stardew fishing minigame over n over
        Woodcutting,
        Farming,
        Resting,
    }
}