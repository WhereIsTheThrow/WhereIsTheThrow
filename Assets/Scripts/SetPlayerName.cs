using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerName : MonoBehaviour
{
    public Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "Baseman")
        {
            nameText.text = this.gameObject.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
