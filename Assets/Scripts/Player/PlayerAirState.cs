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

        //�W�����v���ɒn�ʂ����o���ꂽ�Ƃ�
        if (player.IsGroundDetected())
        {
            player.SetVelocity(0, rb.linearVelocity.y);
            stateMachine.ChangeState(player.idleState);
            player.PlayBGM_Landing();
        }

        //�W�����v���ɏ����ړ�
        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed*.92f*xInput,rb.linearVelocity.y);
        }
    }


    
}
