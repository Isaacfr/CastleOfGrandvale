using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNoTarget : DraggingActions
{
    public GameObject[] GroupOfTargets;
    TargetingOptions targetionOptions;
    ChosenEffect chosenEffect;

    public GameObject card;
    CardAsset ca;
    public int spellDamage;

    private GameObject deckObject;
    Deck deck;

    private void Awake()
    {
        deckObject = GameObject.Find("Deck");
        deck = deckObject.GetComponent<Deck>();
    }
    private void Start()
    {
        ca = card.GetComponent<OneCardManager>().cardAsset;
        spellDamage = ca.specialSpellAmount;

        targetionOptions = ca.Targets;

        if (targetionOptions == TargetingOptions.EnemyCharacters)
        {
            GroupOfTargets = GameObject.FindGameObjectsWithTag("Enemy");
        }
        else if (targetionOptions == TargetingOptions.YourCharacters)
        {
            GroupOfTargets = GameObject.FindGameObjectsWithTag("Ally");

        }
        Debug.Log(GroupOfTargets.Length);

        chosenEffect = ca.TargetEffect;

    }

    private void Update()
    {
        if (targetionOptions == TargetingOptions.EnemyCharacters)
        {
            GroupOfTargets = GameObject.FindGameObjectsWithTag("Enemy");
        }
        else if (targetionOptions == TargetingOptions.YourCharacters)
        {
            GroupOfTargets = GameObject.FindGameObjectsWithTag("Ally");
        }
    }

    public override void OnStartDrag()
    {
        
    }
    public override void OnDraggingInUpdate()
    {
       
    }

    public override void OnEndDrag()
    {
        RaycastHit hitter;
        if (Physics.Raycast(this.transform.position, Vector3.forward * 100f, out hitter))
        {
            if (hitter.transform.tag == "Field" || hitter.transform.tag == "Enemy" || hitter.transform.tag == "Ally")
            {
                foreach (GameObject Targets in GroupOfTargets)
                {
                    if (Targets == null)
                    {
                        Debug.Log("nothing");
                    }
                    switch (chosenEffect)
                    {
                        case ChosenEffect.TakeDamage:
                            Targets.GetComponent<PlayableCharacter>().TakeDamage(spellDamage);
                            break;
                        case ChosenEffect.AtkIncrease:
                            Targets.GetComponent<PlayableCharacter>().IncreaseAttack(spellDamage);
                            break;
                        case ChosenEffect.AtkDecrease:
                            Targets.GetComponent<PlayableCharacter>().DecreaseAttack(spellDamage);
                            break;
                        case ChosenEffect.HealHp:
                            Targets.GetComponent<PlayableCharacter>().HealDamage(spellDamage);
                            break;
                    }

                }
                //add animations/visuals
                deck.slotNumber--;
                
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.localPosition = new Vector3(0f, 0f, 0f);
        }
        
    }

    protected override bool DragSuccessful()
    {
        return true;
    }

}
