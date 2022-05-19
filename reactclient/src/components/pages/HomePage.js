import React from 'react'
import BugItLogo from '../images/BugItLogo.png'
const HomePage = () => {
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
                      Projects page coming Second quarter of 2022{' '}
                    </label>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </React.Fragment>
  )
}

export default HomePage
