using UnityEngine;
using UnityEngine.EventSystems;

public class DeathBringerBattleState : EnemyState
{
    private Enemy_DeathBringer enemy;
    private Transform player;
    private int moveDirection;

    public DeathBringerBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachiine, _animBooolName)
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

        //�v���C���[���o
        if (enemy.IsplayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsplayerDetected().distance < enemy.attacckDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
                else
                {
                    stateMachine.ChangeState(enemy.idleState);
                }
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

        //�G���v���C���[�̒�������Ȃ��悤��
        if(enemy.IsplayerDetected() && enemy.IsplayerDetected().distance < enemy.attacckDistance - .1f)
            return;

        //���x��^����
        enemy.SetVelocity(enemy.moveSpeed * moveDirection, rb.linearVelocity.y);
    }

    //�U���̊Ԋu�𐧌�
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttack + enemy.attackCooldown)
        {
            enemy.lastTimeAttack = Time.time;
            return true;
        }

        return false;
    }
}
