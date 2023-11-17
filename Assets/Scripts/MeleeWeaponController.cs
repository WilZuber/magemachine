using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    private Animator anim;
    public GameObject weapon;
    private bool canAttack;
    private KeyCode meleeKey = KeyCode.F;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canAttack = true;
    }

    void Update()
    {
        if (canAttack && Input.GetKey(meleeKey))
        {
            Attack();
        }
    }
    //Start attack process
    public void Attack()
    {
        canAttack = false;
        Invoke(nameof(Activate), 0.25f);
    }

    //Activate weapon after reaching start of swing
    private void Activate()
    {
        weapon.SetActive(true);
        anim.SetBool("MeleeAtk", true);
        Invoke(nameof(Deactivate), 0.5f);
    }

    //Deactivate weapon at end of swing
    private void Deactivate()
    {
        anim.SetBool("MeleeAtk", false);
        weapon.SetActive(false);
        canAttack = true;
    }
}
