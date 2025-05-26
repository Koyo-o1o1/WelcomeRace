using UnityEngine;

public class SkeltonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skelton enemy;
    private int moveDirection;

    public SkeltonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName,Enemy_Skelton _enemy) : base(_enemyBase, _stateMachiine, _animBooolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //�ǉ�
        //�ǂ̌��oor�n�ʂ��Ȃ�
        if (enemy.isWallDetcted() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);

        }

        //�v���C���[���o
        if (enemy.IsplayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsplayerDetected().distance < enemy.attacckDistance)
            {
                if(canAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        //�o�g����Ԓ��Ɍ�����or�o�g����Ԃ̎��Ԃ��߂����ꍇ
        else
        {
            if(stateTimer < 0 || Vector2.Distance(player.transform.position,enemy.transform.position) > 15)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        //�v���C���[�̕����E�ɂ���ꍇ
        if (player.position.x > enemy.transform.position.x)
        {
            moveDirection = 1;
        }
        //�v���C���[�̂ق������ɂ���ꍇ
        else
        {
            moveDirection = -1;
        }

        //���x��^����
        enemy.SetVelocity(enemy.moveSpeed * moveDirection, rb.linearVelocity.y);
    }

    //�U���̊Ԋu�𐧌�
    private bool canAttack()
    {
        if (Time.time >= enemy.lastTimeAttack + enemy.attackCooldown)
        {
            enemy.lastTimeAttack = Time.time;
            return true;
        }

        return false;
    }
}
