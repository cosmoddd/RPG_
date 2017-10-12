using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon, IProjectileWeapon {


    private Animator animator;

    public List<BaseStat> Stats { get; set; }

    public Transform ProjectileSpawn { get; set; }

    public Fireball fireball;


    void Start()
    {
        fireball = Resources.Load<Fireball>("Weapons/Projectiles/Fireball");
        animator = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
        animator.SetTrigger("Base_Attack");
    }

    public void PerformSpecialAttack()
    {

    }

    public void CastProjectile()
    {
        Fireball fireBallInstance = Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation) as Fireball;
        fireBallInstance.Direction = ProjectileSpawn.forward;
    }
}
