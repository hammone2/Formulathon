using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    [SerializeField] Sprite red;
    [SerializeField] Sprite blue;
    [SerializeField] Sprite grey;
    [SerializeField] Sprite green;
    [SerializeField] Sprite white;
    [SerializeField] Sprite black;
    [SerializeField] Sprite yellow;
    [SerializeField] Sprite orange;
    [SerializeField] Sprite purple;
    [SerializeField] Sprite pink;

    public void SelectColor(string color)
    {
        switch (color) {
            case "Red":
                SetColor(red);
                break;

            case "Blue":
                SetColor(blue);
                break;

            case "Grey":
                SetColor(grey);
                break;

            case "Green":
                SetColor(green);
                break;

            case "White":
                SetColor(white);
                break;

            case "Black":
                SetColor(black);
                break;

            case "Yellow":
                SetColor(yellow);
                break;

            case "Orange":
                SetColor(orange);
                break;

            case "Purple":
                SetColor(purple);
                break;

            case "Pink":
                SetColor(pink);
                break;

        }
    }

    private void SetColor(Sprite icon)
    {
        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = icon;    
        }
    }

    public void UpdateLivesCounter()
    {
        GameObject lastChildObj = transform.GetChild(transform.childCount - 1).gameObject;
        Destroy(lastChildObj.gameObject);
    }
}
