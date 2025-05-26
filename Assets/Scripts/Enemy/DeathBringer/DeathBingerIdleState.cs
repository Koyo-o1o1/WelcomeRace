using UnityEngine;

public class DeathBingerIdleState : EnemyState
{
    private Enemy_DeathBringer enemy;
    private Transform player;

    public DeathBingerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName, Enemy_DeathBringer enemy) : base(_enemyBase, _stateMachiine, _animBooolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        
        stateTimer = enemy.idleTime;
        player=PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Vector2.Distance(player.transform.position,enemy.transform.position)<2)
            enemy.bossFightBegun = true;

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    stateMachine.ChangeState(enemy.teleportState);
        //}

        if (stateTimer < 0 && enemy.bossFightBegun)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
