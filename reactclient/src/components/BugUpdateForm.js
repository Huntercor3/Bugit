import React, { useState } from "react";

export default function BugUpdateForm(props) {
  const initialFormData = Object.freeze({
    bugId: "4",
    creator: "This",
    timeCreated: "Project",
    type: "Project",
    status: "Needs",
    priority: "It's",
    estimatedTime: "Own",
    description: "BugTracker",
  });

  const [formData, setFormData] = useState(initialFormData);

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  //set bugIDToSearch to the URL location
  var bugIDToSearch = window.location.pathname;
  //set bugIDToSearch to the bug ID       deletes "/showbug/"
  bugIDToSearch = bugIDToSearch.substring(11);
  console.log(bugIDToSearch);
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

  return (
    <form className="w-100 px-5">
      <h1 className="mt-5">Updating the Bug with ID: {bugIDToSearch}</h1>
      <div className="form-group row">
        <div className="col-sm-6">
          <label className="h3 form-label">Owner</label>
          <input
            value={formData.creator}
            name="title"
            type="text"
            className="form-control"
            onchange={handleChange}
          />
        </div>

        <div className="col-sm-6">
          <label className="h3 form-label">Type</label>
          <input
            value={formData.type}
            name="content"
            type="text"
            className="form-control"
            onchange={handleChange}
          />
        </div>
      </div>
      <div className="form-group row">
        <div className="col-sm-6">
          <label className="h3 form-label">Status</label>
          <input
            value={formData.status}
            name="content"
            type="text"
            className="form-control"
            onchange={handleChange}
          />
        </div>

        <div className="col-sm-6">
          <label className="h3 form-label">Priority</label>
          <input
            value={formData.priority}
            name="content"
            type="text"
            className="form-control"
            onchange={handleChange}
          />
        </div>
      </div>

      <div className="form-group row">
        <div className="col-sm-6">
          <label className="h3 form-label">Esitmated Time</label>
          <input
            value={formData.estimatedTime}
            name="content"
            type="text"
            className="form-control"
            onchange={handleChange}
          />
        </div>

        <div className="col-sm-6">
          <label className="h3 form-label">Description</label>
          <input
            value={formData.description}
            name="content"
            type="text"
            className="form-control"
            onchange={handleChange}
          />
        </div>
      </div>
      <div className="form-group row">
        <button
          onClick={handleSubmit}
          className="text-center btn btn-md btn-Orange form-control"
        >
          Submit
        </button>
        <div>
          <a
            href="/"
            className="text-center btn btn-md btn-second form-control"
            type="submit"
            onClick={() => props.onPostUpdated(null)}
          >
            Cancel
          </a>
        </div>
      </div>
    </form>
  );
}
