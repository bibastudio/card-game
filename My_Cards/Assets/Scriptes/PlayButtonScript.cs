using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour {

    public Button b;

    void Start()
    {
        b.onClick.AddListener(delegate () { SceneManager.LoadScene("Game"); });
    }

   


}
