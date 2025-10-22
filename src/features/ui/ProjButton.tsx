import { Pressable, Text } from "react-native-web";
import { StyleSheet, type StyleProp, type ViewStyle } from "react-native";

export {ProjButtonTypes, ProjButton};

enum ProjButtonTypes {
    primary,
    secondary,
    success,
    white
}

export default function ProjButton({type, action, title, style} : 
    {type: ProjButtonTypes, 
     action:() => void, 
     title:string, 
     style?: StyleProp<ViewStyle> | undefined
    }) {

    let btnStyle;
    switch(type) {
        case ProjButtonTypes.secondary: btnStyle = styles.secondary; break;
        case ProjButtonTypes.success: btnStyle = styles.success; break;
        case ProjButtonTypes.white: btnStyle = styles.white; break;
        default: btnStyle = styles.primary;
    };

    return <Pressable onPress={action} style={style}>
        <Text style={[styles.button, btnStyle]}>{title}</Text>
    </Pressable>;
}

const styles = StyleSheet.create({
    "button" : {
        borderWidth: 2,
        borderRadius: 5,
        fontWeight: 700,
        paddingHorizontal: 15,
        paddingVertical: 8,
        textAlign: "center",
    },
    "primary" : {
        backgroundColor: "#6c41d3ff",
        borderColor: "#3c2473ff",
        color: "#cdb0b9ff",
    },
    "secondary" : {
        backgroundColor: "#a4a1acff",
        borderColor: "#cdb0b9ff",
        color: "#181818ff",
    },
    "success" : {
        backgroundColor: "#40d148ff",
        borderColor: "#5eb821ff",
        color: "white",
    },
    "white": {
        backgroundColor: "#ffffffff",
        borderColor: "#e0e0e0ff",
        color: "#181818ff",
    }
}); 