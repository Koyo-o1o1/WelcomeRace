using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // まだゲームスタート直後1秒以内なら攻撃を無効化
        if (Time.time - GameStartManager.gameStartTime < 1.1f)
            return;

        //壁に向かって移動しようとするとき
        if (xInput == player.facingDirection && player.isWallDetcted())
        {
            return;
        }

        //左右入力が0出ないとき、かつ、コンボ中でないとき
        if (xInput != 0 && !player.isBusy)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
