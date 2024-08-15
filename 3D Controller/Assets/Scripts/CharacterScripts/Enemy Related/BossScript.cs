using UnityEngine;

public class BossScript : EnemyScript, IElectrilizable
{
    [SerializeField]private  GameEvent bossDied;
    [SerializeField]private Material electrifiedMaterial;


    public override void GetDamage(float _damage) //Only Difference in this method to Enemy Script is the LifeCanvas. Looks like something fixworthy.
    {
        if (!HealthScript.isAlive)
        { return; }

        float defMultiplier = (_damage / 100) * (Stats.Defense * 3f);
        HealthScript.currentHealth -= (_damage - defMultiplier);
        HealthScript.UpdateHealthBar();

        Debug.Log($"{gameObject.name} got {_damage - defMultiplier} Damage ({_damage} - {defMultiplier})");

        Animator.SetTrigger("Stagger");
        if (HealthScript.currentHealth <= 0)
        { Die(); }

        PlaySFXSound("Get Hit");
    }

    public override void Die()
    {
        base.Die();
        bossDied.Raise();
    }

    public void Electrify()
    {

        var SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (SkinnedMeshRenderer.materials.Length <= 1) // Indicator if the the Second or third Material, which is the Electrify Material, already has been added or not
        {
            var Condition = SkinnedMeshRenderer.gameObject.AddComponent<EffectCondition_Lightning>();

            SkinnedMeshRenderer.materials = new Material[] { Condition.OriginalMaterial[0], electrifiedMaterial };
        }
        else
        {
            var lightningCondition = SkinnedMeshRenderer.gameObject.GetComponent<EffectCondition_Lightning>();
            lightningCondition.duration = lightningCondition.maxduration;
            Debug.Log(gameObject.name + "has already been electrified");
        }
    }

    public void Electrify(Vector3 _hitpoint)
    {
        Debug.Log("Call of Electrified Method used a Vector3 as Input, which is not intended for this Effect." +
    " Go the Scripit in which the Electrify Method is called and remove the Vector3 Paramater.");
        Electrify();
    }
}
