using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{
    public GameObject directoryLine;
    public GameObject responseLine;

    public InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect sr;
    public GameObject msgList;

    Interpreter interpreter;

    private void Start()
    {
        interpreter = GetComponent<Interpreter>();
    }

    private void OnGUI()
    {
        if(terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            string userInput = terminalInput.text;
            ClearInputField();
            AddDirectoryLine(userInput);
            int lines = AddInterpreterLines(interpreter.Interpret(userInput));
            //scroll to the bottom
            ScrollToBottom(lines);
            //Move the user inputLine to the end
            userInputLine.transform.SetAsLastSibling();
            //Refocus the input field
            terminalInput.ActivateInputField();
            terminalInput.Select();
        }

        void ClearInputField()
        {
            terminalInput.text = "";
        }

        void AddDirectoryLine(string userInput)
        {
            Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + 35.0f);

            GameObject msg = Instantiate(directoryLine, msgList.transform);
            //set its child index
            msg.transform.SetSiblingIndex(msgList.transform.childCount - 1);

            msg.GetComponentsInChildren<Text>()[1].text = userInput;
        }

        int AddInterpreterLines(List<string> interpretation)
        {
            for (int i = 0; i < interpretation.Count; i++)
            {
                GameObject res = Instantiate(responseLine, msgList.transform);
                res.transform.SetAsLastSibling();
                Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
                msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 35.0f);

                res.GetComponentInChildren<Text>().text = interpretation[i];
            }

            return interpretation.Count;
        }

        void ScrollToBottom(int lines)
        {
            if(lines > 4)
            {
                sr.velocity = new Vector2(0, 450);
            }
            else
            {
                sr.verticalNormalizedPosition = 0;
            }
        }
    }
}
