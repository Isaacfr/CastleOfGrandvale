using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TargetingOptions
{
    NoTarget,
    AllCharacters, 
    EnemyCharacters,
    YourCharacters
}

public enum NumberOfTargets
{
    Single,
    Multiple
}

public enum ChosenEffect
{
    AtkIncrease,
    DefIncrease,
    HealHp,
    TakeDamage,
    AtkDecrease,
    DefDecrease
}

public enum CharacterType
{
    Warrior,
    Mage,
    Priest
}

public class CardAsset : ScriptableObject 
{
    // this object will hold the info about the most general card
    [Header("General info")]
    //public CharacterAsset characterAsset;  // if this is null, it`s a neutral card
    //[TextArea(2,3)]
    public ChosenEffect TargetEffect;
    public CharacterType Characters;
    public string Description;  // Description for spell or character
	public Sprite CardImage;
    public int ManaCost;
    
    //going to need characterType string in order to change the string


    [Header("SpellInfo")]
    /*
    public string SpellScriptName;
    public string SpellEffectName;
    */
    public int specialSpellAmount;
    public TargetingOptions Targets;

    public NumberOfTargets HowManyTargets;

}
