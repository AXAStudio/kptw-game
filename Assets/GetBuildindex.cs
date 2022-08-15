using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetBuildindex : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public Launcher launcher;

    void Update()
    {
        launcher.nextscene = dropdown.value + 1;
    }


}
