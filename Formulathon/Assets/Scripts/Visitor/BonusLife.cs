using UnityEngine;

public class BonusLife : MonoBehaviour, ICarElement
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void AddLife()
    {
        int lives = transform.GetComponent<PlayerController>().lives;
        if (lives > 0 && lives < 3)
            transform.GetComponent<PlayerController>().UpdateLives();
    }
}
