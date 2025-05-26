using UnityEngine;

public class DeathBringerTeleportState : EnemyState
{

    private Enemy_DeathBringer enemy;

    public DeathBringerTeleportState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachiine, _animBooolName)
    {
        this.enemy = _enemy;
    }


    public override void Enter()
    {
        base.Enter();

        //‚Å‚«‚È‚¢
        //enemy.fx.MakeTransparent(true);
        //enemy_trigger.MakeInvisible();

    }

    public override void Exit()
    {
        base.Exit();

        //‚È‚º‚©‚Å‚«‚È‚¢
        //enemy_trigger.MakeVisible();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (enemy.CanDoSpellCast())
            {
                stateMachine.ChangeState(enemy.spellCastState);
            }
            else 
            {
                stateMachine.ChangeState(enemy.battleState);
            }
            
        }
    }
}
