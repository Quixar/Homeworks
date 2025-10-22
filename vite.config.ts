import { defineConfig } from 'vite'
import reactNativeWeb from "vite-plugin-react-native-web";
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [
    react(),
    reactNativeWeb()
  ]
});