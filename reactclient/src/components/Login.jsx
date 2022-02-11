import React from 'react'
import './Login.css'
import BugItLogo from './BugItLogo.jpg'

export const Login = () => (
  <React.Fragment>
    <body class='text-center'>
      <main class='form-signin'>
        <form>
          <img
            class='logo'
            rel='icon'
            src={BugItLogo}
            alt='Logo'
            width='100px'
            height='100px'
          />
          <h1 class='h3 mb-3 fw-normal'>Please Sign In</h1>
          <div class='form-floating'>
            <input
              type='email'
              class='form-control'
              id='floatingInput'
              placeholder='name@example.com'
            ></input>
            <label for='floatingInput'>Email address</label>
          </div>

          <div class='form-floating'>
            <input
              type='password'
              class='form-control'
              id='floatingPassword'
              placeholder='Password'
            ></input>
            <label for='floatingPassword'>Password</label>
          </div>
          <div class='form-check'>
            <input
              class='form-check-input'
              type='checkbox'
              value=''
              id='flexCheckChecked'
            />
            <label class='form-check-label' for='flexCheckChecked'>
              Remember me
            </label>
          </div>
          <a href='/#home' class='w-100 btn btn-md btn-primary' type='submit'>
            Sign In
          </a>
          <div class='border-top my-3'></div>
          <div>
            <label class='signuplabel'>Dont Have an Account?</label>
            <a
              href='/signup'
              class='w-100 btn btn-md btn-primary'
              type='submit'
            >
              Create Account
            </a>
          </div>
          <p class='mt-5 mb-3 text-muted'>&copy; BugIt 2022</p>
        </form>
      </main>
    </body>
  </React.Fragment>
)
