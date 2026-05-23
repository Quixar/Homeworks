import { useState, useEffect } from 'react';
import './ui/Calc.css';
import CalcButton from './ui/CalcButton';
import CalcButton2 from './ui/CalcButton2';

export default function Calc() {
    const buttons = [
        ["%", "CE", "C", "⌫"],          
        ["1/x", "x²", "√x", "÷"],       
        ["_7", "_8", "_9", "×"],
        ["_4", "_5", "_6", "−"],
        ["_1", "_2", "_3", "+"],
        ["_±", "_0", "_.", "="],            
    ];

    const [expression, setExpression] = useState("");
    const [display, setDisplay] = useState("0");
    const [displayFontSize, setDisplayFontSize] = useState(36);
    const [isOp, setIsOp] = useState(false);

    useEffect(() => {
        if(display.length > 8) {
            setDisplayFontSize(36 - 2.5 * (display.length - 8));
        }
        else {
            setDisplayFontSize(36);
        }
    }, [display]);

    const onButtonClick = (face) => {
        if(face.charAt(0) === '_' && "0123456789".indexOf(face.substring(1)) > -1 ){
            onDigitClick(face.substring(1));
        }
        else switch(face){
            case "C": onClearClick(); break;
            case "⌫": onBackspaceClick(); break;
            case "_±": onPmClick(); break;
            case "_.": onDotClick(); break;
            case "%": doPercent(); break;
            case "CE": doClearEntry(); break;
            case "1/x": doInverse(); break;
            case "x²": doSquare(); break;
            case "√x": doSqrt(); break;
            case "÷": doOp("÷"); break;
            case "×": doOp("×"); break;
            case "−": doOp("−"); break;
            case "+": doOp("+"); break;
            case "=": doEquals(); break;
        }
    };

    const onClearClick = () => {
        setDisplay("0");
        setExpression("");
        setIsOp(false);
    };

    const doClearEntry = () => {
        setDisplay("0");
        setIsOp(false);
    };

    const onDotClick = () => {
        if (display.includes(".")) return;
        let res = display;
        if (res === "0" || isOp) {
            res = "0.";
            setIsOp(false);
        } else {
            res += ".";
        }
        setDisplay(res);
    };

    const onPmClick = () => {
        if (display === "0") return; 
        if (display.startsWith("-")) {
            setDisplay(display.substring(1));
        } else {
            setDisplay("-" + display);
        }
    };

    const onBackspaceClick = () => {
        let res = (display.length > 1
            ? display.substring(0, display.length - 1)
            : "0");
        setDisplay(res === '-' ? "0" : res);
    };

    const onDigitClick = (digit) => {
        let res = display;
        if(res === "0" || isOp){
            res = "";
            setIsOp(false);
        }
        if(res.length > 14) return;
        if(res.length > 8) {
            setDisplayFontSize(36 - 2.5 * (res.length - 8));
        }
        res += digit;
        setDisplay(res);
    };

    const doPercent = () => {
        const num = parseFloat(display);
        if (isNaN(num)) return;
        const res = (num / 100).toString();
        setDisplay(res);
        setExpression((prev) => prev.replace(/\d+(\.\d+)?$/, res));
    };

    const doInverse = () => {
        const num = parseFloat(display);
        if (num === 0) return;
        const res = (1 / num).toString();
        setDisplay(res);
        setExpression((prev) => prev.replace(/\d+(\.\d+)?$/, res));
    };

    const doSquare = () => {
        const num = parseFloat(display);
        if (isNaN(num)) return;
        const res = (num * num).toString();
        setDisplay(res);
        setExpression((prev) => prev.replace(/\d+(\.\d+)?$/, res));
    };

    const doSqrt = () => {
        const num = parseFloat(display);
        if (num < 0) return;
        const res = Math.sqrt(num).toString();
        setDisplay(res);
        setExpression((prev) => prev.replace(/\d+(\.\d+)?$/, res));
    };

    const doOp = (op) => {
        if (isOp) {
            setExpression((prev) => prev.slice(0, -2) + ` ${op} `);
        } else {
            setExpression((prev) => prev + display + ` ${op} `);
        }
        setIsOp(true);
    };

    const doEquals = () => {
        if (isOp) return;
        const fullExp = expression + display;
        try {
            const evalExp = fullExp
                .replace(/×/g, "*")
                .replace(/÷/g, "/")
                .replace(/−/g, "-");
            const res = eval(evalExp).toString();
            setDisplay(res);
            setExpression(fullExp + " = ");
            setIsOp(false);
        } catch (e) {
            setDisplay("Error");
            setExpression("");
            setIsOp(false);
        }
    };

    const buttonObjects = [
        [ 
            {face: "%",  type: "func", action: doPercent},
            {face: "CE", type: "func", action: doClearEntry},
            {face: "C",  type: "func", action: onClearClick},
            {face: "⌫", type: "func", action: onBackspaceClick},
        ],
        [ 
            {face: "1/x",  type: "func", action: doInverse},
            {face: "x²", type: "func", action: doSquare},
            {face: "√x",  type: "func", action: doSqrt},
            {face: "÷", type: "func", action: () => doOp("÷")},
        ],
        [ 
            {face: "7",  type: "digit", action: () => onDigitClick("7")},
            {face: "8", type:  "digit", action: () => onDigitClick("8")},
            {face: "9",  type: "digit", action: () => onDigitClick("9")},
            {face: "×", type: "func", action: () => doOp("×")},
        ],
        [ 
            {face: "4",  type: "digit", action: () => onDigitClick("4")},
            {face: "5", type:  "digit", action: () => onDigitClick("5")},
            {face: "6",  type: "digit", action: () => onDigitClick("6")},
            {face: "−", type: "func", action: () => doOp("−")},
        ],
        [ 
            {face: "1",  type: "digit", action: () => onDigitClick("1")},
            {face: "2",  type: "digit", action: () => onDigitClick("2")},
            {face: "3",  type: "digit", action: () => onDigitClick("3")},
            {face: "+", type: "func", action: () => doOp("+")},
        ],
        [ 
            {face: "±",  type: "digit", action: onPmClick},
            {face: "0",  type: "digit", action: () => onDigitClick("0")},
            {face: ".",  type: "digit", action: onDotClick},
            {face: "=", type: "func", action: doEquals},
        ],        
    ];

    return (
        <div className="calc">
            <div className='calc-expression'>{expression}</div>
            <div className='calc-display' style={{fontSize:displayFontSize}}>{display}</div>
            {buttonObjects.map((row, index) =>  (
                <div key={index} className="calc-row">
                    {row.map(obj => <CalcButton2 key={obj.face} buttonObject={obj} >/</CalcButton2>)}         
                </div>
            ))}
        </div>
    );
}