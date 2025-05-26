using UnityEngine;

public class DeathBringerSpellCastState : EnemyState
{
    private Enemy_DeathBringer enemy;


    private int amountOfSpells;
    private float spellTimer;

    public DeathBringerSpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachiine, _animBooolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        amountOfSpells=enemy.amountOfSpells;
        spellTimer =.5f;
    }

    public override void Update()
    {
        base.Update();

        spellTimer-=Time.deltaTime;


        if (CanCast())
        {
            enemy.CastSpell();
        }

        //�����I�������
        if (amountOfSpells <= 0)
        {
            stateMachine.ChangeState(enemy.teleportState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeCast = Time.time;
    }


    private bool CanCast()
    {
        if (amountOfSpells > 0 && spellTimer < 0)
        {
            amountOfSpells--;
            spellTimer = enemy.spellCooldown;
            return true;
        }

        return false;
    }
}
