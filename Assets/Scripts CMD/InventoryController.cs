using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    private Animator animator;

    public PlayerWeaponController playerWeaponController;
    public Item sword;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerWeaponController = GetComponent<PlayerWeaponController>();
        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat(7, "Power", "Your power level"));
        sword = new Item(swordStats, "sword");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            playerWeaponController.EquipWeapon(sword);
        }
    }

}
