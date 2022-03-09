import React, { useState } from 'react'
import { Navigate } from 'react-router-dom'
import './CSS/CreateAccount.css'
import BugItLogo from './images/BugItLogo.jpg'

const NewCreateAccount = () => {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [redirect, setRedirect] = useState(false)
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [phoneNumber, setPhoneNumber] = useState('')
  const [hardware, setHardware] = useState('')
  const [role, setRole] = useState('')

  const submit = async (e) => {
    e.preventDefault()

    await fetch('https://localhost:7075/Register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include',
      body: JSON.stringify({
        email: email,
        password: password,
        firstName: firstName,
        lastName: lastName,
        phoneNumber: phoneNumber,
        hardware: hardware,
        role: role,
      }),
    }).then(function (response) {
      if (response.status === 200) setRedirect(true)
      else alert('Invalid credientials, please try again')
    })
  }
  if (redirect) return <Navigate to='/login1' />

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
        className='card o-hidden border-0 shadow-lg my-5'
        style={{ marginTop: '5rem!important;' }}
      >
        <div className='card-body p-0'>
          <div className='row'>
            <div className='col-lg-12'>
              <div className='p-5'>
                <div className='text-center'>
                  <h1 className='h4 text-gray-900 mb-4'>Create a New User!</h1>
                </div>
                <form onSubmit={submit} className='user'>
                  <div className='form-group row'>
                    <div className='col-sm-6 mb-3 mb-sm-0'>
                      <input
                        type='email'
                        required
                        onChange={(e) => setEmail(e.target.value)}
                        className='form-control'
                        placeholder='Email'
                      />
                    </div>
                    <div className='col-sm-6'>
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
                      <input
                        type='firstName'
                        required
                        onChange={(e) => setFirstName(e.target.value)}
                        className='form-control'
                        placeholder='First Name'
                      />
                    </div>
                    <div className='col-sm-6'>
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
                      <input
                        type='phoneNumber'
                        required
                        onChange={(e) => setPhoneNumber(e.target.value)}
                        className='form-control'
                        placeholder='Phone Number'
                      />
                    </div>
                    <div class='col-sm-6'>
                      <input
                        type='text'
                        name='test'
                        /*value={data.PhoneNumber}*/
                        class='form-control'
                        id='test'
                        placeholder='test'
                      />
                    </div>
                    <div className='col-sm-6'>
                      <input
                        type='hardware'
                        required
                        onChange={(e) => setHardware(e.target.value)}
                        className='form-control'
                        placeholder='Hardware'
                      />
                    </div>
                    <div className='col-sm-6'>
                      <input
                        type='role'
                        required
                        onChange={(e) => setRole(e.target.value)}
                        className='form-control'
                        placeholder='Role'
                      />
                    </div>
                  </div>
                  <button type='submit' className='btn btn-primary  btn-block'>
                    Create User
                  </button>
                  <hr />
                </form>
                <hr />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
export default NewCreateAccount
