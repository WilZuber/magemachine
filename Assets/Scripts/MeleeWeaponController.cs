using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    public Animator anim;
    public GameObject weapon;
    private bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canAttack = true;
    }

    //Start attack process
    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            anim.SetBool("MeleeAtk", true); //this parm must be consistentently n
            Invoke(nameof(Activate), 0.25f);
        }
    }

    //Activate weapon after reaching start of swing
    private void Activate()
    {
        weapon.SetActive(true);
        Invoke(nameof(Deactivate), 0.5f);
        anim.SetBool("MeleeAtk", false);
    }

    //Deactivate weapon at end of swing
    private void Deactivate()
    {
        weapon.SetActive(false);
        canAttack = true;
    }
}
