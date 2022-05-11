import React, { useState, useEffect } from "react";
import { Navigate } from "react-router-dom";
import { Editor } from "@tinymce/tinymce-react";
import Select from "react-select";
import "./CSS/CreateBug.css";
import BugItLogo from "./images/BugItLogo.png";
import { Modal, Button, Form } from "react-bootstrap";
export default function BugUpdateForm() {
  //////////////////////////////////GET BUG BY ID//////////////////////////////////////////
  //

  //set bugIDToSearch to the URL location
  var bugIDToSearch = window.location.pathname;
  //set bugIDToSearch to the bug ID       deletes "/showbug/"
  bugIDToSearch = bugIDToSearch.substring(11);
  console.log("Bug ID to Seach: ", bugIDToSearch);

  var myInit = {
    method: "POST",
    Headers: {
      "Content-Type": "application/json",
    },
    mode: "cors",
    cache: "default",
  };

  const getBugUrl = `${"https://bugitserver.azurewebsites.net/get-bug-by-bug-id"}/${bugIDToSearch}`;

  let myRequest = new Request(getBugUrl, myInit);

  const [bugData, setBugData] = useState({});
  const [bugCurrentlyBeingUpdated, setBugCurrentlyBeingUpdated] = useState({});
  const bugId = bugIDToSearch;
  const [creator, setOwner] = useState(0);
  const [description, setBugDescription] = useState("");
  const [type, setType] = useState("");
  const [status, setStatus] = useState("");
  const [priority, setPriority] = useState("");
  const [estimatedTime, setEstimatedTime] = useState("");
  const [redirect, setRedirect] = useState(false);
  async function getBugById() {
    await fetch(getBugUrl, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((bugFromServer) => {
        setBugData(bugFromServer);
        setPriority(bugFromServer.priority);
        setOwner(bugFromServer.creator);
        setType(bugFromServer.type);
        setEstimatedTime(bugFromServer.estimatedTime);
        setStatus(bugFromServer.status);
        setBugDescription(bugFromServer.description);
      })
      .then(function(data) {
        console.log("Data: ", data);
      });
  }

  useEffect(() => {
    getBugById({});
  }, []);

  //////////////////////////////////GET BUG BY ID//////////////////////////////////////////

  /* const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const bugToUpdate = {
      // bugId: props.bug.bugId,
      creator: formData.creator,
      timeCreated: formData.timeCreated,
      type: formData.type,
      status: formData.status,
      priority: formData.priority,
      estimatedTime: formData.estimatedTime,
      description: formData.description,
    };

    fetch("https://localhost:7075/update-bug", {
      method: "Post",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(bugToUpdate),
    })
      .then((response) => response.json())
      .then((responseFromServer) => {
        console.log(responseFromServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });

    props.onBugUpdated(bugToUpdate);
  };
*/

  //////////////////////////////////UPDATE BUG//////////////////////////////////////////

  const submit = async (e) => {
    e.preventDefault();

    await fetch("https://bugitserver.azurewebsites.net/update-bug", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
      body: JSON.stringify({
        bugId: bugId,
        creator: creator,
        description: description,
        type: type,
        status: status,
        priority: priority,
        estimatedTime: estimatedTime,
      }),
    }).then(function(response) {
      console.log(response.status);
      if (response.status === 200) setRedirect(true);
      else alert("Invalid credientials, please try again");
    });
    console.log(
      bugId,
      creator,
      type,
      status,
      priority,
      estimatedTime,
      description
    );
  };
  if (redirect) return <Navigate to="/newHome" />;

  //////////////////////////////////UPDATE BUG//////////////////////////////////////////

  function customTheme(theme) {
    return {
      ...theme,
      colors: {
        ...theme.colors,
        primary25: "#ccfff2",
        primary: "#4abdac",
        dangerLight: "blue",
      },
    };
  }

  const priorityOptions = [
    { label: "High", value: "High" },
    { label: "Moderate", value: "Moderate" },
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
    { label: "Completed", value: "Completed" },
  ];

  //////////////////////////////////REMOVE HTML FROM OUTPUT//////////////////////////////////////////
  function removeHTML(str) {
    var tmp = document.createElement("DIV");
    tmp.innerHTML = str;
    return tmp.textContent || tmp.innerText || "";
  }
  //////////////////////////////////REMOVE HTML FROM OUTPUT//////////////////////////////////////////
  return (
    <React.Fragment>
      <div className="container">
        <div className="row">
          <img
            className="logo"
            rel="icon"
            src={BugItLogo}
            alt="Logo"
            width="100px"
            height="100px"
          />
          {/* <div className='col-sm-12 btn btn-primary' style={{ margin: '6px' }}>
          Add New Contact
        </div> */}
        </div>
        <div
          className="card-b o-hidden border-0 shadow-lg my-5"
          style={{ marginTop: "5rem!important" }}
        >
          <div className="card-body-b p-0">
            <div className="row">
              <div className="col-lg-12">
                <div className="p-5-b">
                  <div className="text-center">
                    <label className="h2 form-label">
                      Updating the Bug with ID: {bugIDToSearch}
                    </label>
                  </div>
                  <form onSubmit={submit} className="user">
                    <div className="form-group row">
                      <div className="col-sm-6 mb-3 mb-sm-0">
                        <label className="h5 form-label">Owner</label>
                        <input
                          type="text"
                          required
                          defaultValue={bugData.creator}
                          onChange={(e) => setOwner(e.target.value)}
                          className="form-control"
                          //placeholder={bugData.creator}
                        />
                      </div>
                      <div className="col-sm-6">
                        <label className="h5 form-label">Type</label>
                        {/* <input
                          type="text"
                          required
                          defaultValue={bugData.type}
                          onChange={(e) => setType(e.target.value)}
                          className="form-control"
                          //placeholder={bugData.creator}
                        /> */}
                        <Select
                          options={typeOptions}
                          theme={customTheme}
                          onChange={(e) => setType(e.value)}
                          placeholder={bugData.type}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <div className="col-sm-6 ">
                        <label className="h5 form-label">Status</label>
                        {/* <input
                          type="text"
                          required
                          defaultValue={bugData.status}
                          onChange={(e) => setStatus(e.target.value)}
                          className="form-control"
                          //placeholder={bugData.status}
                        /> */}
                        <Select
                          options={statusOptions}
                          theme={customTheme}
                          onChange={(e) => setStatus(e.value)}
                          placeholder={bugData.status}
                        />
                        {/* <Select
                       options={this.state.typeOptions} 
                       onChange = {setStatus(this.handleChange.bind(this))}
                       placeholder = 'Set Type' 
                       />     */}
                      </div>
                      <div className="col-sm-6 ">
                        <label className="h5 form-label">Priority</label>
                        {/* <input
                          type="text"
                          required
                          defaultValue={bugData.priority}
                          onChange={(e) => setPriority(e.target.value)}
                          className="form-control"
                          //placeholder={bugData.priority}
                        /> */}
                        {/*<Select
                       options={statusOptions} 
                       onChange = {setStatus}
                       placeholder = 'Set status'
                       />  */}
                        <Select
                          options={priorityOptions}
                          theme={customTheme}
                          onChange={(e) => setPriority(e.value)}
                          placeholder={bugData.priority}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <div className="col-sm-6">
                        <label className="h5 form-label text-center">
                          Estimated Time
                        </label>
                        <input
                          type="text"
                          required
                          defaultValue={bugData.estimatedTime}
                          onChange={(e) => setEstimatedTime(e.target.value)}
                          className="form-control"
                          //placeholder={bugData.estimatedTime}
                        />
                      </div>
                      <div className="col-sm-12">
                        <label className="h5 form-label">Bug Description</label>
                        <textarea
                          required
                          defaultValue={bugData.description}
                          onChange={(e) => setBugDescription(e.target.value)}
                          className="form-control"
                        ></textarea>
                        {/* <Editor
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
                        /> */}
                      </div>
                    </div>
                    <div className="row justify-content-center">
                      <Button
                        className="text-center btn btn-md btn-primary"
                        type="submit"
                        onClick={submit}
                      >
                        Update
                      </Button>
                      <Button
                        variant="secondary"
                        className="text-center btn btn-md btn-cancel"
                        type="submit"
                      >
                        Cancel
                      </Button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </React.Fragment>
    // <div className='card-b o-hidden border-0 shadow-lg my-5'>
    //   <div className='card-body-b p-0'>
    //     <form className='w-100 px-5'>
    //       <h1 className='mt-5'>Updating the Bug with ID: {bugIDToSearch}</h1>
    //       <div className='form-group row'>
    //         <div className='col-sm-6'>
    //           <label className='h3 form-label'>Owner</label>
    //           <input
    //             id='OwnerInput'
    //             type='text'
    //             required
    //             defaultValue={bugData.creator}
    //             //onChange={(e) => setOwner(e.target.value)}
    //             className='form-control'
    //             //placeholder={bugData.creator}
    //           />
    //         </div>
    //         <div className='col-sm-6'>
    //           <label className='h3 form-label'>Type</label>
    //           <input
    //             id='TypeInput'
    //             type='text'
    //             required
    //             defaultValue={bugData.type}
    //             //onChange={(e) => setOwner(e.target.value)}
    //             className='form-control'
    //             //placeholder={bugData.creator}
    //           />
    //         </div>
    //       </div>
    //       <div className='form-group row'>
    //         <div className='col-sm-6'>
    //           <label className='h3 form-label'>Status</label>
    //           <input
    //             id='StatusInput'
    //             type='text'
    //             required
    //             defaultValue={bugData.status}
    //             //onChange={(e) => setStatus(e.target.value)}
    //             className='form-control'
    //             //placeholder={bugData.status}
    //           />
    //         </div>

    //         <div className='col-sm-6'>
    //           <label className='h3 form-label'>Priority</label>
    //           <input
    //             id='PriorityInput'
    //             type='text'
    //             required
    //             defaultValue={bugData.priority}
    //             //onChange={(e) => setPriority(e.target.value)}
    //             className='form-control'
    //             //placeholder={bugData.priority}
    //           />
    //         </div>
    //       </div>

    //       <div className='form-group row'>
    //         <div className='col-sm-6'>
    //           <label className='h3 form-label text-center'>
    //             Estimated Time
    //           </label>
    //           <input
    //             id='EstimatedTimeInput'
    //             type='text'
    //             required
    //             defaultValue={bugData.estimatedTime}
    //             //onChange={(e) => setEstimatedTime(e.target.value)}
    //             className='form-control'
    //             //placeholder={bugData.estimatedTime}
    //           />
    //         </div>
    //         <div className='col-sm-6'>
    //           <label className='h3 form-label'>Description</label>
    //           <textarea
    //             id='DescriptionInput'
    //             required
    //             defaultValue={bugData.description}
    //             className='form-control'
    //           ></textarea>
    //           {/* <Editor
    //         //textareaName="Description"
    //         //id="BugDescriptionInput"
    //         apiKey='i8eqch0ybta5qyoxntbm1vqssmljsl9w4z83li4ia3wv64t3'
    //         referrerpolicy='origin'
    //         required
    //         initialValue={bugData.description}
    //         init={{
    //           selector: 'textarea#default-editor',
    //           height: 175,
    //           menubar: false,
    //           format: 'text',
    //           toolbar: 'undo redo',
    //           browser_spellcheck: true,
    //         }}
    //         onEditorChange={(t) => setBugDescription(removeHTML(t))}
    //         className='form-control'
    //         //placeholder={bugData.description}
    //       /> */}
    //         </div>
    //       </div>
    //       <div className='form-group row'>
    //         <button
    //           onClick={submit}
    //           className=' text-center btn-md btn-Orange form-control '
    //         >
    //           Submit
    //         </button>
    //         <div>
    //           <a
    //             href='/'
    //             className='text-center btn btn-md btn-second form-control'
    //             type='submit'
    //           >
    //             Cancel
    //           </a>
    //         </div>
    //       </div>
    //     </form>
    //   </div>
    // </div>
  );
}
