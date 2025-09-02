using UnityEngine;

public class Menu : MonoBehaviour
{ 

    [SerializeField] private SkinManager showCarSkinManager;
    [SerializeField] private SkinManager playerSkinManager;
    [SerializeField] private Lives livesIcon;

    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject HUD;

    [SerializeField] private AudioSource selectSFX;
    [SerializeField] private AudioSource startSFX;
    [SerializeField] private AudioSource menuMusic;

    public void StartGame()
    {
        titleScreen.SetActive(false);
        //HUD.SetActive(true);
        GameManager.instance.playerCam.state = FollowPlayer.CameraState.Cinematic;

        menuMusic.Stop();
        startSFX.Play();
    }

    public void ChooseColor(string color)
    {
        showCarSkinManager.ChooseSkin(color);
        playerSkinManager.ChooseSkin(color);
        livesIcon.SelectColor(color);
        //change life icon to match skin

        selectSFX.Play();
    }
}
