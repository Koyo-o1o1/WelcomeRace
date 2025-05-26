using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // キャラに使うもの
    #region Component


    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Entity_FX fx { get; private set; }
    public Character_Stats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }


    #endregion

    //ノックバック処理に用いる変数
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    //衝突判定に用いる変数
    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallcheckDistance;
    [SerializeField] protected LayerMask whatIsGround;


    //向きの制御
    public int facingDirection { get; private set; } = 1;
    protected bool facingRight = true;


    //HPバーの反転
    public System.Action onFlipped; 

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<Entity_FX>();
        stats = GetComponent<Character_Stats>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    protected virtual void Update()
    {

    }

    //攻撃を受けた際の処理
    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockBack");
    }

    //ノックバックを生じさせる処理
    public virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        rb.linearVelocity = new Vector2(knockbackDirection.x * -facingDirection, knockbackDirection.y * -facingDirection);

        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    #region Velocity

    //速度を0にする関数
    public void SetZeroVelocity()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }

    //速度を与える関数
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {

        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);

    }

    #endregion


    #region Collision

    //床と衝突しているか返す関数
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    //壁と衝突しているか返す関数
    public virtual bool isWallDetcted() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallcheckDistance, whatIsGround);

    //線を引く関数
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallcheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion


    #region Flip

    //反転させる関数
    public void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, -180, 0);

        //UIバー制御
        if (onFlipped != null)
            onFlipped();
    }

    //反転を制御する関数
    public void FlipController(float _x)
    {
        //右へ移動したいが左向き
        if (_x > 0 && !facingRight)
            Flip();
        //左へ移動したいが右向き
        else if (_x < 0 && facingRight)
            Flip();
    }
    #endregion


    public virtual void Die()
    {

    }


    //右向きに戻す関数
    public void FaceRight()
    {
        if (!facingRight)
            Flip();
    }
}
