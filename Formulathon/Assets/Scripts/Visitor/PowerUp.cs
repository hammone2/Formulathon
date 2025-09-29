using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject, IVisitor
{
    public string powerupName;
    public string powerupDescription;
    

    [Tooltip("Invincibility Shield")]
    public bool invincibilityShield;
    public float shieldDuration = 3f; 

    [Tooltip("Bonus Life")]
    public bool bonusLife;


    public void Visit(InvincibilityShield _invincibilityShield)
    {
        if (invincibilityShield)
        {
            _invincibilityShield.Activate(shieldDuration);
            Debug.Log("Invincibility");
        }
    }

    public void Visit(BonusLife _bonusLife)
    {
        if (bonusLife)
        {
            _bonusLife.AddLife();
            Debug.Log("Bonus Life");
        }
    }
}
