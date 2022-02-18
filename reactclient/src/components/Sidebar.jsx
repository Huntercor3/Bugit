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
    <div class='container-fluid'>
      <div class='row'>
        {/* class="d-flex flex-column flex-shrink-0 bg-light" */}
        <nav
          id='sidebarMenu'
          class='col-md-3 col-lg-2 d-md-block bg-light sidebar collapse'
          style={{ float: 'left' }}
        >
          <ul class='nav nav-pills nav-flush flex-column mb-auto text-center'>
            <li>
              <a
                href='/login'
                class='nav-link active py-3 border-bottom'
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
                class='nav-link active py-3 border-bottom'
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
                class='nav-link py-3 border-bottom'
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
                class='nav-link py-3 border-bottom'
                title='About'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                About
              </a>
            </li>
            <li>
              <a
                href='/contact'
                class='nav-link py-3 border-bottom'
                title='Contact'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                Contact
              </a>
            </li>
            <li>
              <a
                href='/newCreateAccount'
                class='nav-link active py-3 border-bottom'
                aria-current='page'
                title='signup'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                New Sign up
              </a>
            </li>
            <li>
              <a
                href='/nomatch'
                class='nav-link py-3 border-bottom'
                title='nomatch'
                data-bs-toggle='tooltip'
                data-bs-placement='right'
              >
                {' '}
              </a>
            </li>
            <li>
              <a
                href='/nomatch'
                class='nav-link py-3 border-bottom'
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
                class='nav-link py-3 border-bottom'
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
                class='nav-link py-3 border-bottom'
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
              class='nav-link py-3 border-bottom'
              title='Account'
              data-bs-toggle='tooltip'
              data-bs-placement='right'
            >
              Account
            </a>
          </ul>
        </nav>
        <main class='col-md-9 ms-sm-auto col-lg-11'>
          {/* An <Outlet> renders whatever child route is currently active,
            so you can think about this <Outlet> as a placeholder for
            the child routes we defined above. */}
          <Outlet />
        </main>
      </div>
    </div>
  </React.Fragment>
)
