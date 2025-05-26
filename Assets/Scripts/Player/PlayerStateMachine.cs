using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState {  get; private set; }

    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    //ó‘Ô‚ÌXV
    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }

}
