import React from 'react'
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom'
import Layout from './components/Sidebar'
import { Home } from './components/Home'
import { About } from './components/About'
import { Contact } from './components/Contact'
import { NoMatch } from './components/NoMatch'
import { Component } from 'react'
import { Login } from './components/Login'
import { Signup } from './components/Signup'
import { Account } from './components/Account'

export default function App() {
  return (
    <div>
      <h1>Hello World. Hunter is bad</h1>
    </div>
  )
}
