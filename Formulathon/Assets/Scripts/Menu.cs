using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 

    [SerializeField] private SkinManager showCarSkinManager;
    [SerializeField] private SkinManager playerSkinManager;

    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject HUD;

    public void StartGame()
    {
        titleScreen.SetActive(false);
        HUD.SetActive(true);
        GameManager.instance.playerCam.state = FollowPlayer.CameraState.Cinematic;
    }

    public void ChooseColor(string color)
    {
        showCarSkinManager.ChooseSkin(color);
        playerSkinManager.ChooseSkin(color);
        //change life icon to match skin
    }
}
