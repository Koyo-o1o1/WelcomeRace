using System.Xml.Serialization;
using UnityEngine;

public class Enemy_AnimationTriggers : MonoBehaviour
{
    [SerializeField] private Player player;
    private Enemy enemy => GetComponentInParent<Enemy>();

    [Header("BGM Settings")]
    public AudioSource attackbgmSource;
    public AudioClip attackbgmClip;

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    //�U���������������̏���
    private void AttackTrigger()
    {
        if (attackbgmSource != null && attackbgmClip != null)
        {

            attackbgmSource.volume = 0.3f;
            attackbgmSource.clip = attackbgmClip;
            attackbgmSource.Play();
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position,enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats _target = hit.GetComponent<PlayerStats>();

                enemy.stats.DoDamage(_target);

                
            }
        }
    }


    //�U�����[�V�������Ƀv���C���[��enemy�̌��ɂ����甽�]
    private void FlipTrigger()
    {
        if (player == null || enemy == null)
            return;

        float playerPosX = player.transform.position.x;
        float enemyPosX = enemy.transform.position.x;

        if ((enemy.facingDirection == 1 && playerPosX < enemyPosX) || (enemy.facingDirection == -1 && playerPosX > enemyPosX))
        {
            //�����Ɍ��ɂ��邩���肵�āA�����甽�]
            enemy.Flip();
        }
    }
}
