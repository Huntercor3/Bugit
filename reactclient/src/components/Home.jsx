import { render } from '@testing-library/react'
import React, { useState, useEffect } from 'react'
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom'
//import BugItLogo from './images/BugItLogo.jpg';
import './CSS/home.css'
import Data from './data.json'
import { Button } from 'react-bootstrap'



function readJson(bug, index) {

  return (
    <tr key={index}>
      <td>{bug.software}</td>
      <td>{bug.name}</td>
      <td>{bug.date}</td>
      <td>{bug.type}</td>
      <td>{bug.status}</td>
      <td>{bug.priority}</td>
      <td>{bug.estimatedTime}</td>
    </tr>
  )
}


function addBug() {
  console.log('added bug')
  const newBug = {
    software: 'BugIt',
    name: 'Jonas 2',
    date: 'never',
    type: 'Optimize',
    status: 'In Progress',
    priority: 'Low',
    estimatedTime: 'trust trust',
  }
  document.getElementById('myTable').insertRow(newBug)
  document.getElementById('myTbody').insertRow(newBug)
  return
}

function deleteBug() {
  console.log('deleted bug')
  document.getElementById('myTable').deleteRow(1)
  return
}

// function showUser() {
//   const [user, setuser] = useState({ Email: '', Password: '' })
//   useEffect(() => {
//     var a = localStorage.getItem('myData')
//     var b = JSON.parse(a)
//     console.log(b.Name)
//     setuser(b)
//     console.log(user.Name)
//   }, [])
//   return (
//     <>
//       <div class='bottomleft'>{user.Name}</div>
//     </>
//   )
//}



 const Home = () => {
   
  

  return (
    <body>
      <h1>BugIt</h1>
      <Link to='/createBug'>
        <Button class='btn btn-success'>New Bug</Button>
      </Link>
      <div class='table-responsive'>
        <table class='table table-striped table-sm' id='myTable'>
          <thead>
            <tr>
              <th>Software</th>
              <th>Name</th>
              <th>Date</th>
              <th>Type</th>
              <th>Status</th>
              <th>Priority</th>
              <th>Estimated Time</th>
            </tr>
          </thead>
          <tbody id='myTbody'>{Data.map(readJson)}</tbody>
        </table>
      </div>
      {/* <div>
        <button type='button' class='btn btn-success' onClick={addBug}>
          Add
        </button>
        <button type='button' class='btn btn-danger' onClick={deleteBug}>
          Delete
        </button>
      </div> */}
    </body>
  )
}
export default Home
