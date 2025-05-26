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

        //プレイヤー検出
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

        //プレイヤーの方が右にいる場合
        if (player.position.x > enemy.transform.position.x)
        {
            moveDirection = 1;
        }
        //プレイヤーのほうが左にいる場合
        else
        {
            moveDirection = -1;
        }

        //敵がプレイヤーの中を歩かないように
        if(enemy.IsplayerDetected() && enemy.IsplayerDetected().distance < enemy.attacckDistance - .1f)
            return;

        //速度を与える
        enemy.SetVelocity(enemy.moveSpeed * moveDirection, rb.linearVelocity.y);
    }

    //攻撃の間隔を制御
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
