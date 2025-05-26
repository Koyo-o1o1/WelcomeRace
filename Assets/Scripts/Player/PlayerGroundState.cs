using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public class PlayerGroundState : PlayerState
{

    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
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


        //�}�E�X�������ꂽ�Ƃ� //leap
        if (Input.GetKeyDown(KeyCode.Mouse0) || player.leapAttack)
        {
            //leap
            player.leapAttack = false;

            //�U��
            stateMachine.ChangeState(player.primaryAttack);

            
        }

        //���ƐڐG���Ă��Ȃ��Ƃ�
        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        //�W�����v�֑J��(���ƐڐG���Ă��鎞�̂�) //leap
        if ((Input.GetKey(KeyCode.UpArrow) || player.leapJump) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);

            //leap
            yInput = 0;
            player.leapJump = false;
        }

        
    }

}
