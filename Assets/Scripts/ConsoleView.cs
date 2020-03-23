using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class ConsoleView : MonoBehaviour {
    ConsoleController console = new ConsoleController();
	
    bool didShow = false;

    public GameObject viewContainer; //Container for console view, should be a child of this GameObject
    public Text logTextArea;
    public InputField inputField;

    private Vector3 swipeFirstPosition;
    private Vector3 swipeLastPosition;
    private float swipeDragDistance;
    private List<Vector3> touchPositions = new List<Vector3>();

    private int currentCommand;
    private int index;

    void Start() {
        if (console != null) {
            console.visibilityChanged += onVisibilityChanged;
            console.logChanged += onLogChanged;
        }
        updateLogStr(console.log);
        swipeDragDistance = Screen.width * 30 / 100;
    }
	
    ~ConsoleView() {
        console.visibilityChanged -= onVisibilityChanged;
        console.logChanged -= onLogChanged;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            runCommand();
        }
        
        //Toggle visibility when tilde key pressed
        if (Input.GetKeyUp("`"))
        {
            toggleVisibility();
        }
        
        //Arrow up in history
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            upCommand();
        }
        
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            downCommand();
        }

        //Toggle visibility when right swipe
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                touchPositions.Add(touch.position);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                swipeFirstPosition = touchPositions[0];
                swipeLastPosition = touchPositions[touchPositions.Count - 1];

                //if swipeDragDistance > 30% from screen edge
                if (Mathf.Abs(swipeLastPosition.x - swipeFirstPosition.x) > swipeDragDistance)
                {
                    if ((swipeLastPosition.x < swipeFirstPosition.x)) 
                    {
                        toggleVisibility();
                    }
                    else
                    {
                        toggleVisibility();
                    }
                }
            }
        }
        
        //Visibility on mouse swipe
        if(Input.GetMouseButtonDown(0))
        {
            swipeFirstPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        }
        if(Input.GetMouseButtonUp(0))
        {
            swipeLastPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
       
            if (Mathf.Abs(swipeLastPosition.x - swipeFirstPosition.x) > swipeDragDistance)
            {
                if ((swipeLastPosition.x < swipeFirstPosition.x)) 
                {
                    toggleVisibility();
                }
                else
                {
                    toggleVisibility();
                }
            }
        }
    }

    public void upCommand()
    {
        currentCommand++;
        index = console.commandHistory.Count - currentCommand;
        if ((index >= 0) && (console.commandHistory.Count!=0))
        {
            inputField.text = console.commandHistory[index].ToString();
        }
        else
        {
            currentCommand = console.commandHistory.Count;
        }
        inputField.ActivateInputField();
        inputField.Select();
    }

    public void downCommand()
    {
        currentCommand--;
        index = console.commandHistory.Count - currentCommand ;
        if (index < console.commandHistory.Count)
        {
            inputField.text = console.commandHistory[index].ToString();
        }
        else
        {
            inputField.text = "";
            currentCommand = 0;
        }
        inputField.ActivateInputField();
        inputField.Select();
    }

    void toggleVisibility()
    {
        setVisibility(!viewContainer.activeSelf);
        inputField.ActivateInputField();
        inputField.Select();
    }

    void setVisibility(bool visible)
    {
        viewContainer.SetActive(visible);
        if (inputField.text == "`")
        {
            inputField.text = "";
        }
    }

    void onVisibilityChanged(bool visible)
    {
        setVisibility(visible);
    }

    void onLogChanged(string[] newLog)
    {
        updateLogStr(newLog);
    }

    void updateLogStr(string[] newLog)
    {
        if (newLog == null)
        {
            logTextArea.text = "";
        }
        else
        {
            logTextArea.text = string.Join("\n", newLog);
        }
    }

    public void runCommand()
    {

        console.runCommandString(inputField.text);
        inputField.text = "";
        inputField.ActivateInputField();
        inputField.Select();
        currentCommand = 0;
    }
}