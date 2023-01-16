using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI screen;

    public List<char> operators;
    public List<string> presentOps = new List<string>();
    public List<float> operands = new List<float>();
    public float result;

    void Awake()
    {
        operators = new List<char> { '/', '*', '+', '-' };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Calculate()
    {
        if (screen.text == "" || operators.Contains(screen.text[screen.text.Length - 1]) || presentOps.Count == 0)
            return;

        string[] splitVal = screen.text.Split(operators.ToArray());
        for (int i = 0; i < splitVal.Length; i++)
        {
            operands.Add(float.Parse(splitVal[i]));
        }

        for (int i = 0; i < operators.Count; i++)
        {
            string op = operators[i].ToString();
            while (presentOps.Contains(op))
            {
                int idx = presentOps.IndexOf(op);
                float p1 = operands[idx];
                float p2 = operands[idx + 1];

                float ans = Map(p1, op, p2);

                operands.Insert(idx, ans);

                operands.Remove(p2);
                operands.Remove(p1);

                presentOps.Remove(op);

                result = ans;

            }
        }

        screen.fontSize = 0;
        screen.text = result.ToString();
        operands.Clear();
    }

    private float Map(float p1, string op, float p2)
    {
        float ans = 0;
        switch (op)
        {
            case "/":
                ans = p1 / p2;
                break;
            case "*":
                ans = p1 * p2;
                break;
            case "+":
                ans = p1 + p2;
                break;
            case "-":
                ans = p1 - p2;
                break;

        }
        return ans;
    }

    public void Input(string key)
    {
        int val;
        if (!int.TryParse(key, out val))
        {
            int lastIdx = screen.text.Length - 1;
            if (screen.text == "" || operators.Contains(screen.text[lastIdx]))
                return;

            for (int i = lastIdx; key == "." && i >= 0; i--)
            {
                if (screen.text[i] == '.')
                    return;
                else if (operators.Contains(screen.text[i]))
                    break;
            }

            if (key != ".")
                presentOps.Add(key);
        }
        screen.text += key;
    }

    public void ClearAll()
    {
        screen.text = "";
        presentOps.Clear();
    }

    public void Clear()
    {
        int lastIdx = screen.text.Length - 1;
        string lastVal = screen.text[lastIdx].ToString();
        screen.text = screen.text.Remove(lastIdx);
        presentOps.Remove(lastVal);
    }

    public void Close()
    {
        Application.Quit();
        Debug.Log("Close");
    }
}
