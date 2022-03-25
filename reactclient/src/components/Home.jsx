import { render } from '@testing-library/react'
import React, { useState, useEffect } from 'react'
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom'
//import BugItLogo from './images/BugItLogo.jpg';
import './CSS/home.css'

import { Button } from 'react-bootstrap'



function readJson(bug, index) {

  return (
    <tr key={index}>
      <td>{bug.bugid}</td>
      <td>{bug.creator}</td>
      <td>{bug.timecreated}</td>
      <td>{bug.description}</td>
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

var myInit = {
  method: 'POST',
  Headers: {
    'Content-Type': 'application/json',
  },
  mode: 'cors',
  cache: 'default',
}

const getBugUrl = 'https://localhost:7075/get-all-bugs'

let myRequest = new Request(getBugUrl, myInit)

 const Home =() => {
  const [bugData, setBugData] = useState([]);
  
  async function getAllBugs() {
    fetch(myRequest)
    .then(response => response.json())
    .then(bugsFromServer => {
      setBugData(bugsFromServer);
      })
      .then(function (data) {
        console.log(data)
      })
  }
  
  console.log("bugData: " + bugData)
  
  useEffect(() => {
    getAllBugs([])
  }, []);
    
    
      
  

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
              <th>BugId</th>
              <th>Creator</th>
              <th>Name</th>
              <th>Date</th>
              <th>Type</th>
              <th>Status</th>
              <th>Priority</th>
              <th>Estimated Time</th>
            </tr>
          </thead>
          <tbody >
          {bugData.map((bug)=>(
 <tr key={bug.id}>
 <th scope="row">{bug.bugId}</th>
 <td>{bug.creator}</td>
 <td>{bug.timeCreated}</td>
 <td>{bug.description}</td>
 <td>{bug.type}</td>
 <td>{bug.status}</td>
 <td>{bug.priority}</td>
 <td>{bug.estimatedTime}</td>
 </tr>))}
    
          </tbody>
        </table>
      
      </div>
      
    </body>
    
  )
}
export default Home
