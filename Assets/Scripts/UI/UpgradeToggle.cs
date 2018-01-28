using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeToggle : MonoBehaviour {

    public GameObject UpgradeScreen;
    public GameObject UpgradeScreen2;

	void Update () {
        if (Input.GetButton("R1 Toggle P1"))
        {
            UpgradeScreen.SetActive(true);
        }
        else
        {
            UpgradeScreen.SetActive(false);
        }

        if (Input.GetButton("R1 Toggle P2"))
        {
            UpgradeScreen2.SetActive(true);
        }
        else
        {
            UpgradeScreen2.SetActive(false);
        }
    }
}
