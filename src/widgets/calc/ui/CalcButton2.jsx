import './CalcButton.css';

export default function CalcButton2({buttonObject} ) {

    const isDigital = buttonObject.type == 'digit';

    return <button  className={"calc-button " + ( isDigital
                    ? "calc-button-digit" 
                    : "calc-button-func")}
                    onClick={ () => buttonObject.action(buttonObject.face)}>
                    {buttonObject.face}
            </button>;
}