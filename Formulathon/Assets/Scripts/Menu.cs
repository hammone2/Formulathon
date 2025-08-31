using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 

    [SerializeField] private SkinManager showCarSkinManager;
    [SerializeField] private SkinManager playerSkinManager;

    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //make UI disappear
    }

    public void ChooseColor(string color)
    {
        showCarSkinManager.ChooseSkin(color);
        playerSkinManager.ChooseSkin(color);
        //change life icon to match skin
    }
}
