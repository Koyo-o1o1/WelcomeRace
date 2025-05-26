using UnityEngine;

public class PlayerAirState : PlayerState
{
    

    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //ジャンプ中に地面が検出されたとき
        if (player.IsGroundDetected())
        {
            player.SetVelocity(0, rb.linearVelocity.y);
            stateMachine.ChangeState(player.idleState);
            player.PlayBGM_Landing();
        }

        //ジャンプ中に少し移動
        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed*.92f*xInput,rb.linearVelocity.y);
        }
    }


    
}
