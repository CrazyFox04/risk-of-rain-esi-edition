using UnityEngine;

public class AllReloadActions : MonoBehaviour
{
    private void Start()
    {
        ReloadActions[] reloadActions = GetComponentsInChildren<ReloadActions>();
        reloadActions[2].setAttackIndex(2);
        reloadActions[3].setAttackIndex(1);
        reloadActions[4].setAttackIndex(0);
    }
}