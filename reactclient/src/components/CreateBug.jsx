import React, { useState } from 'react'
import { Navigate } from 'react-router-dom'
import './CSS/CreateAccount.css'
import BugItLogo from './images/BugItLogo.jpg'
import Select from 'react-select'


const priorityOptions=[
{label:'High', value:'high'},
{label:'Moderate', value:'moderate'},
{label:'Low', value:'low'}
]

const typeOptions=[
{label:'Optimize', value:'optimize'},
{label:'Crash', value:'crash'},
{label:'Upgrade', value:'upgrade'}
]

const statusOptions=[
{label:'In progress', value:'inProgress'},
{label:'Stuck', value:'stuck'}
]



const CreateBug = () => {
  
  const [owner, setOwner] = useState('')
  const [date, setDate] = useState('')
  const [type, setType] = useState('')
  const [status, setStatus] = useState('')
  const [priority, setPriority] = useState('')
  const [estimatedTime, setEstimatedTime] = useState('')
  const [bugDescription, setBugDescription] = useState('')
  const [redirect, setRedirect] = useState(false)
  const submit = async (e) => {
    e.preventDefault()

    await fetch('https://localhost:7075/Register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include',
      body: JSON.stringify({
        owner: owner,
        date: date,
        type: type,
        status: status,
        priority: priority,
        estimatedTime: estimatedTime,
        bugDescription: bugDescription
      }),
    }).then(function (response) {
      if (response.status == 200) setRedirect(true)
      else alert('Invalid credientials, please try again')
    })
  }
  if (redirect) return <Navigate to='/home' />


  /*onChangeFunc=({value}) =>
  {
    (e) => {value(e.target.value)}
  }
*/
  

  return (
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
        {/* <div className='col-sm-12 btn btn-primary' style={{ margin: '6px' }}>
          Add New Contact
        </div> */}
      </div>
      <div
        className='card o-hidden border-0 shadow-lg my-5'
        style={{ marginTop: '5rem!important' }}
      >
        <div className='card-body p-0'>
          <div className='row'>
            <div className='col-lg-12'>
              <div className='p-5'>
                <div className='text-center'>
                  <h1 className='h4 text-gray-900 mb-4'>Create a New Bug</h1>
                </div>
                <form onSubmit={submit} className='user'>
                  <div className='form-group row'>
                    <div className='col-sm-6 mb-3 mb-sm-0'>
                      <input
                        type='text'
                        required
                        onChange = {(e) => setOwner(e.target.value)}
                        className='form-control'
                        placeholder='Owner'
                      />
                    </div>
                    <div className='col-sm-6'>
                      <input
                        type='date'
                        required
                        onChange = {(e) => setDate(e.target.value)}
                        className='form-control'
                        placeholder='Todays date'
                      />
                    </div>
                  </div>
                  <div className='form-group row'>
                    <div className='col-sm-6 '>
                    <Select
                       options={typeOptions} 
                       onChange = {setType}
                       placeholder = 'Set Type' 
                       />         
                    </div>
                    <div className='col-sm-6 '>
                      <Select
                       options={statusOptions} 
                       onChange = {setStatus}
                       placeholder = 'Set status'
                       />                    
                    </div>
                  </div>
                  <div className='form-group row'>
                    <div className='col-sm-6'>
                    <Select
                       options={priorityOptions} 
                       onChange = {setPriority}
                       placeholder = 'Set Priority'
                       
                       />              
                    </div>
                    <div className='col-sm-6'>
                      <input
                        type='text'
                        required
                        onChange = {(e) => setEstimatedTime(e.target.value)}
                        className='form-control'
                        placeholder='Estimated Time'
                      />
                    </div>
                    <div className='col-sm-12'>
                      <input
                        type='text'
                        required
                        onChange = {(e) => setBugDescription(e.target.value)}
                        
                        className="form-control text-center"
                        placeholder='Bug description'
                      />
                    </div>
                    
                  </div>
                  <div className='row justify-content-center'>
                    <button
                      className='text-center btn btn-md btn-primary'
                      type='submit'
                    >
                      Create Bug
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
export default CreateBug
