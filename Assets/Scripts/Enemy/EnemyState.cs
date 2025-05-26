using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    private string animBooolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachiine;
        this.animBooolName = _animBooolName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBooolName, true);
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBooolName, false);
        enemyBase.AssignLastAnimBoolName(animBooolName);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
