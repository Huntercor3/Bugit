///import { render } from '@testing-library/react'
//import BugItLogo from './images/BugItLogo.png';
import './CSS/home.css'
import { BrowserRouter as Router, Link } from 'react-router-dom'
import React, { useContext, useState, useEffect } from 'react'
import { Modal, Button, Form } from 'react-bootstrap'
import BootstrapTable from 'react-bootstrap-table-next'
import paginationFactory from 'react-bootstrap-table2-paginator'
import filterFactory, {
  textFilter,
  dateFilter,
  selectFilter,
} from 'react-bootstrap-table2-filter'
import Select from 'react-select'
import body from 'react-bootstrap-table-next/lib/src/body'
import BugUpdateForm from './BugUpdateForm'
import { IconButton } from '@material-ui/core'
//Icons
import { RiDeleteBin5Fill } from 'react-icons/ri'
import { FaRegEdit, FaEdit } from 'react-icons/fa'
import { darkTheme } from './styles/theme'
import * as Scroll from 'react-scroll';
import { ThemeContext } from '../App'

//export const DarkContext = React.createContext(null)

const Home = (props) => { 
  
/////////////////SETTINGTABLEDARK////////////////////
  const { setTheme, theme } = useContext(ThemeContext)
  const [DarkContext, setDarkContext] = useState(true)
  var bModalLight = "light-Modal"
  var bModalDark = "dark-Modal"
  var bModal;
  var bTableInputFalse = "table-hover table-bordered table-striped"
 var bTableInputTrue = "table-hover table-bordered table-striped table-dark"
  var bTable;
  console.log("theme", theme)
  if(theme === 'light')
  {
    bTable = bTableInputFalse;
    bModal = bModalLight;
  }
  else
  {
    bTable = bTableInputTrue;
    bModal = bModalDark
  }

 console.log("Bool", DarkContext)
/////////////////SETTINGTABLEDARK////////////////////

  /////////////////GETALLBUGS////////////////////
  const getBugsUrl = 'https://bugitserver.azurewebsites.net/get-all-bugs'
  const [bugData, setBugData] = useState([])
  const [bugCurrentlyBeingUpdated, setBugCurrentlyBeingUpdated] = useState(null)
  var myInit = {
    method: 'POST',
    Headers: {
      'Content-Type': 'application/json',
    },
    mode: 'cors',
    cache: 'default',
  }

  let myRequest = new Request(getBugsUrl, myInit)

  async function getAllBugs() {
    fetch(myRequest)
      .then(function(resp) {
        return resp.json()
      })
      .then(function(data) {
        setBugData(data)
      })
  }
/////////////////GETALLBUGS////////////////////



/////////////////GETUSERDATA////////////////////
const getUserUrl = "https://bugitserver.azurewebsites.net/GetCookie"
const [userData, setUserData] = useState([])
const [userName, setUserName] = useState("")

async function getUserData() {
  await fetch(getUserUrl, {
    method: 'GET',
  })
  .then(function (resp) {
    return resp.json()
  })
  
  .then(async function (data)  {
    setUserData(data)
   console.log("data sent in func ", data)
   console.log(data[0].value)
   setUserName(data[0].value)
  })    
} 
console.log("UserName value: ", userName)

/////////////////GETUSERDATA////////////////////
  useEffect(() => {
    getAllBugs([])
    getUserData([])
  }, [])
  console.log(userData[0])

  //modal stuff
  const data = require('./data.json')
  const [modalInfo, setModalInfo] = useState([])
  const [showModal, setShowModal] = useState(false)
  //modal stuff
  const [show, setShow] = useState(false)
  const handleClose = () => setShow(false)
  const handleShow = () => setShow(true)

  //modal stuff
  const rowEvents = {
    onClick: (e, row) => {
      e.preventDefault()
      console.log(row)
      setModalInfo(row)
    },
  }
  //modal stuff
  const toggleTrueFalse = () => {
    setShowModal(handleShow)
  }

  //closes modal without saving the chages
  function closeModalWithOutSaving() {
    handleClose()
  }

  const [bugId, setbugId] = useState('')
  const [creator, setCreator] = useState('')
  const [timeCreated, setTimeCreated] = useState('')
  const [description, setDescription] = useState('')
  const [type, setType] = useState('')
  const [status, setStatus] = useState('')
  const [priority, setPriority] = useState('')
  const [estimatedTime, setEstimatedTime] = useState('')

  const [updatedBugToSendData, setUpdatedBugToSendData] = useState([])

  const [redirect, setRedirect] = useState(false)

  async function closeModalWithSaving(editedModalInfo) {
    const bugToUpdate = {
      bugId: modalInfo.bugId,
      creator: editedModalInfo.creator,
      timeCreated: editedModalInfo.timeCreated,
      type: editedModalInfo.type,
      status: editedModalInfo.status,
      priority: editedModalInfo.priority,
      estimatedTime: editedModalInfo.estimatedTime,
      description: editedModalInfo.description,
    }

    console.log(editedModalInfo.bugId)

    setbugId(editedModalInfo.bugId)
    setCreator('editedModalInfo.creator')
    setDescription(editedModalInfo.description)
    setEstimatedTime(editedModalInfo.estimatedTime)
    setPriority(editedModalInfo.priority)
    setStatus(editedModalInfo.status)
    setTimeCreated(editedModalInfo.timeCreated)
    setType(editedModalInfo.type)

    console.log('this is whats in editedModalInfo ' + bugToUpdate)
    await fetch('https://bugitserver.azurewebsites.net/update-bug', {
      method: 'Post',
      headers: {
        'Content-Type': 'application/json',
      },
      //credentials: 'include',
      body: JSON.stringify(bugToUpdate),
    }).then(function(response) {
      if (response.status === 200) setRedirect(true)
      else alert('Something went wrong')
    })
  }

  //modal stuff
  //modal type priority menu
  const priorityOptions = [
    { label: 'N/A', value: 'N/A' },
    { label: 'High', value: 'High' },
    { label: 'Moderate', value: 'Moderate' },
    { label: 'Low', value: 'Low' },
  ]

  //modal stuff
  //modal type select menu
  const typeOptions = [
    { label: 'N/A', value: 'N/A' },
    { label: 'Optimize', value: 'Optimize' },
    { label: 'Crash', value: 'Crash' },
    { label: 'Upgrade', value: 'Upgrade' },
  ]

  //modal stuff
  //modal status select menu
  const statusOptions = [
    { label: 'N/A', value: 'N/A' },
    { label: 'In Progress', value: 'In Progress' },
    { label: 'Stuck', value: 'Stuck' },
  ]

  //modal stuff
  //auto populates the priority select menu
  function autoPopulatePriority(priority) {
    for (let i = 0; i < priorityOptions.length; i++) {
      if (priorityOptions[i].value === priority) return priorityOptions[i]
    }
    return priorityOptions[0]
  }

  //modal stuff
  //auto populates the type select menu
  function autoPopulateTypeOptions(type) {
    for (let i = 0; i < typeOptions.length; i++) {
      if (typeOptions[i].value === type) return typeOptions[i]
    }
    return typeOptions[0]
  }

  //modal stuff
  //auto populates the status select menu
  function autoPopulateStatusOptions(status) {
    for (let i = 0; i < statusOptions.length; i++) {
      if (statusOptions[i].value === status) return statusOptions[i]
    }
    return statusOptions[0]
  }

  const ModalContent = () => {
    return (
      <Modal className = "card-b color-primary" show={show} onHide={handleClose} size='lg' centered>
        <Modal.Header closeButton>
          <Modal.Title>Bug ID: {modalInfo.bugId}</Modal.Title>
        </Modal.Header>
        <Modal.Body>Are you sure you want to delete this bug?</Modal.Body>
        <Modal.Footer>
          <Button
            variant='primary'
            onClick={() => {
              deleteBug(modalInfo.bugId)
            }}
          >
            Delete Bug
          </Button>
          <Button
            variant='secondary'
            className='text-center btn btn-md btn-cancel'
            type='submit'
            onClick={closeModalWithOutSaving}
          >
            Cancel
          </Button>
        </Modal.Footer>
      </Modal>
    )
  }

  ////////////////////////DELETE BUG///////////////////////////////////////

  function deleteBug(bugId) {
    const deleteUrl = `${'https://bugitserver.azurewebsites.net/delete-bug-by-id'}/${bugId}`
    console.log(deleteUrl)
    fetch(deleteUrl, {
      method: 'DELETE',
    }).then(function(response) {
      window.location.reload(true)
      if (response.status === 200)
        alert(`Bug: ${bugId} has succesfully been deleted.`)
      else alert(`Bug: ${bugId} was not succesfully deleted.`)
    })
  }

  ////////////////////////DELETE BUG///////////////////////////////////////

  const typeSelectOptions = {
    0: 'Optimize',
    1: 'Crash',
    2: 'Upgrade',
  }

  const statusSelectOptions = {
    0: 'In Progress',
    1: 'Stuck',
  }

  const prioritySelectOptions = {
    0: 'High',
    1: 'Moderate',
    2: 'Low',
  }

  const columns = [
    {
      dataField: 'bugId',
      text: 'ID',
      //sort: true,
      filter: textFilter(),
    },
    {
      dataField: 'creator',
      text: 'Owner',
      //sort: true,
      filter: textFilter(),
    },
    {
      dataField: 'timeCreated',
      text: 'Date',
      //sort: true,
      filter: dateFilter(),
    },
    {
      dataField: 'type',
      text: 'Type',
      //sort: true,
      filter: textFilter(),
      // formatter: (cell) => typeSelectOptions[cell],
      // filter: selectFilter({
      //   options: typeSelectOptions,
      // }),
    },
    {
      dataField: 'status',
      text: 'Status',
      filter: textFilter(),
      //sort: true,
      // formatter: (cell) => statusSelectOptions[cell],
      // filter: selectFilter({
      //   options: statusSelectOptions,
      // }),
    },
    {
      dataField: 'priority',
      text: 'Priority',
      filter: textFilter(),
      //sort: true,
      // formatter: (cell) => prioritySelectOptions[cell],
      // filter: selectFilter({
      //   options: prioritySelectOptions,
      // }),
    },
    {
      dataField: 'estimatedTime',
      text: 'Estimated Time',
      //sort: true,
      
    },
    {
      dataField: 'description',
      text: 'Description',
      //sort: true,
      
    },
    {
      text: 'Modify',
      formatter: (cell, row, rowIndex, extraData) => (
        <div>
          <Link to={'/UpdateBug/' + row.bugId}>
            <IconButton aria-label='update'>
              <FaEdit />
            </IconButton>
          </Link>
          <IconButton aria-label='delete' onClick={toggleTrueFalse}>
            <RiDeleteBin5Fill />
          </IconButton>
        </div>
      ),
    },
  ]

  // const defaultSorted = [
  //   {
  //     dataField: 'name',
  //     order: 'desc',
  //   },
  // ];

  const options = {
    sizePerPageList: [
      {
        text: '5',
        value: 5,
      },
      {
        text: '10',
        value: 10,
      },
      {
        text: 'All',
        value: bugData.length,
      },
    ], // A numeric array is also available. the purpose of above example is custom the text
  }

  return (
    <React.Fragment>
     
      <h3 className="mt-5">Welcome: {userName}</h3>
      <div className= "table-responsive" >
      <BootstrapTable
        keyField='id'
        data={bugData}
        columns={columns}
        
        classes= {bTable}
        //expandRow={expandRow}
        //defaultSorted={defaultSorted}
        pagination={paginationFactory(options)}
        rowEvents={rowEvents}
        filter={filterFactory()}        
      />
      </div>
      {show ? <ModalContent /> : null}
    </React.Fragment>
  )
}
export default Home
