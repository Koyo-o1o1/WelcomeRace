using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
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

        // まだゲームスタート直後1秒以内なら攻撃を無効化
        if (Time.time - GameStartManager.gameStartTime < 1.1f)
            return;

        player.SetVelocity(player.moveSpeed * xInput , rb.linearVelocity.y);

        //何もしていない状態or壁の検出
        if (xInput == 0 || player.isWallDetcted())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
