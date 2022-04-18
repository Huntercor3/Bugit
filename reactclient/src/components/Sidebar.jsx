import React from 'react'
import {
  BrowserRouter as Router,
  Route,
  Routes,
  Link,
  Outlet,
} from 'react-router-dom'
import './CSS/home.css'

export const Sidebar = () => (
  <React.Fragment>
    <div className='container-fluid'>
      <div className='row'>
        {/* className="d-flex flex-column flex-shrink-0 bg-light" */}
        <nav
          id='sidebarMenu'
          className='col-md-3 col-lg-2 d-md-block bg-light sidebar collapse'
          style={{ float: 'left' }}
        >
          <ul className='nav nav-pills nav-flush flex-column mb-auto text-center'>
            <li>
              <a
                href='/login'
                className='nav-link active py-3 border-bottom'
                aria-current='page'
                title='login'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                Login
              </a>
            </li>
            <li>
              <a
                href='/createAccount'
                className='nav-link active py-3 border-bottom'
                aria-current='page'
                title='signup'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                Sign up
              </a>
            </li>
            <li>
              <a
                // href points refer to the destination ex: www.abc.com/home   or.com/about
                href='/#home'
                className='nav-link py-3 border-bottom'
                title='Home'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                Home
              </a>
            </li>
            <li>
              <a
                href='/about'
                className='nav-link py-3 border-bottom'
                title='About'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                About
              </a>
            </li>
            
            <li>
              <a
                href='/newCreateAccount'
                className='nav-link active py-3 border-bottom'
                aria-current='page'
                title='signup'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                {' '}
              </a>
            </li>
            <li>
              <a
                href='/nomatch'
                className='nav-link py-3 border-bottom'
                title='NoMatch'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                {' '}
              </a>
            </li>
            <li>
              <a
                href='/nomatch'
                className='nav-link py-3 border-bottom'
                title='NoMatch'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                {' '}
              </a>
            </li>
            <li>
              <a
                href='/nomatch'
                className='nav-link py-3 border-bottom'
                title='NoMatch'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                {' '}
              </a>
            </li>
            <li>
              <a
                href='/nomatch'
                className='nav-link py-3 border-bottom'
                title='NoMatch'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                {' '}
              </a>
            </li>
            <li></li>
            <a
              href='/account'
              className='nav-link py-3 border-bottom'
              title='Account'
              data-bs-toggle='tooltip'
              data-bs-placement='right'
            >
              Account
            </a>
          </ul>
        </nav>
        <main className='col-md-9 ms-sm-auto col-lg-11'>
          {/* An <Outlet> renders whatever child route is currently active,
            so you can think about this <Outlet> as a placeholder for
            the child routes we defined above. */}
          <Outlet />
        </main>
      </div>
    </div>
  </React.Fragment>
)