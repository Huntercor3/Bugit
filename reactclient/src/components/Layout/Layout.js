import React from "react";
import Sidebar from "../SideBar/Sidebar";
import { SLayout, SMain } from "./Styles";

const Layout = ({ children }) => {
  return (
    <SLayout>
      <Sidebar />
      <SMain>{children}</SMain>
    </SLayout>
  );
};

export default Layout;
