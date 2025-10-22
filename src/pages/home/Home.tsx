import { View, Text } from 'react-native-web'
import { Pressable, StyleSheet } from 'react-native'
import { useContext } from 'react';
import AppContext from '../../features/context/AppContext';
import ProjButton, { ProjButtonTypes } from '../../features/ui/ProjButton';

export default function Home() {
  const { showModal } = useContext(AppContext);

  const onShowPress = () => {
    showModal({
      title: "Title",
      message: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
    });
  };

  return <View style={styles.root}>
    <Text>Hello World</Text>
      <ProjButton 
      action={onShowPress} 
      title="Show Modal" 
      type={ProjButtonTypes.primary}
      style={{maxWidth: 150, width: "20%"}}/>

      <ProjButton 
      action={onShowPress} 
      title="Show Modal" 
      type={ProjButtonTypes.secondary}
      style={{maxWidth: 150, width: "20%"}}/>

      <ProjButton 
      action={() => showModal({message: "Lorem ipsum"})} 
      title="Show Modal" 
      type={ProjButtonTypes.success}
      style={{maxWidth: 150, width: "20%"}}/>
  </View>;
}

const styles = StyleSheet.create({
  root: {
    backgroundColor: 'inherit',
  }
});