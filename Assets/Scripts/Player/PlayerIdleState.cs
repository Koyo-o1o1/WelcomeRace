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

        // �܂��Q�[���X�^�[�g����1�b�ȓ��Ȃ�U���𖳌���
        if (Time.time - GameStartManager.gameStartTime < 1.1f)
            return;

        //�ǂɌ������Ĉړ����悤�Ƃ���Ƃ�
        if (xInput == player.facingDirection && player.isWallDetcted())
        {
            return;
        }

        //���E���͂�0�o�Ȃ��Ƃ��A���A�R���{���łȂ��Ƃ�
        if (xInput != 0 && !player.isBusy)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
