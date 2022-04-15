import React, { useState, useEffect } from "react";
import { Navigate } from "react-router-dom";

export default function BugUpdateForm() {
  //////////////////////////////////GET BUG BY ID//////////////////////////////////////////
  //
  //set bugIDToSearch to the URL location
  var bugIDToSearch = window.location.pathname;
  //set bugIDToSearch to the bug ID       deletes "/showbug/"
  bugIDToSearch = bugIDToSearch.substring(11);
  console.log(bugIDToSearch);

  var myInit = {
    method: "POST",
    Headers: {
      "Content-Type": "application/json",
    },
    mode: "cors",
    cache: "default",
  };

  const getBugUrl = `${"https://localhost:7075/get-bug-by-bug-id"}/${bugIDToSearch}`;

  let myRequest = new Request(getBugUrl, myInit);

  const [bugData, setBugData] = useState({});
  const [bugCurrentlyBeingUpdated, setBugCurrentlyBeingUpdated] = useState({});
  async function getBugById() {
    fetch(getBugUrl, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((bugFromServer) => {
        setBugData(bugFromServer);
      })
      .then(function(data) {
        console.log(data);
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
  const bugId = bugIDToSearch;
  const [creator, setOwner] = useState(0);
  const [description, setBugDescription] = useState("");
  const [type, setType] = useState("");
  const [status, setStatus] = useState("");
  const [priority, setPriority] = useState("");
  const [estimatedTime, setEstimatedTime] = useState("");

  const [redirect, setRedirect] = useState(false);

  const submit = async (e) => {
    e.preventDefault();

    await fetch("https://localhost:7075/update-bug", {
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
  if (redirect) return <Navigate to="/#home" />;

  //////////////////////////////////UPDATE BUG//////////////////////////////////////////

  return (
    <form className="w-100 px-5">
      <h1 className="mt-5">Updating the Bug with ID: {bugIDToSearch}</h1>
      <div className="form-group row">
        <div className="col-sm-6">
          <label className="h3 form-label">Owner</label>
          <input
            type="text"
            required
            onChange={(e) => setOwner(e.target.value)}
            className="form-control"
            placeholder={bugData.creator}
          />
        </div>

        <div className="col-sm-6">
          <label className="h3 form-label">Type</label>
          <input
            type="text"
            required
            onChange={(e) => setType(e.target.value)}
            className="form-control"
            placeholder={bugData.type}
          />
        </div>
      </div>
      <div className="form-group row">
        <div className="col-sm-6">
          <label className="h3 form-label">Status</label>
          <input
            type="text"
            required
            onChange={(e) => setStatus(e.target.value)}
            className="form-control"
            placeholder={bugData.status}
          />
        </div>

        <div className="col-sm-6">
          <label className="h3 form-label">Priority</label>
          <input
            type="text"
            required
            onChange={(e) => setPriority(e.target.value)}
            className="form-control"
            placeholder={bugData.priority}
          />
        </div>
      </div>

      <div className="form-group row">
        <div className="col-sm-6">
          <label className="h3 form-label">Esitmated Time</label>
          <input
            type="text"
            required
            onChange={(e) => setEstimatedTime(e.target.value)}
            className="form-control"
            placeholder={bugData.estimatedTime}
          />
        </div>

        <div className="col-sm-6">
          <label className="h3 form-label">Description</label>
          <input
            type="text"
            required
            onChange={(e) => setBugDescription(e.target.value)}
            className="form-control"
            placeholder={bugData.description}
          />
        </div>
      </div>
      <div className="form-group row">
        <button
          onClick={submit}
          className="text-center btn btn-md btn-Orange form-control"
        >
          Submit
        </button>
        <div>
          <a
            href="/"
            className="text-center btn btn-md btn-second form-control"
            type="submit"
          >
            Cancel
          </a>
        </div>
      </div>
    </form>
  );
}
