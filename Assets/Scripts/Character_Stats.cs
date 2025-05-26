using UnityEngine;

public class Character_Stats : MonoBehaviour
{
    [Header("Major Stats")]
    public Stat strength; // 1point increase damage by 1 and crit.power by 1%
    public Stat agility; //1 poit increace evsion by 1% and crit.chance by 1%
    public Stat intelgence; //1 point increace magic damage by 1 and magic resistance by 3
    public Stat vitality;


    public Stat damage;
    public Stat maxHealth;

    public int currentHealth;



    public System.Action onHealthChanged;

    protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();
    }

    //対象にダメージを計算し、ダメージを与える処理をする関数
    public virtual void DoDamage(Character_Stats _targetStats)
    {

        int totalDamage = damage.GetValue() + strength.GetValue();

        _targetStats.TakeDamage(totalDamage);
    }

    //実際にダメージを与える関数
    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealthBy(_damage);

        if (currentHealth <= 0)
        {
            Die();
        }


        
    }
    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if(onHealthChanged != null)
        {
            onHealthChanged();
        }
    }

    //死亡時の処理
    protected virtual void Die()
    {
        //
    }


    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }
}
