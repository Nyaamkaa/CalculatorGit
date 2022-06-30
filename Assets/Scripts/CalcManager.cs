using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalcManager : MonoBehaviour
{
    [SerializeField] private Text countingString;
    [SerializeField] private Text countOperator;

    private bool errorCount;
    private bool countValid;
    private bool specialAction;
    private double firstValue;
    private double secondValue;
    private double result;
    private char storedOperator;

    private void Start()
    {
        ButtonInput('c');
    }
    private void ClearCalc()
    {
        countingString.text = "0";
        countOperator.text = "";
        specialAction = countValid = errorCount = false;
        firstValue = result = secondValue = 0;
        storedOperator = ' ';
    }
    private void UpdateСounting()
    {
        if (!errorCount)
        {
            countingString.text = firstValue.ToString();
        }
        countValid = false;
    }

    private void CalcResult(char activeOperator)
    {
        switch (activeOperator)
        {
            case '=':
                result = firstValue;
                break;
            case '+':
                result = secondValue + firstValue;
                break;
            case '-':
                result = secondValue - firstValue;
                break;
            case '*':
                result = secondValue * firstValue;
                break;
            case '/':
                if (firstValue != 0)
                {
                    result = secondValue / firstValue;
                }
                else
                {
                    errorCount = true;
                    countingString.text = "ERROR";
                }
                break;
            default:
                Debug.Log("unknown: " + activeOperator);
                break;

        }
        firstValue = result;
        UpdateСounting();
    }
    public void ButtonInput(char caption)
    {
        if (errorCount)
        {
            ClearCalc();
        }

        if ((caption >= '0' && caption <= '9') || caption == ',')
        {
            if (countingString.text.Length < 15 || !countValid)
            {

                if (!countValid)
                {
                    countingString.text = (caption == ',' ? "0" : " ");
                }
               
                else if (countingString.text == "0" && caption != ',')
                {
                    countingString.text = "";
                }
                // ограничения по запятой
                if (!countingString.text.Contains(",") && !(caption == ','))
                {
                    countingString.text += caption;
                }
                if (!countingString.text.Contains(",") && (caption == ','))
                {
                    countingString.text += caption;
                }
                if (countingString.text.Contains(",") && !(caption == ','))
                {
                    countingString.text += caption;
                }

                countValid = true;
            }
        }

        else if (caption == 'c')
        {
            ClearCalc();
        }

        else if (caption == '±')
        {
            firstValue = -double.Parse(countingString.text);
            UpdateСounting();
            specialAction = true;
        }

        else if (caption == '%')
        {
            firstValue = double.Parse(countingString.text) / 100.0d;
            UpdateСounting();
            specialAction = true;
        }

        else if (caption == '√')
        {
            firstValue = Math.Sqrt(double.Parse(countingString.text));
            UpdateСounting();
            specialAction = true;
        }

        else if (countValid || storedOperator == '=' || specialAction)
        {
            firstValue = double.Parse(countingString.text);
            countValid = false;
            if (storedOperator != ' ')
            {
                CalcResult(storedOperator);
                storedOperator = ' ';
            }
            countOperator.text = caption.ToString();
            storedOperator = caption;
            secondValue = firstValue;
            UpdateСounting();
            specialAction = false;
        }
    }
}
