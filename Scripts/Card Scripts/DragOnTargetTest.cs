using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DragOnTargetTest : DraggingActionsTest
{
    public TargetingOptions Targets = TargetingOptions.AllCharacters;
    private SpriteRenderer targetRenderer;
    private LineRenderer lineRenderer;
    private Transform triangle;
    private SpriteRenderer triangleRenderer;
    private GameObject Target;

    public GameObject card;
    CardAsset ca;
    public int spellDamage;

    TargetingOptions targets;
    ChosenEffect chosenEffect;

    private GameObject deckObject;
    Deck deck;

    void Awake()
    {
        targetRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.sortingLayerName = "VeryFront";
        triangle = transform.Find("PointerTriangle");
        triangleRenderer = triangle.GetComponent<SpriteRenderer>();

        deckObject = GameObject.Find("Deck");
        deck = deckObject.GetComponent<Deck>();
    }

    private void Start()
    {
        ca = card.GetComponent<OneCardManager>().cardAsset;
         
        spellDamage = ca.specialSpellAmount;
        targets = ca.Targets;
        /*string effect = ca.SpellEffectName.ToString();
        Debug.Log(effect);
        if (card != null)
        {
            Debug.Log("yes");
                }
        else
        {
            Debug.Log("no");
        }*/
        chosenEffect = ca.TargetEffect;

    }

    public override void OnStartDrag()
    {
        targetRenderer.enabled = true;
        lineRenderer.enabled = true;
    }

    public override void OnDraggingInUpdate()
    {
        // This code only draws the arrow
        Vector3 notNormalized = transform.position - transform.parent.position;
        Vector3 direction = notNormalized.normalized;
        float distanceToTarget = (direction*2.3f).magnitude;
        if (notNormalized.magnitude > distanceToTarget)
        {
            // draw a line between the creature and the target
            lineRenderer.SetPositions(new Vector3[]{ transform.parent.position, transform.position - direction*2.3f });
            lineRenderer.enabled = true;

            // position the end of the arrow between near the target.
            triangleRenderer.enabled = true;
            triangleRenderer.transform.position = transform.position - 1.5f*direction;

            // proper rotarion of arrow end
            float rot_z = Mathf.Atan2(notNormalized.y, notNormalized.x) * Mathf.Rad2Deg;
            triangleRenderer.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else
        {
            // if the target is not far enough from creature, do not show the arrow
            lineRenderer.enabled = false;
            triangleRenderer.enabled = false;
        }

    }

    public override void OnEndDrag()
    {
        
        Target = null;
        RaycastHit hitter;
        if (Physics.Raycast(this.transform.position, Vector3.forward * 100f, out hitter))
        {
            
            if (targets == TargetingOptions.EnemyCharacters)
            {
                if (hitter.transform.tag == "Ally")
                {
                    transform.localPosition = new Vector3(0f, 0f, 0.4f);
                    targetRenderer.enabled = false;
                    lineRenderer.enabled = false;
                    triangleRenderer.enabled = false;
                }
                
                else if (hitter.transform.tag == "Enemy")
                {
                    //Debug.Log("i am an enemy");
                    //Debug.Log(Target.transform.name);
                    Target = hitter.transform.gameObject;

                    switch (chosenEffect)
                    {
                        case ChosenEffect.TakeDamage:
                            Target.GetComponent<PlayableCharacter>().TakeDamage(spellDamage);
                            break;
                        case ChosenEffect.AtkIncrease:
                            Target.GetComponent<PlayableCharacter>().IncreaseAttack(spellDamage);
                            break;
                        case ChosenEffect.AtkDecrease:
                            Target.GetComponent<PlayableCharacter>().DecreaseAttack(spellDamage);
                            break;
                        case ChosenEffect.HealHp:
                            Target.GetComponent<PlayableCharacter>().HealDamage(spellDamage);
                            break;
                    }

                    deck.slotNumber--;
                    Destroy(transform.parent.gameObject);
                }

                else
                {
                    transform.localPosition = new Vector3(0f, 0f, 0.4f);
                    targetRenderer.enabled = false;
                    lineRenderer.enabled = false;
                    triangleRenderer.enabled = false;
                }

            }

            else if (targets == TargetingOptions.YourCharacters)
            {
                if (hitter.transform.tag == "Ally")
                {
                    //Debug.Log("i am an ally");
                    //Debug.Log(Target.transform.name);
                    Target = hitter.transform.gameObject;

                    switch (chosenEffect)
                    {
                        case ChosenEffect.TakeDamage:
                            Target.GetComponent<PlayableCharacter>().TakeDamage(spellDamage);
                            break;
                        case ChosenEffect.AtkIncrease:
                            Target.GetComponent<PlayableCharacter>().IncreaseAttack(spellDamage);
                            break;
                        case ChosenEffect.AtkDecrease:
                            Target.GetComponent<PlayableCharacter>().DecreaseAttack(spellDamage);
                            break;
                        case ChosenEffect.HealHp:
                            Target.GetComponent<PlayableCharacter>().HealDamage(spellDamage);
                            break;
                    }

                    deck.slotNumber--;
                    Destroy(transform.parent.gameObject);
                }

                else if (hitter.transform.tag == "Enemy")
                {
                    transform.localPosition = new Vector3(0f, 0f, 0.4f);
                    targetRenderer.enabled = false;
                    lineRenderer.enabled = false;
                    triangleRenderer.enabled = false;
                }

                else
                {
                    transform.localPosition = new Vector3(0f, 0f, 0.4f);
                    targetRenderer.enabled = false;
                    lineRenderer.enabled = false;
                    triangleRenderer.enabled = false;
                }
            }
        }
        
        // return target and arrow to original position
        // this position is special for spell cards to show the arrow on top


    }
    /*
    private void Update()
    {
        Debug.DrawRay(this.transform.position, Vector3.forward * 100f, Color.yellow);
    }*/

    // NOT USED IN THIS SCRIPT
    protected override bool DragSuccessful()
    {
        return true;
    }
}
