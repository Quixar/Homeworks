import { useEffect, useState } from 'react';
import './ui/Calc.css';
import CalcButton from './ui/CalcButton'; 
import CalcButton2 from './ui/CalcButton2';

export default function Calc() {

    const [expression, setExpession] = useState("");
    const [display, setDisplay] = useState("0");
    const [displayFontSize, setDisplayFontSize] = useState(36);

    const [prevValue, setPrevValue] = useState(null);
    const [operator, setOperator] = useState(null);
    const [waitingForOperand, setWaitingForOperand] = useState(false); 

    useEffect(() => {
        if(display.length > 8) {
            setDisplayFontSize(36 - 2.5 * (display.length - 8));
        }
        else {
            setDisplayFontSize(36);
        }
    }, [display]);


    const onDotClick = (dotSymbol) => {
        if (waitingForOperand) {
            setDisplay("0" + dotSymbol);
            setWaitingForOperand(false);
        } else if (!display.includes(dotSymbol)) {
            setDisplay(display + dotSymbol);
        }
    }
    
    const onBackspaceClick = () => {
        let res = (display.length > 1
            ? display.substring(0, display.length - 1)
            : "0");
        setDisplay(res === '-' ? "0" : res);
    };
    
    const onDigitClick = (digit) => {
        if (waitingForOperand) {
            setDisplay(digit);
            setWaitingForOperand(false);
        } else {
            let res = display;
            if (res === "0") {
                res = "";
            }
            if (res.length > 14) return;
            res += digit;
            setDisplay(res);
        }
    }

    const onClearClick = () => { 
        setDisplay("0");
        setExpession("");
        setPrevValue(null);
        setOperator(null);
        setWaitingForOperand(false);
    };

    const performCalculation = () => { 
        const current = parseFloat(display);
        const previous = parseFloat(prevValue);

        if (isNaN(previous) || isNaN(current) || !operator) {
            return current;
        }

        let result;
        switch (operator) {
            case "+":
                result = previous + current;
                break;
            case "−":
                result = previous - current;
                break;
            case "×":
                result = previous * current;
                break;
            case "÷":
                if (current === 0) {
                    return "Error"; 
                }
                result = previous / current;
                break;
            default:
                return current;
        }
        return parseFloat(result.toPrecision(12));
    };


    const onOperatorClick = (op) => {
        if(display === "Error") {
            onClearClick();
            return;
        }

        const currentValue = parseFloat(display);

        if (operator && waitingForOperand) {
            setOperator(op);
            setExpession(`${prevValue} ${op}`);
            return;
        }

        if (prevValue === null) {
            setPrevValue(currentValue);
        } else {
            const result = performCalculation();
            if (result === "Error") {
                setDisplay("Error");
                setExpession("");
                setPrevValue(null);
                setOperator(null);
                setWaitingForOperand(true);
                return;
            }
            const resultString = String(result);
            setDisplay(resultString);
            setPrevValue(result);
        }

        setWaitingForOperand(true);
        setOperator(op);
        setExpession(`${display} ${op}`);
    };

    const onEqualsClick = () => {
        if (!operator || prevValue === null || waitingForOperand || display === "Error") {
            return;
        }

        const result = performCalculation();
        if (result === "Error") {
            setDisplay("Error");
            setExpession("");
            setPrevValue(null);
            setOperator(null);
            setWaitingForOperand(true);
            return;
        }

        const resultString = String(result);
        setExpession(`${prevValue} ${operator} ${display} =`);
        setDisplay(resultString);

        setPrevValue(null);
        setOperator(null);
        setWaitingForOperand(true); 
    };

    
    const buttonObjects = [
        [ 
            {face: "%",  type: "func", action: () => {}},
            {face: "CE", type: "func", action: () => {}},
            {face: "C",  type: "func", action: onClearClick},
            {face: "⌫", type: "func", action: onBackspaceClick},
        ],
        [ 
            {face: "1/x", type: "func", action: () => {}},
            {face: "x²",  type: "func", action: () => {}},
            {face: "√x",  type: "func", action: () => {}},
            {face: "÷",   type: "func", action: () => onOperatorClick("÷")},
        ],
        [ 
            {face: "7",  type: "digit", action: onDigitClick},
            {face: "8",  type: "digit", action: onDigitClick},
            {face: "9",  type: "digit", action: onDigitClick},
            {face: "×",  type: "func", action: () => onOperatorClick("×")},
        ],
        [ 
            {face: "4",  type: "digit", action: onDigitClick},
            {face: "5", type:  "digit", action: onDigitClick},
            {face: "6",  type: "digit", action: onDigitClick},
            {face: "−", type: "func", action: () => onOperatorClick("−")},
        ],
        [ 
            {face: "1",  type: "digit", action: onDigitClick},
            {face: "2",  type: "digit", action: onDigitClick},
            {face: "3",  type: "digit", action: onDigitClick},
            {face: "+", type: "func", action: () => onOperatorClick("+")},
        ],
        [ 
            {face: "±",  type: "digit", action: _ => {if(display === "0" || display === "Error") return; setDisplay(display.startsWith('-') ? display.substring(1) : "-" + display);}},
            {face: "0",  type: "digit", action: onDigitClick},
            {face: ".",  type: "digit", action: onDotClick},
            {face: "=", type: "func", action: onEqualsClick}, 
        ],        
    ];

    return <div className="calc">
        <div className='calc-expression'>{expression}</div>
        <div className='calc-display' style={{fontSize: displayFontSize}}>{display}</div>
        {buttonObjects.map((row, index) => <div key={index} className="calc-row">
            {row.map(obj => <CalcButton2 key={obj.face} buttonObject={obj} />)}
        </div>)}
    </div>;

}