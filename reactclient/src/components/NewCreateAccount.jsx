import React, { useState } from 'react'
import BugItLogo from './images/BugItLogo.jpg'
import axios from 'axios'
import './CSS/CreateAccount.css'

function NewCreateAccount(props) {
  const [data, setdata] = useState({
    Email: '',
    Password: '',
    FirstName: '',
    LastName: '',
    PhoneNumber: '',
    Hardware: '',
    Role: '',
  })
  const apiUrl = 'http://localhost:1680/api/employee/InsertEmployee'
  const Registration = (e) => {
    e.preventDefault()
    debugger
    const data1 = {
      Email: data.Email,
      Password: data.Password,
      FirstName: data.FirstName,
      LastName: data.LastName,
      PhoneNumber: data.PhoneNumber,
      Hardware: data.Hardware,
      Role: data.Role,
    }
    axios.post(apiUrl, data1).then((result) => {
      debugger
      console.log(result.data)
      if (result.data.Status == 'Invalid') alert('Invalid User')
      else props.history.push('/Dashboard')
    })
  }
  const onChange = (e) => {
    e.persist()
    debugger
    setdata({ ...data, [e.target.name]: e.target.value })
  }
  return (
    <div class='container'>
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
        class='card o-hidden border-0 shadow-lg my-5'
        style={{ marginTop: '5rem!important;' }}
      >
        <div class='card-body p-0'>
          <div class='row'>
            <div class='col-lg-12'>
              <div class='p-5'>
                <div class='text-center'>
                  <h1 class='h4 text-gray-900 mb-4'>Create a New User</h1>
                </div>
                <form onSubmit={Registration} class='user'>
                  <div class='form-group row'>
                    <div class='col-sm-6 mb-3 mb-sm-0'>
                      <input
                        type='text'
                        name='Email'
                        onChange={onChange}
                        value={data.Email}
                        class='form-control'
                        id='exampleEmail'
                        placeholder='Email'
                      />
                    </div>
                    <div class='col-sm-6'>
                      <input
                        type='Password'
                        name='Password'
                        onChange={onChange}
                        value={data.Password}
                        class='form-control'
                        id='examplePassword'
                        placeholder='Password'
                      />
                    </div>
                  </div>
                  <div class='form-group row'>
                    <div class='col-sm-6'>
                      <input
                        type='text'
                        name='FirstName'
                        onChange={onChange}
                        value={data.FirstName}
                        class='form-control'
                        id='exampleFirstName'
                        placeholder='First Name'
                      />
                    </div>
                    <div class='col-sm-6'>
                      <input
                        type='text'
                        name='LastName'
                        onChange={onChange}
                        value={data.LastName}
                        class='form-control'
                        id='exampleLastName'
                        placeholder='Last Name'
                      />
                    </div>
                  </div>
                  <div class='form-group row'>
                    <div class='col-sm-6'>
                      <input
                        type='text'
                        name='PhoneNumber'
                        onChange={onChange}
                        value={data.PhoneNumber}
                        class='form-control'
                        id='examplePhoneNumber'
                        placeholder='Phone Number'
                      />
                    </div>
                    <div class='col-sm-6'>
                      <input
                        type='text'
                        name='test'
                        onChange={onChange}
                        /*value={data.PhoneNumber}*/
                        class='form-control'
                        id='test'
                        placeholder='test'
                      />
                    </div>
                    <div class='col-sm-6'>
                      <input
                        type='text'
                        name='Hardware'
                        onChange={onChange}
                        value={data.Hardware}
                        class='form-control'
                        id='exampleHardware'
                        placeholder='Hardware'
                      />
                    </div>
                    <div class='col-sm-6'>
                      <input
                        type='text'
                        name='Role'
                        onChange={onChange}
                        value={data.Role}
                        class='form-control'
                        id='exampleRole'
                        placeholder='Role'
                      />
                    </div>
                  </div>
                  <a
                    href='/#home'
                    type='submit'
                    class='btn btn-primary  btn-block'
                  >
                    Create User
                  </a>
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
