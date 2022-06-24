using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameState _currentState;

    public List<Character> characterList = new List<Character>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        Instance = this;

        GridManager.Instance.OnCharacterSpawned += GetCharacter;

        _currentState = new GameStateSelecting(this);
        _currentState.Enter();
    }

    private void GetCharacter(Character character) 
    { 
        characterList.Add(character);
    }

    public void StateWalking()
    {
        _currentState.Exit();
        _currentState = new GameStateWalking(this);
        _currentState.Enter();
    }

    public void StateFighting(WayPoint wayPoint)
    {
        _currentState.Exit();
        _currentState = new GameStateFighting(this, wayPoint);
        _currentState.Enter();
    }

    public void CompleteSelection()
    {
        if(characterList.Count > 0)
            StateWalking();
    }
}
