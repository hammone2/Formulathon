using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public void ChooseSkin(string color)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.name == color)
                child.SetActive(true);
            else
                child.SetActive(false);
        }
    }
}
