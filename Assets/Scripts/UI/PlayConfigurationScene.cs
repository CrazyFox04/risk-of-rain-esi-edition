using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayConfiguration : MenuScene
{
    [SerializeField] private string initializerScene;
    [SerializeField] private Dropdown lightAttackDpd;
    [SerializeField] private Dropdown strongAttackDpd;

    public PlayerConfig playerConfig;

    
    [SerializeField] private List<string> lightAttacks = new List<string>() {"Light Distance", "Light Short"}; // 2 et 4
    [SerializeField] private List<string> strongAttacks = new List<string>() {"Strong Distance", "Strong Short"}; // 3 et 5

    private int LIGHT = 0;
    private int STRONG = 1;

    void Start() 
    {
        InitializeAttackDropDown(lightAttackDpd, 0);
        InitializeAttackDropDown(strongAttackDpd, 1);
    }

    void InitializeAttackDropDown(Dropdown attackDpd, int type) {
        //attackDpd.ClearOptions();

        if (type == LIGHT) {
            attackDpd.AddOptions(lightAttacks);
        }

        if (type == STRONG) {
            attackDpd.AddOptions(strongAttacks);
        }

        attackDpd.value = 0;
        attackDpd.RefreshShownValue();
    }
    
    public void PlayGame()
    {
        // save and play
        playerConfig.attack1 = 0;
        playerConfig.attack2 = (lightAttackDpd.value == 0) ? 1 : 3;
        playerConfig.attack3 = (strongAttackDpd.value == 0) ? 2 : 4;

        SceneLoader.LoadSceneIfExists(initializerScene);
    }

}
