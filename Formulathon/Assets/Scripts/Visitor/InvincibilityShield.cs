using UnityEngine;

public class InvincibilityShield : MonoBehaviour, ICarElement
{

    private GameObject shield;
    private string prefabPath = "Prefabs/InvincibilityShield";

    private void Start()
    {
        GameObject shieldInstance = Resources.Load<GameObject>(prefabPath);

        shield = Instantiate(shieldInstance, transform);
        shield.SetActive(false);
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Activate(float duration)
    {
        shield.SetActive(true);
        Invoke("Deactivate", duration);
    }

    private void Deactivate()
    {
        shield.SetActive(false);
    }
}
