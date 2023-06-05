using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    private static ManageScene _instance;
    void Awake(){
        if(_instance == null){
            _instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene("temp level");
        }
    }
}
