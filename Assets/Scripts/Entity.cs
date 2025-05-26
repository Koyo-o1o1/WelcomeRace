using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // �L�����Ɏg������
    #region Component


    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Entity_FX fx { get; private set; }
    public Character_Stats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }


    #endregion

    //�m�b�N�o�b�N�����ɗp����ϐ�
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    //�Փ˔���ɗp����ϐ�
    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallcheckDistance;
    [SerializeField] protected LayerMask whatIsGround;


    //�����̐���
    public int facingDirection { get; private set; } = 1;
    protected bool facingRight = true;


    //HP�o�[�̔��]
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

    //�U�����󂯂��ۂ̏���
    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockBack");
    }

    //�m�b�N�o�b�N�𐶂������鏈��
    public virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        rb.linearVelocity = new Vector2(knockbackDirection.x * -facingDirection, knockbackDirection.y * -facingDirection);

        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    #region Velocity

    //���x��0�ɂ���֐�
    public void SetZeroVelocity()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }

    //���x��^����֐�
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {

        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);

    }

    #endregion


    #region Collision

    //���ƏՓ˂��Ă��邩�Ԃ��֐�
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    //�ǂƏՓ˂��Ă��邩�Ԃ��֐�
    public virtual bool isWallDetcted() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallcheckDistance, whatIsGround);

    //���������֐�
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallcheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion


    #region Flip

    //���]������֐�
    public void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, -180, 0);

        //UI�o�[����
        if (onFlipped != null)
            onFlipped();
    }

    //���]�𐧌䂷��֐�
    public void FlipController(float _x)
    {
        //�E�ֈړ���������������
        if (_x > 0 && !facingRight)
            Flip();
        //���ֈړ����������E����
        else if (_x < 0 && facingRight)
            Flip();
    }
    #endregion


    public virtual void Die()
    {

    }


    //�E�����ɖ߂��֐�
    public void FaceRight()
    {
        if (!facingRight)
            Flip();
    }
}
