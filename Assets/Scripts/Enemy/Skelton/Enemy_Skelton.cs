using UnityEngine;

public class Enemy_Skelton : Enemy
{

    #region

    public SkeltonIdleState idleState {  get; private set; }
    public SkeltonMoveState moveState { get; private set; }
    public SkeltonBattleState battleState { get; private set; }
    public SkeltonAttackState attackState { get; private set; }
    public SkeltonStunnedState stunnedstate { get; private set; }
    public SkeltonDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeltonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeltonMoveState(this, stateMachine,"Move",this);
        battleState = new SkeltonBattleState(this, stateMachine, "Move", this);
        attackState=new SkeltonAttackState(this,stateMachine,"Attack",this);
        deadState = new SkeltonDeadState(this, stateMachine, "Dead", this);
        

    }

    // idleèÛë‘Ç÷
    public override void ResetToIdleState()
    {
        stateMachine.ChangeState(idleState);
    }


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }



    //í«â¡ÇµÇƒÇ›ÇΩÇ‚Ç¬
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
