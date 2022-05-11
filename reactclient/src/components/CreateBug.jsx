import React, { useState } from 'react'
import { Navigate } from 'react-router-dom'
import './CSS/CreateBug.css'
import BugItLogo from './images/BugItLogo.png'
import { Editor } from '@tinymce/tinymce-react'
import Select from 'react-select'

//  const priorityOptions=[
// {label:'High', value:'high'},
// {label:'Moderate', value:'moderate'},
// {label:'Low', value:'low'}
// ]

// const typeOptions=[
// {label:'Optimize', value:'optimize'},
// {label:'Crash', value:'crash'},
// {label:'Upgrade', value:'upgrade'}
// ]

// const statusOptions=[
// {label:'In progress', value:'inProgress'},
// {label:'Stuck', value:'stuck'}
// ]

const CreateBug = () => {
  const [creator, setCreator] = useState(0)
  const [timeCreated, setDate] = useState('')
  const [type, setType] = useState('')
  const [status, setStatus] = useState('')
  const [priority, setPriority] = useState('')
  const [estimatedTime, setEstimatedTime] = useState('')
  const [description, setBugDescription] = useState('')
  const [redirect, setRedirect] = useState(false)
  const submit = async (e) => {
    e.preventDefault()

    await fetch('https://bugitserver.azurewebsites.net/create-bug', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include',
      body: JSON.stringify({
        creator: creator,
        timeCreated: timeCreated,
        type: type,
        status: status,
        priority: priority,
        estimatedTime: estimatedTime,
        description: description,
      }),
    }).then(function(response) {
      if (response.status === 200) setRedirect(true)
      else alert('Invalid credientials, please try again')
    })
  }
  if (redirect) return <Navigate to='/newHome' />

  /*onChangeFunc=({value}) =>
  {
    (e) => {value(e.target.value)}
  }
*/
  // function handleChange(e){
  //   this.setState({id:e.value, name:e.label})
  //  }

  //////////////////////////////////REMOVE HTML FROM OUTPUT//////////////////////////////////////////
  function removeHTML(str) {
    var tmp = document.createElement('DIV')
    tmp.innerHTML = str
    return tmp.textContent || tmp.innerText || ''
  }
  //////////////////////////////////REMOVE HTML FROM OUTPUT//////////////////////////////////////////

  function customTheme(theme) {
    return{
      ...theme,
      colors: {
        ...theme.colors,
        primary25: '#ccfff2',
        primary: '#4abdac',
        dangerLight:"blue"
      }
    }
  }

  const priorityOptions = [
    { label: "High", value: "High" },
    { label: "Moderate", value: "Moderate"},
    { label: "Low", value: "Low" },
  ];

  const typeOptions = [
    { label: "Optimize", value: "Optimize" },
    { label: "Crash", value: "Crash" },
    { label: "Upgrade", value: "Upgrade" },
  ];

  const statusOptions = [
    { label: "In progress", value: "In progress" },
    { label: "Stuck", value: "Stuck" },
  ];

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
          {/* <div className='col-sm-12 btn btn-primary' style={{ margin: '6px' }}>
          Add New Contact
        </div> */}
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
                    <label className='h2 form-label'>Create a New Bug</label>
                  </div>
                  <form onSubmit={submit} className='user'>
                    <div className='form-group row'>
                      <div className='col-sm-6 mb-3 mb-sm-0'>
                        <label className='h5 form-label'>Owner</label>
                        <input
                          type='text'
                          required
                          onChange={(e) => setCreator(e.target.value)}
                          className='form-control'
                          placeholder='Owner'
                        />
                      </div>
                      <div className='col-sm-6'>
                        <label className='h5 form-label'>Priority</label>
                        
                        <Select
                          options={priorityOptions}
                          theme={customTheme}
                          onChange={(e) => setPriority(e.value)}
                          placeholder='Set Priority'
                        />
                        
                      </div>
                      
                    </div>
                    <div className='form-group row'>
                      <div className='col-sm-6 '>
                        <label className='h5 form-label'>Type</label>
                        
                        <Select
                          options={typeOptions}
                          theme={customTheme}
                          onChange={(e) => setType(e.value)}
                          placeholder='Set Type'
                        />
                        
                      </div>
                      <div className='col-sm-6 '>
                        <label className='h5 form-label'>Status</label>
                        <div class="react-select-container">
                        <Select
                          options={statusOptions}
                          theme={customTheme}
                          onChange={(e) => setStatus(e.value)}
                          placeholder='Set status'
                        />
                        </div>
                      </div>
                    </div>
                    <div className='form-group row'>
                    <div className='col-sm-6'>
                        <label className='h5 form-label'>Date</label>
                        <input
                          type='date'
                          required
                          onChange={(e) => setDate(e.target.value)}
                          className='form-control'
                          placeholder='Todays date'
                        />
                      </div>
                      <div className='col-sm-6'>
                        <label className='h5 form-label'>Estimated Time</label>
                        <input
                          type='text'
                          required
                          onChange={(e) => setEstimatedTime(e.target.value)}
                          className='form-control'
                          placeholder='Estimated Time'
                        />
                      </div>
                      <div className='col-sm-12'>
                        <label className='h5 form-label'>Bug Description</label>
                        <Editor
                          apiKey='i8eqch0ybta5qyoxntbm1vqssmljsl9w4z83li4ia3wv64t3'
                          referrerpolicy='origin'
                          required
                          init={{
                            selector: 'textarea#default-editor',
                            height: 175,
                            menubar: false,
                            format: 'text',
                            toolbar: 'undo redo',
                            browser_spellcheck: true,
                          }}
                          onEditorChange={(t) =>
                            setBugDescription(removeHTML(t))
                          }
                          className='form-control'
                          placeholder={description}
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
    </React.Fragment>
  )
}
export default CreateBug
