///import { render } from '@testing-library/react'
//import BugItLogo from './images/BugItLogo.jpg';
import './CSS/home.css'
import { BrowserRouter as Router, Link } from 'react-router-dom'
import React, { useState, useEffect } from 'react'
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
const Home = (props) => {
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

  useEffect(() => {
    getAllBugs([])
    getUserData([])
  }, [])
  console.log(userData[0]);
  

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
      <Modal show={show} onHide={handleClose} size='lg' centered>
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
      sort: true,
      filter: textFilter(),
    },
    {
      dataField: 'creator',
      text: 'Name',
      sort: true,
      filter: textFilter(),
    },
    {
      dataField: 'timeCreated',
      text: 'Date',
      sort: true,
      filter: dateFilter(),
    },
    {
      dataField: 'type',
      text: 'Type',
      sort: true,
      // formatter: (cell) => typeSelectOptions[cell],
      // filter: selectFilter({
      //   options: typeSelectOptions,
      // }),
    },
    {
      dataField: 'status',
      text: 'Status',
      sort: true,
      // formatter: (cell) => statusSelectOptions[cell],
      // filter: selectFilter({
      //   options: statusSelectOptions,
      // }),
    },
    {
      dataField: 'priority',
      text: 'Priority',
      sort: true,
      // formatter: (cell) => prioritySelectOptions[cell],
      // filter: selectFilter({
      //   options: prioritySelectOptions,
      // }),
    },
    {
      dataField: 'estimatedTime',
      text: 'Estimated Time',
      sort: true,
      filter: textFilter(),
    },
    {
      dataField: 'description',
      text: 'Description',
      sort: true,
      filter: textFilter(),
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
        value: data.length,
      },
    ], // A numeric array is also available. the purpose of above example is custom the text
  }

  return (
    <React.Fragment>
      <BootstrapTable
        keyField='id'
        data={bugData}
        columns={columns}
        classes=' table-hover table-bordered table-striped'
        //expandRow={expandRow}
        //defaultSorted={defaultSorted}
        pagination={paginationFactory(options)}
        rowEvents={rowEvents}
        filter={filterFactory()}
      />
      {show ? <ModalContent /> : null}

      {/* {show ? <ModalContent /> : null} 
       <div class="border-top my-3"></div>
      <Form className="user">
        <div className="form-group row">
          <div className="col-sm-6 mb-3 mb-sm-0">
            <input
            
              type="text"
              required
              defaultValue={modalInfo.creator}
              onChange={(e) => setCreator(e.target.value)}
              className="form-control"
              placeholder="Owner"
            />
          </div>
          <div className="col-sm-6">
            <input
              type="text"
              required
              defaultValue={modalInfo.date}
              onChange={(e) => setTimeCreated(e.target.value)}
              className="form-control"
              
            />
          </div>
        </div>
        <div className="form-group row">
          <div className="col-sm-6 ">
            <input
              type="text"
              required
              defaultValue={modalInfo.type}
              //onChange={(e) => setType(e.target.value)}
              className="form-control"
              placeholder="Set Type"
            />

            {/* <Select
                       options={this.state.typeOptions} 
                       onChange = {setStatus(this.handleChange.bind(this))}
                       placeholder = 'Set Type' 
                       />     
          </div>
          <div className="col-sm-6 ">
            <input
              type="text"
              required
              defaultValue={modalInfo.status}
              //onChange={(e) => setStatus(e.target.value)}
              className="form-control"
              placeholder="Set Status"
            />
            {/*<Select
                       options={statusOptions} 
                       onChange = {setStatus}
                       placeholder = 'Set status'
                       />  
          </div>
        </div>
        <div className="form-group row">
          <div className="col-sm-6">
            <input
              type="text"
              required
              defaultValue={modalInfo.priority}
              //onChange={(e) => setPriority(e.target.value)}
              className="form-control"
              placeholder="Set Priority"
            />

            {/*<Select
                       options={priorityOptions} 
                       onChange = {setPriority}
                       placeholder = 'Set Priority'                      
                       /> 
          </div>
          <div className="col-sm-6">
            <input
              type="text"
              required
              defaultValue={modalInfo.estimatedTime}
              //onChange={(e) => setEstimatedTime(e.target.value)}
              className="form-control"
              placeholder="Estimated Time"
            />
          </div>
          <div className="col-sm-12">
            <input
              type="text"
              required
              defaultValue={modalInfo.description}
              //onChange={(e) => setBugDescription(e.target.value)}
              className="form-control text-center"
              placeholder="Bug description"
            />
          </div>
        </div>
        <div className="row justify-content-center">
          <button className="text-center btn btn-md btn-primary" type="submit">
            Create Bug
          </button>
        </div>
      </Form> */}
    </React.Fragment>
  )
}
export default Home
