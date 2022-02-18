import React from 'react'
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom'
import { Sidebar } from './components/Sidebar'
import { Home } from './components/Home'
import { About } from './components/About'
import { Contact } from './components/Contact'
import { NoMatch } from './components/NoMatch'
import { Component } from 'react'
import { Login } from './components/Login'
import { CreateAccount } from './components/CreateAccount'
import NewCreateAccount from './components/NewCreateAccount'
import { Account } from './components/Account'

export default function App() {
  return (
    <div>
      <Routes>
        <Route path='login' element={<Login />} />
        <Route path='createAccount' element={<CreateAccount />} />
        <Route path='newCreateAccount' element={<NewCreateAccount />} />
        <Route path='/' element={<Sidebar />}>
          <Route index element={<Home />} />
          <Route path='about' element={<About />} />
          <Route path='contact' element={<Contact />} />
          <Route path='account' element={<Account />} />
          <Route path='*' element={<NoMatch />} />
        </Route>
      </Routes>
    </div>
  )
}
