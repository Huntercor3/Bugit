import React, { useState } from 'react'
import { Navigate } from 'react-router-dom'
import './CSS/CreateBug.css'
import BugItLogo from './images/BugItLogo.jpg'

const NewCreateAccount = () => {
  const [emailAddress, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [redirect, setRedirect] = useState(false)
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [phoneNumber, setPhoneNumber] = useState('')

  const submit = async (e) => {
    e.preventDefault()

    await fetch('https://localhost:7075/registerController', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include',
      body: JSON.stringify({
        emailAddress: emailAddress,
        password: password,
        firstName: firstName,
        lastName: lastName,
        phoneNumber: phoneNumber,
      }),
    }).then(function(response) {
      if (response.status === 200) setRedirect(true)
      else alert('Invalid credientials, please try again')
    })
  }
  if (redirect) return <Navigate to='/login' />

  return (
    <div className='container'>
      <div class='row'>
        <img
          class='logo'
          rel='icon'
          src={BugItLogo}
          alt='Logo'
          width='100px'
          height='100px'
        />
        {/* <div class='col-sm-12 btn btn-primary' style={{ margin: '6px' }}>
          Add New Contact
        </div> */}
      </div>
      <div
        className='card-b o-hidden border-0 shadow-lg my-5'
        style={{ marginTop: '5rem!important;' }}
      >
        <div className='card-body-b p-0'>
          <div className='row'>
            <div className='col-lg-12'>
              <div className='p-5-b'>
                <div className='text-center'>
                  <h1 className='h4 text-gray-900 mb-4'>Create a New User!</h1>
                </div>
                <form onSubmit={submit} className='user'>
                  <div className='form-group row'>
                    <div className='col-sm-6 mb-3 mb-sm-0'>
                    <label className="h5 form-label">Email Address</label>
                      <input
                        type='email'
                        required
                        onChange={(e) => setEmail(e.target.value)}
                        className='form-control'
                        placeholder='Email'
                      />
                    </div>
                    <div className='col-sm-6'>
                    <label className="h5 form-label">Password</label>
                      <input
                        type='password'
                        required
                        onChange={(e) => setPassword(e.target.value)}
                        className='form-control'
                        placeholder='Password'
                      />
                    </div>
                  </div>
                  <div className='form-group row'>
                    <div className='col-sm-6'>
                    <label className="h5 form-label">First Name</label>
                      <input
                        type='firstName'
                        required
                        onChange={(e) => setFirstName(e.target.value)}
                        className='form-control'
                        placeholder='First Name'
                      />
                    </div>
                    <div className='col-sm-6'>
                    <label className="h5 form-label">Last Name</label>
                      <input
                        type='lastName'
                        required
                        onChange={(e) => setLastName(e.target.value)}
                        className='form-control'
                        placeholder='Last Name'
                      />
                    </div>
                  </div>
                  <div class='form-group row'>
                    <div className='col-sm-6'>
                    <label className="h5 form-label">Phone Number</label>
                      <input
                        type='phoneNumber'
                        required
                        onChange={(e) => setPhoneNumber(e.target.value)}
                        className='form-control'
                        placeholder='Phone Number'
                      />
                    </div>
                  </div>
                  <div className='row justify-content-center'>
                    <button
                      className='text-center btn btn-md btn-primary'
                      type='submit'
                    >
                      Create User
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
export default NewCreateAccount
