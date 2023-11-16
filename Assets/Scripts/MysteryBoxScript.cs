using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MysteryBoxScript : MonoBehaviour
{
    private KeyCode interactKey = KeyCode.R;
    private Animator mysteryBoxAnim;
    private bool isOpen = false;
    private bool isOpened = false;
    private int mysteryRoll;

    public GameObject weapon;
    public GameObject soulPotion;
    public GameObject techPart;
    public GameObject skillPoint;
    // Start is called before the first frame update
    void Start()
    {
        mysteryBoxAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen && !isOpened)
        {
            isOpened = true; //prevents looping
            
            mysteryBoxAnim.SetBool("isOpen", true); //open animation
            mysteryRoll = UnityEngine.Random.Range(0, 3); //roll for rewards
            Vector3 pos = transform.position;
            Quaternion rot = Quaternion.identity;
            Transform parent = GetComponent<Transform>().parent;
            switch (mysteryRoll)
            {
                case 0: //weapon
                    Instantiate(weapon, pos, rot, parent);
                    break;

                case 1: //soul refill potion
                    Instantiate(soulPotion, pos, rot, parent);
                    break;

                case 2: //3 tech weapon parts
                    Instantiate(techPart, pos, rot, parent);
                    Instantiate(techPart, pos, rot, parent);
                    Instantiate(techPart, pos, rot, parent);
                    break;

                case 3: //skill point
                    Instantiate(skillPoint, pos, rot, parent);
                    break;
            }  

            Destroy(GetComponent<Collider>());
               
        }

    
        if (mysteryBoxAnim.GetCurrentAnimatorStateInfo(0).IsName("MysteryBoxOpened"))
            transform.Find("Bottom").gameObject.SetActive(false);

    }

    void OnTriggerStay(Collider other)
    { 
        if (other.CompareTag("Player") && Input.GetKey(interactKey) && !isOpen) //player uses interact key in range of mystery box
        {
            isOpen = true;       
        }
    }
}
