using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : Entity
{
    private EnemyState enemyState;

    //プレイヤーを検出するレイヤーを定義
    [SerializeField] protected LayerMask whatIsPlayer;


    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    [Header("Attack info")]
    public float attacckDistance;
    public float attackY;
    public float attackCooldown;
    [SerializeField] public float lastTimeAttack;


    //continue時に復活させるための変数
    // 初期位置とHPを保存
    private Vector3 initialPosition;
    private int initialHealth;


    public EnemyStateMachine stateMachine { get; private set; }
    public string lastAnimBoolName { get; private set; }


    protected override void Awake()
    {
        stateMachine = new EnemyStateMachine();

        // 初期位置とHPを保存
        initialPosition = transform.position;
        initialHealth = GetComponent<Character_Stats>().GetMaxHealthValue();

    }

    //敵をリセットする関数
    public void ResetEnemy()
    {
        // 位置を初期位置に戻し、HPを最大値に設定
        transform.position = initialPosition;
        var stats = GetComponent<Character_Stats>();
        stats.currentHealth = stats.GetMaxHealthValue();

        // HPバーを更新
        stats.onHealthChanged?.Invoke();
    }


    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }


    public virtual void AssignLastAnimBoolName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }


    //アニメーションをストップ
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    //プレイヤー検出
    public virtual RaycastHit2D IsplayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, 10, whatIsPlayer);

    //線を引く関数
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x+attacckDistance*facingDirection,transform.position.y-attackY,transform.position.z));
    }


    //敵をリセット
    // 敵をリセットする処理
    public void ResetEnemies()
    {
        // "Enemy" タグのついた全ての敵をリセット
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemyObject in enemies)
        {
            var enemy = enemyObject.GetComponent<Enemy>();
            enemy.ResetEnemy();
            //idle状態に
            enemy.ResetToIdleState();
            enemy.ResetToIdleStateBoss();
        }
    }

    //敵をidle状態にする
    public virtual void ResetToIdleState()
    {
        // 派生クラスでオーバーライドしてidleStateに戻す
    }

    //敵をidle状態にする
    public virtual void ResetToIdleStateBoss()
    {
        // 派生クラスでオーバーライドしてidleStateに戻す
    }

}
