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

        // �܂��Q�[���X�^�[�g����1�b�ȓ��Ȃ�U���𖳌���
        if (Time.time - GameStartManager.gameStartTime < 1.1f)
            return;

        player.SetVelocity(player.moveSpeed * xInput , rb.linearVelocity.y);

        //�������Ă��Ȃ����or�ǂ̌��o
        if (xInput == 0 || player.isWallDetcted())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
