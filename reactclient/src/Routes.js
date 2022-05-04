import React from 'react'
import { Route, Routes } from 'react-router-dom'
import NewHome from './components/newHome'
import Login from './components/Login'
import CreateBug from './components/CreateBug'
import NewCreateAccount from './components/NewCreateAccount'
import BugUpdateForm from './components/BugUpdateForm'
import { Component } from 'react'
//import Sidebar from "./components/SideBar/Sidebar";
import HomePage from './components/pages/HomePage'
import Profile from './components/pages/Profile'
import Home from './components/newHome'
import MemberList from './components/pages/MemberList'

const BrowserRoutes = () => {
  return (
    <Routes>
      <Route path='/' element={<Login />} />
      <Route path='/newHome' element={<NewHome />} />
      <Route path='login' element={<Login />} />
      <Route path='createAccount' element={<NewCreateAccount />} />
      <Route path='UpdateBug/:id' element={<BugUpdateForm />}></Route>
      <Route path='CreateBug' element={<CreateBug />} />
      <Route path='HomePage' element={<HomePage />} />
      <Route path='Profile' element={<Profile />} />
      <Route path='MemberList' element={<MemberList />} />
    </Routes>
  )
}

export default BrowserRoutes
