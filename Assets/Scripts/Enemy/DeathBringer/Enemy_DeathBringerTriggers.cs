using UnityEngine;
using UnityEngine.UI;

public class Enemy_DeathBringerTriggers : Enemy_AnimationTriggers
{

    private Enemy_DeathBringer enemyDeathBringer =>GetComponentInParent<Enemy_DeathBringer>();

    private void Relocate() => enemyDeathBringer.FindPosition();

    public void MakeInvisible() => enemyDeathBringer.fx.MakeTransparent(true);
    public void MakeVisible() => enemyDeathBringer.fx.MakeTransparent(false);




}
