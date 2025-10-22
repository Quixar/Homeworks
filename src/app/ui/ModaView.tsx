import { View, Text, Image } from 'react-native-web'
import { Pressable, StyleSheet } from 'react-native';
import AppContext from '../../features/context/AppContext';
import { use, useContext, useState } from 'react';
import type { ModalData } from '../../features/types/ModalData';
import ProjButton, { ProjButtonTypes } from '../../features/ui/ProjButton';

export default function ModalView({data}: {data:ModalData | null}) {
  const {showModal} = useContext(AppContext);

  return <View style={[styles.fullScreen, {display: (data == null ? 'none' : 'flex')} ]}>
    <View style={styles.modal}>
      <Pressable onPress={() => showModal(null)} style={styles.closeButton}>
        <Image style={styles.closeButtonImg} source="/img/close.png" />
      </Pressable>
      <Text style={styles.title}>{data?.title}</Text>

      <Text style={styles.message}>{data?.message}</Text> 

      <ProjButton 
        type={ProjButtonTypes.secondary} 
        title="Close" 
        action={() => showModal(null)} />
    </View>

  </View>
}

const styles = StyleSheet.create({
  fullScreen: {
    width: "100%",
    minWidth: 300,
    height: "100%",
    backgroundColor: "#88b0b0b0",
    position: "absolute",
    left: 0,
    top: 0,
    justifyContent: "center",
    alignItems: "center",
    flexDirection: "column",
  },
  modal: {
    paddingTop: 40,
    width: "30%",
    minHeight: "30%",
    backgroundColor: "#f0f0f0ff",
    borderRadius: 15,
  },
  closeButton: {
    width: 40,
    height: 40,
    position: "absolute",
    top: 0,
    right: 0,
  },
  closeButtonImg: {
    width: 20,
    height: 20,
    position: "absolute",
    top: 10,
    right: 10,
  },
  title:{
    fontWeight: 700,
    fontSize: 22,
    textAlign: "center"
  },
  message:{
    textAlign: "center",
    paddingVertical: 16,
    paddingHorizontal: 10,
    fontSize: 14
  }
});