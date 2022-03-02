import { render } from '@testing-library/react'
import React from 'react'
//import BugItLogo from './images/BugItLogo.jpg';
import './CSS/home.css'
import Data from './data.json'

function readJson(bugs, index) {
  return (
    <tr key={index}>
      <td>{bugs.software}</td>
      <td>{bugs.name}</td>
      <td>{bugs.date}</td>
      <td>{bugs.type}</td>
      <td>{bugs.status}</td>
      <td>{bugs.priority}</td>
      <td>{bugs.estimatedTime}</td>
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

function showUser() {  
  const [user, setuser] = useState({ Email: '', Password: '' });  
  useEffect(() => {  
      var a = localStorage.getItem('myData');  
      var b = JSON.parse(a);  
      console.log(b.Name);  
      setuser(b)  
      console.log(user.Name)  

  }, []);
  return (  
    <>  
       <div class="bottomleft">{user.Name}</div>  
    </>  
)  
}  

export const Home = () => (
  <body>
    <h1>BugIt</h1>
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
    <div>
      <button type='button' class='btn btn-success' onClick={addBug}>
        Add
      </button>
      <button type='button' class='btn btn-danger' onClick={deleteBug}>
        Delete
      </button>
    </div>
  </body>
)
