using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : Entity
{
    private EnemyState enemyState;

    //�v���C���[�����o���郌�C���[���`
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


    //continue���ɕ��������邽�߂̕ϐ�
    // �����ʒu��HP��ۑ�
    private Vector3 initialPosition;
    private int initialHealth;


    public EnemyStateMachine stateMachine { get; private set; }
    public string lastAnimBoolName { get; private set; }


    protected override void Awake()
    {
        stateMachine = new EnemyStateMachine();

        // �����ʒu��HP��ۑ�
        initialPosition = transform.position;
        initialHealth = GetComponent<Character_Stats>().GetMaxHealthValue();

    }

    //�G�����Z�b�g����֐�
    public void ResetEnemy()
    {
        // �ʒu�������ʒu�ɖ߂��AHP���ő�l�ɐݒ�
        transform.position = initialPosition;
        var stats = GetComponent<Character_Stats>();
        stats.currentHealth = stats.GetMaxHealthValue();

        // HP�o�[���X�V
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


    //�A�j���[�V�������X�g�b�v
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    //�v���C���[���o
    public virtual RaycastHit2D IsplayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, 10, whatIsPlayer);

    //���������֐�
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x+attacckDistance*facingDirection,transform.position.y-attackY,transform.position.z));
    }


    //�G�����Z�b�g
    // �G�����Z�b�g���鏈��
    public void ResetEnemies()
    {
        // "Enemy" �^�O�̂����S�Ă̓G�����Z�b�g
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemyObject in enemies)
        {
            var enemy = enemyObject.GetComponent<Enemy>();
            enemy.ResetEnemy();
            //idle��Ԃ�
            enemy.ResetToIdleState();
            enemy.ResetToIdleStateBoss();
        }
    }

    //�G��idle��Ԃɂ���
    public virtual void ResetToIdleState()
    {
        // �h���N���X�ŃI�[�o�[���C�h����idleState�ɖ߂�
    }

    //�G��idle��Ԃɂ���
    public virtual void ResetToIdleStateBoss()
    {
        // �h���N���X�ŃI�[�o�[���C�h����idleState�ɖ߂�
    }

}
