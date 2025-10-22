import { createContext } from "react";
import type { ModalData } from "../types/ModalData";

type AppContextType = {
    showModal: (data: ModalData | null) => void;
};

const AppContext = createContext<AppContextType>({
    showModal: (_:ModalData | null) => {throw "showModal not implemented";}
});

export default AppContext;