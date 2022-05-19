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
import { IconButton } from '@material-ui/core'
//Icons
import { RiDeleteBin5Fill } from 'react-icons/ri'
import { FaRegEdit, FaEdit } from 'react-icons/fa'
import { ThemeContext } from '../App'

const Home = (props) => {
  /////////////////SETTINGTABLEDARK////////////////////
  const { setTheme, theme } = useContext(ThemeContext)
  const [DarkContext, setDarkContext] = useState(true)
  var bModalLight = 'light-Modal'
  var bModalDark = 'dark-Modal'
  var bModal
  var bTableInputFalse = 'table-hover table-bordered table-striped'
  var bTableInputTrue = 'table-hover table-bordered table-striped table-dark'
  var bTable
  console.log('theme', theme)
  if (theme === 'light') {
    bTable = bTableInputFalse
    bModal = bModalLight
  } else {
    bTable = bTableInputTrue
    bModal = bModalDark
  }

  console.log('Bool', DarkContext)
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
  const getUserUrl = 'https://bugitserver.azurewebsites.net/GetCookie'
  const [userData, setUserData] = useState([])
  const [userName, setUserName] = useState('')

  async function getUserData() {
    await fetch(getUserUrl, {
      method: 'GET',
    })
      .then(function(resp) {
        return resp.json()
      })

      .then(async function(data) {
        setUserData(data)
        console.log('data sent in func ', data)
        console.log(data[0].value)
        setUserName(data[0].value)
      })
  }
  console.log('UserName value: ', userName)

  /////////////////GETUSERDATA////////////////////
  useEffect(() => {
    getAllBugs([])
    getUserData([])
  }, [])
  console.log(userData[0])

  //modal stuff
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

  const ModalContent = () => {
    return (
      <Modal
        className='card-b color-primary'
        show={show}
        onHide={handleClose}
        size='lg'
        centered
      >
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
  const columns = [
    {
      dataField: 'bugId',
      text: 'ID',
      filter: textFilter(),
    },
    {
      dataField: 'creator',
      text: 'Name',
      filter: textFilter(),
    },
    {
      dataField: 'timeCreated',
      text: 'Date',
      filter: dateFilter(),
    },
    {
      dataField: 'type',
      text: 'Type',
      filter: textFilter(),
    },
    {
      dataField: 'status',
      text: 'Status',
      filter: textFilter(),
    },
    {
      dataField: 'priority',
      text: 'Priority',
      filter: textFilter(),
    },
    {
      dataField: 'estimatedTime',
      text: 'Estimated Time',
    },
    {
      dataField: 'description',
      text: 'Description',
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
      <h3 className='mt-5'>Welcome: {userName}</h3>
      <div className='table-responsive'>
        <BootstrapTable
          keyField='id'
          data={bugData}
          columns={columns}
          classes={bTable}
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
