using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    List<string> response = new List<string>();
    
    public List<string> Interpret(string userInput)
    {
        response.Clear();
        string[] args = userInput.Split();

        if(args[0] == "help")
        {
            response.Add("If you want to use the terminal, tyoe \"boop\" ");
            response.Add("This is the second line that we are returning");

            return response;
        } else
        {
            response.Add(userInput + ": command not found");

            return response;
        }
    }
}
