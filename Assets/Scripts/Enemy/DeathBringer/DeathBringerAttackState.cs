using UnityEngine;

public class DeathBringerAttackState : EnemyState
{
    private Enemy_DeathBringer enemy;
    public DeathBringerAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName, Enemy_DeathBringer enemy) : base(_enemyBase, _stateMachiine, _animBooolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.chanceToTeleport += 5;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
        {
            if(enemy.CanTeleport())
                stateMachine.ChangeState(enemy.teleportState);
            else
                stateMachine.ChangeState(enemy.battleState);
        }
    }
}
