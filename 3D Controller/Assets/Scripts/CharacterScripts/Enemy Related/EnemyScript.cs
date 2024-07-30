using UnityEngine;

public class EnemyScript : CharacterScript
{
    [SerializeField] private EnemyStateMachineBase[] EnemyStateMachines;
    [SerializeField] private int enemyID;

    private Vector3 SpawnPosition;

    public int EnemyID => enemyID;

    protected override void Start()
    {
        base.Start();
        EnemyStateMachines = GetComponents<EnemyStateMachineBase>();
        SpawnPosition = transform.position;
    }

    public override void Respawn()
    {
        transform.position = SpawnPosition;
        Collider.enabled = true;

        EnemyStateMachines[0].enabled = true;
        Animator.SetTrigger("Respawn");

        HealthScript.ResetHealth();
        HealthScript.isAlive = true;
    }

    public override void GetDamage(float _damage)
    {
        if(AudioManager.instance != null)
        {
        AudioManager.instance.SFX[10].source.pitch = 1.84f;
        }
        else { Debug.Log("Enemy tried to play Get Hit Sound Effect, but couldnt find Audio Manager in Scene"); }
        base.GetDamage(_damage);
        
    }
    public override void Die()
    {
        if (AudioManager.instance != null)
        {
        AudioManager.instance.SFX[10].source.pitch = 1.84f;
        }
        else { Debug.Log("Enemy tried to play Get Hit Sound Effect, but couldnt find Audio Manager in Scene"); }
        base.Die();
        SoulsSystem.instance.GainSouls(Stats.SoulsValue);
        Collider.enabled = false;
        //drop Item
        foreach (var enemyStateMachine in EnemyStateMachines)
        {
            enemyStateMachine.enabled = false;
        }
    }
}
