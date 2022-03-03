import React from "react";
import { BrowserRouter as Router, Route, Routes, Link } from "react-router-dom";
import Sidebar from "./components/Sidebar";
import { Home } from "./components/Home";
import { About } from "./components/About";
import { Contact } from "./components/Contact";
import { NoMatch } from "./components/NoMatch";
import { Component } from "react";
import { Login } from "./components/Login";
import { Account } from "./components/Account";
import Login1 from "./components/Login1";

export default function App() {
  return (
    <div>
      <Routes>
        <Route path="login" element={<Login />} />
        <Route path="login1" element={<Login1 />} />

        <Route path="/" element={<Sidebar />}>
          <Route index element={<Home />} />
          <Route path="about" element={<About />} />
          <Route path="contact" element={<Contact />} />
          <Route path="account" element={<Account />} />
          <Route path="*" element={<NoMatch />} />
        </Route>
      </Routes>
    </div>
  );
}
