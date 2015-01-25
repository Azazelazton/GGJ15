using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    InputField input;

    public void LoadLevelOne()
    {
        PlayerPrefs.SetString("Version", input.text);
        Application.LoadLevel("Level 0");
    }
}
