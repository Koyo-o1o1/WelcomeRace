using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : Character_Stats
{
    private Player player;

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        player.DamageEffect();
    }

    protected override void Die()
    {
        base.Die();

        player.Die();
    }

    //posion
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, GetMaxHealthValue()); // **Å‘å’l‚ğ’´‚¦‚È‚¢‚æ‚¤‚É**
        //Debug.Log("‰ñ•œ: " + amount + " Œ»İ‚ÌHP: " + currentHealth);
        if (onHealthChanged != null) onHealthChanged();
    }
}
