using System.Security.Cryptography;
using UnityEngine;

public class DeathBringerSpell_Controller : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask whatIsPlayer;

    private Character_Stats myStats;

    public void SetupSpell(Character_Stats _stats) => myStats = _stats;


    private void AnimationTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(check.position, boxSize, whatIsPlayer);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Entity>();
                myStats.DoDamage(hit.GetComponent<Character_Stats>());
            }
        }
    }

    private void OnDrawGizmos() => Gizmos.DrawWireCube(check.position,boxSize);

    private void SelfDestoroy() => Destroy(gameObject);
}
