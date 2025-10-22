import { Text, View } from "react-native-web";
import { StyleSheet } from "react-native";
import { Link, Outlet } from "react-router-dom";
import ProjButton, { ProjButtonTypes } from "../../features/ui/ProjButton";

export default function Layout() {

  return (
    <View style={styles.root}>
      <View style={styles.header}>
        <View style={styles.brandContainer}>
          <Text style={styles.brand}>ASP_P26</Text>
          <Link style={styles.navLink} to="/"><Text>Home</Text></Link>
          <Link style={styles.navLink} to="/privacy"><Text>Privacy</Text></Link>
        </View>
        <ProjButton title="Log in" type={ProjButtonTypes.white}></ProjButton>
        <ProjButton title="Log out" type={ProjButtonTypes.white}></ProjButton>
      </View>
      <View style={styles.main}><Outlet /></View>

      <View style={styles.footer}>
        <Text>© 2025 - React-P26 - <Text style={styles.footerLink}>Privacy</Text></Text>
      </View>

    </View>
  );
}


const styles = StyleSheet.create({
  root: {
    display: "flex",
    flexDirection: "column",
    justifyContent: "space-between",
    width: "100%",
    minHeight: "100vh"
  },
  header: {
    display: "flex",
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    gap: 10,
    backgroundColor: "#ffffff",
    borderBottomWidth: 1,
    borderBottomColor: "#e5e5e5",
    paddingHorizontal: 15,
    paddingVertical: 12,
  },
  brandContainer: {
    flex: 1,
    display: "flex",
    flexDirection: "row",
    alignItems: "center",
    gap: 12,
  },
  brand: {
    fontWeight: '700',
    fontSize: 18
  },
  navItems: {
    flex: 2,
    display: "flex",
    flexDirection: "row",
    gap: 12,
    justifyContent: "center",
    alignItems: "center",
  },
  navLink: {
    textDecorationLine: "none",
    color: '#111'
  },
  icon: {
    marginLeft: 8
  },
  controls: {
    flex: 1,
    display: "flex",
    flexDirection: "row",
    justifyContent: "flex-end",
    gap: 10,
    alignItems: "center",
  },
  cartButton: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 8,
    paddingVertical: 6,
    borderWidth: 1,
    borderColor: '#198754',
    borderRadius: 6,
  },
  main: {
    paddingHorizontal: 15,
    paddingVertical: 20,
    flex: 1
  },
  footer: {
    borderTopWidth: 1,
    borderTopColor: '#e5e5e5',
    paddingHorizontal: 15,
    paddingVertical: 12,
    backgroundColor: '#fff'
  },
  footerLink: {
    textDecorationLine: 'underline'
  }
});