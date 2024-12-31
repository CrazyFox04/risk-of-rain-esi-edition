using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayConfiguration : MenuScene
{
    public string initializerScene;
    public Dropdown attack1Dpd;
    public Dropdown attack2Dpd;
    public Dropdown attack3Dpd;

    public PlayerConfig playerConfig;

    private List<string> allAttacks = new List<string>() {"Attack1", "Attack2", "Attack3", "Attack4", "Attack5"};

    void Start() 
    {
        allAttacks = new List<string>(attack1Dpd.options.ConvertAll(option => option.text));

        attack1Dpd.onValueChanged.AddListener(delegate { UpdateDropdownAttacks(); });
        attack2Dpd.onValueChanged.AddListener(delegate { UpdateDropdownAttacks(); });
        attack3Dpd.onValueChanged.AddListener(delegate { UpdateDropdownAttacks(); });
    }

    void UpdateDropdownAttacks() 
    {
        string selectAttack1 = attack1Dpd.options[attack1Dpd.value].text;
        string selectAttack2 = attack1Dpd.options[attack2Dpd.value].text;
        string selectAttack3 = attack1Dpd.options[attack3Dpd.value].text;

        List<string> availableAttacks1 = GetAvailableOptions(new List<string> { selectAttack2, selectAttack3 });
        List<string> availableAttacks2 = GetAvailableOptions(new List<string> { selectAttack1, selectAttack3 });
        List<string> availableAttacks3 = GetAvailableOptions(new List<string> { selectAttack1, selectAttack2 });

        SetDropdownOptions(attack1Dpd, availableAttacks1, selectAttack1);
        SetDropdownOptions(attack2Dpd, availableAttacks2, selectAttack2);
        SetDropdownOptions(attack3Dpd, availableAttacks3, selectAttack3);
    }

    List<string> GetAvailableOptions(List<string> usedOptions)
    {
        return allAttacks.FindAll(option => !usedOptions.Contains(option));
    }

    void SetDropdownOptions(Dropdown attackDpd, List<string> availableAttacks, string currentAttack)
    {
        bool keepCurrentSelection = availableAttacks.Contains(currentAttack);

        attackDpd.ClearOptions();
        attackDpd.AddOptions(availableAttacks);

        if (keepCurrentSelection)
        {
            attackDpd.value = availableAttacks.IndexOf(currentAttack);
        }
        else
        {
            attackDpd.value = 0;
        }

        attackDpd.RefreshShownValue();
    }
    
    public void PlayGame()
    {
        // save and play
        playerConfig.attack1 = 0; //attack1Dpd.value;
        playerConfig.attack2 = 1; //attack2Dpd.value;
        playerConfig.attack3 = 2; //attack3Dpd.value;

        SceneLoader.LoadSceneIfExists(initializerScene);
    }

}
