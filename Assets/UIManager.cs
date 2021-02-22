using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	// Audio player game objects
    public AudioSource manilaMusic;
    public AudioSource jeepAudio;
    public AudioSource barkerAudio;
    public AudioSource bayadAudio;

    // UI state game objects
    public GameObject titleText;
    public GameObject introScreen;
    public GameObject daddyScreen;
    public GameObject barkerScreen;
    public GameObject bayadScreen;
    public GameObject endScreen;

    // Object for jeepney model
    public GameObject jeepneyModel;

    // UI state tracker (essentially start screen vs. inner UI screens)
    int uiState = 0;

    // Rotate the jeepney??
    bool rotateOn = true;

    void Start()
    {

    }

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


    // Bunch of UI state switch functions
    void ShowIntroText(){
    	if(!manilaMusic.isPlaying)
    	{
    		titleText.SetActive(false);
    		introScreen.SetActive(true);
    		uiState = 1;
    	}
    }

    public void GetStarted(){
    	introScreen.SetActive(false);
    	daddyScreen.SetActive(true);
    	rotateOn = false;
    	StartCoroutine(MoveJeepneySide());
    	jeepAudio.Play();
    }

   	public void DaddyPress(){
   		daddyScreen.SetActive(false);
   		barkerScreen.SetActive(true);
   		jeepAudio.Stop();
   		barkerAudio.Play();
   	}

   	public void BarkerPress(){
   		barkerScreen.SetActive(false);
   		bayadScreen.SetActive(true);
   		barkerAudio.Stop();
   		bayadAudio.Play();
   	}

   	public void BayadPress(){
   		bayadScreen.SetActive(false);
   		endScreen.SetActive(true);
   		bayadAudio.Stop();
   		rotateOn = true;
   		StartCoroutine(MoveJeepneyCenter());
   	}

   	public void RestartPress(){
   		endScreen.SetActive(false);
   		introScreen.SetActive(true);
   	}

   	// Rotate jeepney during start screen
    void RotateJeepney(){
    	if(rotateOn){
	    	jeepneyModel.transform.Rotate(0f,0.1f,0f);
    	}    
    }

    // The IEnumerator function type allows
    // us to run the movement script as a coroutine.
    public IEnumerator MoveJeepneySide(){
    	// Set total time allowed for movement, + initialize current time spent moving
    	float totalMovementTime = 2f;
	    float currentMovementTime = 0f;

	    // Starting position
	    // We need a starting position because we compute a linear
	    // interpolation between start and end Vector3s.
	    // The rotation doesn't need one because Quaternion.RotateTowards
	    // works with the object's current rotation anyway.
	    Vector3 startingPosition = jeepneyModel.transform.position;
	    Debug.Log(startingPosition);

	    // Ending position and rotation
	    Vector3 destinationPosition = new Vector3(10f,-5.36f, 33.8f);
	    Quaternion destinationRotation = Quaternion.Euler(new Vector3(0f,-126.632f,0f));

	    // Move until in position
	    while (Vector3.Distance(jeepneyModel.transform.position, destinationPosition) > 0f) {
	        currentMovementTime += Time.deltaTime;

	        // Lerping interpolates between start and end position based on
	        // a sort of completion percentage (e.g. time elapsed over the 
	        // length we want the movement to be)
	        jeepneyModel.transform.position = Vector3.Lerp(startingPosition, 
	        	destinationPosition, 
	        	currentMovementTime / totalMovementTime);

	        // Nice fancy rotate function for EZ and clean rotation calculation
	        jeepneyModel.transform.rotation = Quaternion.RotateTowards(jeepneyModel.transform.rotation, 
	        	destinationRotation,
	        	currentMovementTime / totalMovementTime);
	        yield return null;
	    }
    }

    public IEnumerator MoveJeepneyCenter(){
    	// Set total time allowed for movement, + initialize current time spent moving
    	float totalMovementTime = 2f;
	    float currentMovementTime = 0f;

	    // Starting position
	    Vector3 startingPosition = jeepneyModel.transform.position;

	    // Ending position
	    Vector3 destinationPosition = new Vector3(0.8f,-1.4f,27.2f);

	    // Move until in position
	    // This time, we only move the jeepney without rotating it
	    // since it'll be rotating constantly anyway
	    while (Vector3.Distance(jeepneyModel.transform.position, destinationPosition) > 0f) {
	        currentMovementTime += Time.deltaTime;

	        // Lerping interpolates between start and end position based on
	        // a sort of completion percentage (e.g. time elapsed over the 
	        // length we want the movement to be)
	        jeepneyModel.transform.position = Vector3.Lerp(startingPosition, 
	        	destinationPosition, 
	        	currentMovementTime / totalMovementTime);

	        yield return null;
	    }
    }

}
