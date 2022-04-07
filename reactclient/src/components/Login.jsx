import React, { useState, useEffect } from 'react'
import './CSS/Login.css'
import BugItLogo from './images/BugItLogo.jpg'
import { Navigate } from 'react-router-dom'

const Login = () => {
  const [emailAddress, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [redirect, setRedirect] = useState(false)
  const [flag, setFlag] = useState(1)
  const submit = async (e) => {
    e.preventDefault()

    await fetch('https://localhost:7075/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include',
      body: JSON.stringify({
        emailAddress,
        password,
      }),
    }).then(function (response) {
      if (response.status === 200) setRedirect(true)
      else setFlag(2)
    })
  }
  if (redirect) return <Navigate to='/#home' />
  return (
    <React.Fragment>
      <body className='text-center'>
        <div className='container'>
          <main className='form-signin'>
            <img
              className='logo'
              rel='icon'
              src={BugItLogo}
              alt='Logo'
              width='100px'
              height='100px'
            />
            <div className='card o-hidden border-0 shadow-lg my-5'>
              <div className='p-5'>
                <form onSubmit={submit}>
                  <h1 className='h3 mb-3 fw-normal'>Please Sign In</h1>
                  {flag === 2 && <p>Your login credentials are invalid, please try again.</p>}
                  <div className='form-floating'>
                    <input
                      type='email'
                      className='form-control'
                      id='emailInput'
                      placeholder='name@example.com'
                      onChange={(e) => setEmail(e.target.value)}
                    ></input>
                    <label for='emailInput'>Email address</label>
                  </div>
                  <div className='form-floating'>
                    <input
                      type='password'
                      className='form-control'
                      id='passwordInput'
                      placeholder='Password'
                      required
                      onChange={(e) => setPassword(e.target.value)}
                    ></input>
                    <label for='passwordInput'>Password</label>
                  </div>
                  <div className='form-check'>
                    <input
                      className='form-check-input'
                      type='checkbox'
                      value=''
                      id='flexCheckChecked'
                    />
                    <label className='form-check-label' for='flexCheckChecked'>
                      Remember me
                    </label>
                  </div>
                  <button
                    className='w-100 btn btn-md btn-primary'
                    type='submit'
                  >
                    Sign In
                  </button>
                  <div className='border-top my-3'></div>
                  <div>
                    <label className='signuplabel'>Dont Have an Account?</label>
                    <a
                      href='/createAccount'
                      className='w-100 btn btn-md btn-primary'
                      type='submit'
                    >
                      Create Account
                    </a>
                  </div>
                  <p className='mt-5 mb-3 text-muted'>&copy; BugIt 2022</p>
                </form>
              </div>
            </div>
          </main>
        </div>
      </body>
    </React.Fragment>
  )
}

export default Login