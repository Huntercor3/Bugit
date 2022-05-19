import React, { useState, useEffect } from 'react'
import { Navigate } from 'react-router-dom'
import Select from 'react-select'
import './CSS/CreateBug.css'
import BugItLogo from './images/BugItLogo.png'
import { Modal, Button, Form } from 'react-bootstrap'
export default function BugUpdateForm() {
  //////////////////////////////////GET BUG BY ID//////////////////////////////////////////
  //

  //set bugIDToSearch to the URL location
  var bugIDToSearch = window.location.pathname
  //set bugIDToSearch to the bug ID       deletes "/showbug/"
  bugIDToSearch = bugIDToSearch.substring(11)
  console.log('Bug ID to Seach: ', bugIDToSearch)

  var myInit = {
    method: 'POST',
    Headers: {
      'Content-Type': 'application/json',
    },
    mode: 'cors',
    cache: 'default',
  }

  const getBugUrl = `${'https://bugitserver.azurewebsites.net/get-bug-by-bug-id'}/${bugIDToSearch}`

  let myRequest = new Request(getBugUrl, myInit)

  const [bugData, setBugData] = useState({})
  const [bugCurrentlyBeingUpdated, setBugCurrentlyBeingUpdated] = useState({})
  const bugId = bugIDToSearch
  const [creator, setOwner] = useState(0)
  const [description, setBugDescription] = useState('')
  const [type, setType] = useState('')
  const [status, setStatus] = useState('')
  const [priority, setPriority] = useState('')
  const [estimatedTime, setEstimatedTime] = useState('')
  const [redirect, setRedirect] = useState(false)
  async function getBugById() {
    await fetch(getBugUrl, {
      method: 'GET',
    })
      .then((response) => response.json())
      .then((bugFromServer) => {
        setBugData(bugFromServer)
        setPriority(bugFromServer.priority)
        setOwner(bugFromServer.creator)
        setType(bugFromServer.type)
        setEstimatedTime(bugFromServer.estimatedTime)
        setStatus(bugFromServer.status)
        setBugDescription(bugFromServer.description)
      })
      .then(function(data) {
        console.log('Data: ', data)
      })
  }

  useEffect(() => {
    getBugById({})
  }, [])

  //////////////////////////////////GET BUG BY ID//////////////////////////////////////////

  //////////////////////////////////UPDATE BUG//////////////////////////////////////////

  const submit = async (e) => {
    e.preventDefault()

    await fetch('https://bugitserver.azurewebsites.net/update-bug', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include',
      body: JSON.stringify({
        bugId: bugId,
        creator: creator,
        description: description,
        type: type,
        status: status,
        priority: priority,
        estimatedTime: estimatedTime,
      }),
    }).then(function(response) {
      console.log(response.status)
      if (response.status === 200) setRedirect(true)
      else alert('Invalid credientials, please try again')
    })
    console.log(
      bugId,
      creator,
      type,
      status,
      priority,
      estimatedTime,
      description
    )
  }
  if (redirect) return <Navigate to='/Home' />

  //////////////////////////////////UPDATE BUG//////////////////////////////////////////

  const priorityOptions = [
    { label: 'High', value: 'High' },
    { label: 'Moderate', value: 'Moderate' },
    { label: 'Low', value: 'Low' },
  ]

  const typeOptions = [
    { label: 'Optimize', value: 'Optimize' },
    { label: 'Crash', value: 'Crash' },
    { label: 'Upgrade', value: 'Upgrade' },
  ]

  const statusOptions = [
    { label: 'In progress', value: 'In progress' },
    { label: 'Stuck', value: 'Stuck' },
  ]

  //////////////////////////////////REMOVE HTML FROM OUTPUT//////////////////////////////////////////
  function removeHTML(str) {
    var tmp = document.createElement('DIV')
    tmp.innerHTML = str
    return tmp.textContent || tmp.innerText || ''
  }
  //////////////////////////////////REMOVE HTML FROM OUTPUT//////////////////////////////////////////
  return (
    <React.Fragment>
      <div className='container'>
        <div className='row'>
          <img
            className='logo'
            rel='icon'
            src={BugItLogo}
            alt='Logo'
            width='100px'
            height='100px'
          />
        </div>
        <div
          className='card-b o-hidden border-0 shadow-lg my-5'
          style={{ marginTop: '5rem!important' }}
        >
          <div className='card-body-b p-0'>
            <div className='row'>
              <div className='col-lg-12'>
                <div className='p-5-b'>
                  <div className='text-center'>
                    <label className='h2 form-label'>
                      Updating the Bug with ID: {bugIDToSearch}
                    </label>
                  </div>
                  <form onSubmit={submit} className='user'>
                    <div className='form-group row'>
                      <div className='col-sm-6 mb-3 mb-sm-0'>
                        <label className='h5 form-label'>Owner</label>
                        <input
                          type='text'
                          required
                          defaultValue={bugData.creator}
                          onChange={(e) => setOwner(e.target.value)}
                          className='form-control'
                          //placeholder={bugData.creator}
                        />
                      </div>
                      <div className='col-sm-6'>
                        <label className='h5 form-label'>Type</label>
                        <Select
                          options={typeOptions}
                          onChange={(e) => setType(e.value)}
                          placeholder={bugData.type}
                        />
                      </div>
                    </div>
                    <div className='form-group row'>
                      <div className='col-sm-6 '>
                        <label className='h5 form-label'>Status</label>
                        <Select
                          options={statusOptions}
                          onChange={(e) => setStatus(e.value)}
                          placeholder={bugData.status}
                        />
                      </div>
                      <div className='col-sm-6 '>
                        <label className='h5 form-label'>Priority</label>
                        <Select
                          options={priorityOptions}
                          onChange={(e) => setPriority(e.value)}
                          placeholder={bugData.priority}
                        />
                      </div>
                    </div>
                    <div className='form-group row'>
                      <div className='col-sm-6'>
                        <label className='h5 form-label text-center'>
                          Estimated Time
                        </label>
                        <input
                          type='text'
                          required
                          defaultValue={bugData.estimatedTime}
                          onChange={(e) => setEstimatedTime(e.target.value)}
                          className='form-control'
                        />
                      </div>
                      <div className='col-sm-12'>
                        <label className='h5 form-label'>Bug Description</label>
                        <textarea
                          required
                          defaultValue={bugData.description}
                          onChange={(e) => setBugDescription(e.target.value)}
                          className='form-control'
                        ></textarea>
                      </div>
                    </div>
                    <div className='row justify-content-center'>
                      <Button
                        className='text-center btn btn-md btn-primary'
                        type='submit'
                        onClick={submit}
                      >
                        Update
                      </Button>
                      <Button
                        variant='secondary'
                        className='text-center btn btn-md btn-cancel'
                        type='submit'
                      >
                        Cancel
                      </Button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </React.Fragment>
  )
}
