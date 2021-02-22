using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public AudioSource manilaMusic;
    public AudioSource barkerAudio;

    public GameObject titleText;
    public GameObject introScreen;
    public GameObject daddyScreen;
    public GameObject barkerScreen;
    public GameObject bayadScreen;

    public GameObject jeepneyModel;

    int uiState;
    bool rotateOn = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch(uiState)
        {
        	case 0:
        		ShowIntroText();
        		break;
        	case 1:
        		break;
        }
        RotateJeepney();
    }

    void ShowIntroText(){
    	if(!manilaMusic.isPlaying)
    	{
    		titleText.SetActive(false);
    		introScreen.SetActive(true);
    		uiState = 1;
    	}
    }

    public void GetStarted(){
    	// Hide and show new UI elems
    	introScreen.SetActive(false);
    	daddyScreen.SetActive(true);
    	rotateOn = false;
    }

   	public void DaddyPress(){
   		daddyScreen.SetActive(false);
   		barkerScreen.SetActive(true);
   		barkerAudio.Play();
   	}

   	public void BarkerPress(){
   		barkerAudio.Stop();
   		barkerScreen.SetActive(false);
   		bayadScreen.SetActive(true);
   	}

    void RotateJeepney(){
    	if(rotateOn){
	    	jeepneyModel.transform.Rotate(0f,0.1f,0f);
    	}    
    }

}
