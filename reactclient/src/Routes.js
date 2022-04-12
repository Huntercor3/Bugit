import React from "react";
import { Route, Routes } from "react-router-dom";
import NewHome from "./components/newHome";
import Login from "./components/Login";
import CreateBug from "./components/CreateBug";
import CreateAccount from "./components/CreateAccount";
import BugUpdateForm from "./components/BugUpdateForm";
import { Component } from "react";
//import Sidebar from "./components/SideBar/Sidebar";
import HomePage from "./components/pages/HomePage";
import Home from "./components/newHome";

const BrowserRoutes = () => {
  return (
    <Routes>
      <Route path="login" element={<Login />} />
      <Route path="createAccount" element={<CreateAccount />} />
      <Route path="UpdateBug" element={<BugUpdateForm />}></Route>
      <Route path="/" element={<NewHome />} />
      <Route path="CreateBug" element={<CreateBug />} />
    </Routes>
  );
};

export default BrowserRoutes;
