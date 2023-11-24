using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    private Animator anim;
    public GameObject weapon;
    private bool canAttack;
    private float holdingLeftGun;
    private float holdingRightGun;

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
            // ^wtf
            Invoke(nameof(Activate), 0.25f);
            holdingLeftGun = anim.GetLayerWeight(2);
            holdingRightGun = anim.GetLayerWeight(3);
            anim.SetLayerWeight(2, 0.0f);
            anim.SetLayerWeight(3, 0.0f);
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
        anim.SetLayerWeight(2, holdingLeftGun);
        anim.SetLayerWeight(3, holdingRightGun);
    }
}
