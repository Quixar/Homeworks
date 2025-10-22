import { View, Text } from 'react-native-web'
import { StyleSheet } from 'react-native';

export default function Privacy() {

  return <View style={styles.root}>
    <Text>Privacy</Text>
  </View>;
}

const styles = StyleSheet.create({
  root: {
    backgroundColor: 'inherit',
  }
});