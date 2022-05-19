import React from 'react'
import { Route, Routes } from 'react-router-dom'
import Login from './components/Login'
import CreateBug from './components/CreateBug'
import CreateAccount from './components/CreateAccount'
import BugUpdateForm from './components/BugUpdateForm'
import { Component } from 'react'
import HomePage from './components/pages/HomePage'
import Profile from './components/pages/Profile'
import Home from './components/Home'
import MemberList from './components/pages/MemberList'

const BrowserRoutes = () => {
  return (
    <Routes>
      <Route path='/' element={<Login />} />
      <Route path='/Home' element={<Home />} />
      <Route path='login' element={<Login />} />
      <Route path='createAccount' element={<CreateAccount />} />
      <Route path='UpdateBug/:id' element={<BugUpdateForm />}></Route>
      <Route path='CreateBug' element={<CreateBug />} />
      <Route path='HomePage' element={<HomePage />} />
      <Route path='Profile' element={<Profile />} />
      <Route path='MemberList' element={<MemberList />} />
    </Routes>
  )
}

export default BrowserRoutes
