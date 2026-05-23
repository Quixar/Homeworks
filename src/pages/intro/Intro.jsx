import { useContext } from "react";
import { useState } from "react";
import AppContext from "../../app/features/context/AppContext";
import Calc from "../../widgets/calc/Calc";

export default function Intro() {
    const {user} = useContext(AppContext);  // hook
    const [count, setCount] = useState(0);     // hook

    const onCountClick = () => {
        setCount(count + 1);
    };

    return <div className="text-center">
    <h1 className="display-4">Вступ.Управління станом</h1>
    <div className="row" >
        <div className="col"><button className="btn btn-dark" onClick={onCountClick}>+1</button>
    <h3>Підсумок: {count}</h3>
    {!!user && <p>Вітання, {user.Name}</p>}
    <hr/>
    <CountWidget count={count} setCount={setCount} />
    </div>
        <div className="col"> <Calc /></div>
    </div>
</div>;
}

function CountWidget(props){
    return <div className="border p-2 m-3">Ваш підсумок: {props.count}
    <button className="btn btn-dark" onClick={() => props.setCount(0)}>Скинути</button>
    </div>
}