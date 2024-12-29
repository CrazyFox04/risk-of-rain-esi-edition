using UnityEngine;

public class AllReloadActions : MonoBehaviour
{
    private void Start()
    {
        ReloadActions[] reloadActions = GetComponentsInChildren<ReloadActions>();
        reloadActions[0].setIndex(3);
        reloadActions[0].setMovementType(true);
        reloadActions[1].setIndex(2);
        reloadActions[1].setMovementType(true);
        reloadActions[2].setIndex(2);
        reloadActions[3].setIndex(1);
        reloadActions[4].setIndex(0);
    }
}