using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = .8f;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;

        //�R���{�I��or�R���{�󂯕t�����Ԓ���
        if (comboCounter > 1 || Time.time >= lastTimeAttacked + comboWindow)
        {
            //�R���{�J�E���^�[���Z�b�g
            comboCounter = 0;
        }

        //���̎��̃R���{�J�E���^�[�ɉ������A�j���[�V����
        player.anim.SetInteger("ComboCounter",comboCounter);

        float attackDir = player.facingDirection;

        if (xInput != 0)
        {
            //�R���{���ɍU��������ς�����悤�ɂ���
            attackDir = xInput;
        }

        //�R���{���̈ړ�
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        //�R���{����؂�ւ���R���[�`���X�^�[�g
        player.StartCoroutine("BusyFor", .1f);

        //�R���{�J�E���^�[��i�߂�
        comboCounter++;
        //�Ō�̍U�����Ԃ��L��
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
