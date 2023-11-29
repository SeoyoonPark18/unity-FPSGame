using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System;

public class Calculator : MonoBehaviour
{

    public TextMeshProUGUI textField; //화면 출력용
    string input; //버튼 입력 값
    string number; //숫자 누적용
    float result; //계산 값
    Stack<string> operStack = new Stack<string>();
    Stack<float> numStack = new Stack<float>(); 
    bool stillNum = false; //아직 숫자 누적 진행 중
    bool isFloat = false; //해당 숫자는 float 형태
    bool isMul = false; //곱셈 계산 필요
    bool isDiv = false; //나눗셈 계산 필요
    
    
    void Start()
    {
        result = 0;
        input = "";
        textField.text = "";
        number = "";
    }

    void Update()
    {

    }
 
    public void ButtonClicked()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject; //클릭한 버튼 오브젝트
        input = clickedBtn.GetComponentInChildren<TMP_Text>().text; //버튼의 텍스트
        
        if(input == "x") // x 일때
        {
            if(stillNum == true){
                stillNum = false;
                MakeNumber();
            }

            textField.text += input;
            isMul = true;
        }
        else if(input == "÷") // ÷ 일때
        {
            if(stillNum == true){
                stillNum = false;
                MakeNumber();
            }
            textField.text += input;
            isDiv = true;
        }
        else if(input == "+") // + 일때
        {
            if(stillNum == true){
                stillNum = false;
                MakeNumber();
            }
            textField.text += input;
            operStack.Push("+");
        }
        else if(input == "-") // - 일때
        {
            if(stillNum == true){
                stillNum = false;
                MakeNumber();
            }
            textField.text += input;
            operStack.Push("-");
        }
        else if(input == "=") // = 일때
        {
            if(stillNum == true){
                stillNum = false;
                MakeNumber();
            }
            if(operStack.Count != 0){
                string s = operStack.Pop();
                if(s == "+")
                {
                   Plus();
                }
                else if(s == "-")
                {
                   Minus();
                }
                else
                {              
                   if(numStack.Count != 0){
                      result += numStack.Pop();
                   }
                }
            }
            else
            {
                result += numStack.Pop();
            }
            
            //
            Debug.Log(result);
            textField.text = result.ToString(); //계산 값 화면 출력
        }
        else if(input == ".")
        {
            textField.text += input;  
            MakeFloatNumber();
            isFloat = true;
        }
        else if(input == "c") //지우기 Clear
        {
            result = 0;
            input = "";
            textField.text = "";
            numStack.Clear();
            operStack.Clear();
        }
        else //num
        {           
            textField.text += input;
            //숫자 누적
            stillNum = true;
            number += input;
        }

    }

    void MakeNumber() //이어진 숫자로 변환(1자리 이상의 수로)
    {
        if(isFloat == true){
            MakeFloatNumber();
        }
        else{
            int _number = Int32.Parse(number);
            numStack.Push((float)_number);
            number = "";

            //곱셈 및 나눗셈은 바로 계산
            if(isMul == true){
                Multiply();
            }
            if(isDiv == true){
                Divide();
            }
        }
    }

    string left = ""; string right = "";
    void MakeFloatNumber() //소수로 변환
    {
        if(left == ""){
            left = number;
            number = "";
            return;
        } 
        else if(right == ""){
            right = number;
            number = "";
            string newFloatNumber = left + "." + right;
            Debug.Log(newFloatNumber);
            float newNumber = (float.Parse(newFloatNumber));
            Debug.Log(newNumber);
            numStack.Push(newNumber);
            isFloat = false;
        } 
        //곱셈 및 나눗셈은 바로 계산해주기
        if(isMul == true){
            Multiply();
        }
        if(isDiv == true){
            Divide();
        }
    }

    void Multiply() //곱셈
    {
        float b = numStack.Pop();
        if(numStack.Count == 0){
            result *= b;
        }
        else{
            float a = numStack.Pop();
            float value = a * b;
            numStack.Push(value);
            isMul = false;
        }
    } 
    void Divide() //나눗셈
    {
        float b = numStack.Pop();
        if(numStack.Count == 0){
            float value = result/b + result%b;
            numStack.Push(value);
        }
        else{
            float a = numStack.Pop();
            float value = a/b + a%b;
            numStack.Push(value);
            isDiv = false;
        }        
    }
    void Plus() //덧셈
    {
        float b = numStack.Pop();
        if(numStack.Count == 0){
            result += b;
        }
        else{
            float a = numStack.Pop();
            result += a + b;
        }
    }
    void Minus() // 뺄셈
    {
        float b = numStack.Pop();
        if(numStack.Count == 0){
            result -= b;
        }
        else{
            float a = numStack.Pop();
            result += a - b;
        }
    }
}
