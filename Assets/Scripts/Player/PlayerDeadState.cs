using System;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    private ContinueUIManager continueUIManager;


     

    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();



        // ContinueUIManager���V�[������擾
        continueUIManager = GameManager.Instance.continueUIManager;
        //continueUI�\��
        continueUIManager.Continue_UI_Start();
    }


    public override void Exit()
    {
        base.Exit();

        // �Q�[���S�̂̎��Ԃ��~
        Time.timeScale = 0f;

        
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();
    }
}
