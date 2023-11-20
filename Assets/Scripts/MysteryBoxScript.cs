using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MysteryBoxScript : MonoBehaviour
{
    private KeyCode interactKey = KeyCode.R;
    private Animator mysteryBoxAnim;
    private bool isInteracted = false;
    private bool isOpened = false;
    private int mysteryRoll;
    private AudioSource mysteryBoxOpenAudio;

    public GameObject weapon;
    public GameObject soulPotion;
    public GameObject techPart;
    public GameObject skillPoint;
    // Start is called before the first frame update
    void Start()
    {
        mysteryBoxAnim = GetComponentInChildren<Animator>();
        mysteryBoxOpenAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteracted && !isOpened)
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
                    Instantiate(weapon, pos + new Vector3(0, 1, 0), rot, parent);
                    break;

                case 1: //soul refill potion
                    Instantiate(soulPotion, pos, rot, parent);
                    break;

                case 2: //3 tech weapon parts
                    float k = MathF.PI*2/3;
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 newPos = pos + MathF.Cos(i*k)*Vector3.right + MathF.Sin(i*k)*Vector3.forward;
                        Instantiate(techPart, newPos, rot, parent);
                    }
                    break;

                case 3: //skill point
                    Instantiate(skillPoint, pos, rot, parent);
                    break;
            }  
               
        }

    
        if (mysteryBoxAnim.GetCurrentAnimatorStateInfo(0).IsName("MysteryBoxOpened")) {
            transform.Find("Bottom").gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }

    void OnTriggerStay(Collider other)
    { 
        if (other.CompareTag("Player") && Input.GetKey(interactKey) && !isInteracted) //player uses interact key in range of mystery box
        {
            isInteracted = true;       
        }
    }

    public void playOpeningSound()
    {
        mysteryBoxOpenAudio.Play();
    }
}
