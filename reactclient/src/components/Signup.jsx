import React from 'react'
import './Login.css'

export const Signup = () => (
  <React.Fragment>
    <h1>Sign up</h1>
    <body class='text-center'>
      <img
        class='BugItLogo'
        src='/BugItLogo.jpg'
        alt='BugItLogotest'
        height='250'
        width='350'
      ></img>
      {/* class="form-floating col row g-3" */}
      <form class='row g-3'>
        <div class='row'>
          <div class='col-md-2'>
            <label for='inputFirstName' class='form-label'>
              First Name
            </label>
            <input
              type='text'
              class='form-control'
              id='inputFirstName'
              placeholder='John'
            />
          </div>
          <div class='col-md-2'>
            <label for='inputLastName' class='form-label'>
              Last Name
            </label>
            <input
              type='text'
              class='form-control'
              id='inputLastName'
              placeholder='Doe'
            />
          </div>
        </div>
        <div class='row'>
          <div class='col-md-2'>
            <label for='inputEmail' class='form-label'>
              Email
            </label>
            <input
              type='email'
              class='form-control'
              id='inputAddress'
              placeholder='name@example.com'
            />
          </div>
          <div class='col-md-2'>
            <label for='inputPhoneNumber' class='form-label'>
              Phone Number
            </label>
            <input
              type='text'
              class='form-control'
              id='inputPhoneNumber'
              placeholder='(XXX)XXX-XXXX'
              pattern='[0-9]{3}-[0-9]{2}-[0-9]{3}'
            />
          </div>
        </div>
        <div class='row'>
          <div class='col-md-2'>
            <label for='inputCompany' class='form-label'>
              Company
            </label>
            <input
              type='text'
              class='form-control'
              id='inputCompany'
              placeholder='BugIt'
            />
          </div>
        </div>
      </form>
    </body>
  </React.Fragment>
)
