import React from "react";
import { Route, Routes } from "react-router-dom";
import NewHome from "./components/newHome";
import Login from "./components/Login";
import CreateBug from "./components/CreateBug";
import NewCreateAccount from "./components/NewCreateAccount";
import BugUpdateForm from "./components/BugUpdateForm";
import { Component } from "react";
//import Sidebar from "./components/SideBar/Sidebar";
import HomePage from "./components/pages/HomePage";
import Profile from "./components/pages/Profile";
import Home from "./components/newHome";

const BrowserRoutes = () => {
  return (
    <Routes>
      <Route path="login" element={<Login />} />
      <Route path="createAccount" element={<NewCreateAccount />} />
      <Route path="UpdateBug/:id" element={<BugUpdateForm />}></Route>
      <Route path="/" element={<NewHome />} />
      <Route path="CreateBug" element={<CreateBug />} />
      <Route path="HomePage" element={<HomePage />} />
      <Route path="Profle" element={<Profile />} />
    </Routes>
  );
};

export default BrowserRoutes;
