using NUnit.Framework;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy_DeathBringer : Enemy
{
    #region States

    public DeathBingerIdleState idleState { get; private set; }
    public DeathBringerBattleState battleState { get; private set; }
    public DeathBringerAttackState attackState { get; private set; }
    public DeathBringerDeadState deadState { get; private set; }
    public DeathBringerSpellCastState spellCastState { get; private set; }
    public DeathBringerTeleportState teleportState { get; private set; }

    #endregion

    public bool bossFightBegun;

    [SerializeField] private GameClearManager gameClearManager;

    [Header("Spell cast details")]
    [SerializeField] private GameObject spellPrefab;
    public int amountOfSpells;
    public float spellCooldown;
    public float lastTimeCast;
    [SerializeField] private float spellStateCooldown;
    [SerializeField] private Vector2 spellOffset;

    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceTeleport=25;

    protected override void Awake()
    {
        base.Awake();

        

        idleState = new DeathBingerIdleState(this, stateMachine, "Idle", this);

        battleState = new DeathBringerBattleState(this, stateMachine, "Move", this);
        attackState = new DeathBringerAttackState(this, stateMachine, "Attack", this);

        deadState = new DeathBringerDeadState(this, stateMachine, "Dead", this);

        spellCastState = new DeathBringerSpellCastState(this, stateMachine, "SpellCast", this);
        teleportState = new DeathBringerTeleportState(this, stateMachine, "Teleport", this);
    }

    // idleèÛë‘Ç÷
    public override void ResetToIdleStateBoss()
    {
        stateMachine.ChangeState(idleState);
    }



    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }


    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }


    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;

        float xOffset = 0;

        if (player.rb.linearVelocity.x != 0)
            xOffset = player.facingDirection * spellOffset.x;

        Vector3 spellPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + spellOffset.y);

        GameObject newSpell = Instantiate(spellPrefab,spellPosition,Quaternion.identity);
        newSpell.GetComponent<DeathBringerSpell_Controller>().SetupSpell(stats);
    }


    public void FindPosition()
    {
        float x=Random.Range(arena.bounds.min.x+0.2f,arena.bounds.max.x-0.2f);
        float y= Random.Range(arena.bounds.min.y + 0.2f, arena.bounds.max.y - 0.2f);

        transform.position = new Vector3(x,y);
        transform.position=new Vector3(transform.position.x,transform.position.y-GroundBelow().distance+(cd.size.y/2));

        if(!GroundBelow() || SomethingIsArround())
        {
            Debug.Log("looking for new positioin");
            FindPosition();
        }
    }


    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down,100,whatIsGround);
    private bool SomethingIsArround() => Physics2D.BoxCast(transform.position,surroundingCheckSize,0,Vector2.zero,0,whatIsGround,whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position,surroundingCheckSize);
    }


    public bool CanTeleport()
    {
        if(Random.Range(0,100) <= chanceToTeleport)
        {
            chanceToTeleport=defaultChanceTeleport;
            return true;
        }

        return false;
    }



    public bool CanDoSpellCast()
    {
        if (Time.time >= lastTimeCast+spellStateCooldown)
        {
            
            return true;
        }

        return false;
    }


    //í«â¡ÇµÇƒÇ›ÇΩÇ‚Ç¬
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);

        //gameclearUIÇï\é¶
        gameClearManager.GameClearUI_Start();
    }

}
