import React, { useState } from "react";

export default function BugUpdateForm(props) {
  const initialFormData = Object.freeze({
    creator: props.bug.creator,
    timeCreated: props.bug.timeCreated,
    type: props.bug.type,
    status: props.bug.status,
    priority: props.bug.priority,
    estimatedTime: props.bug.estimatedTime,
    description: props.bug.description,
  });

  const [formData, setFormData] = useState(initialFormData);

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const bugToUpdate = {
      bugId: props.post.postId,
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
      <div className="form-group row">
        <div className="col-sm-6 mb-3 mb-sm-0">
          <input
            type="text"
            required
            name="creator"
            value={formData.creator}
            onChange={handleChange}
            className="form-control"
            placeholder="Owner"
          />
        </div>
        <div className="col-sm-6">
          <input
            type="date"
            required
            name="Date"
            value={formData.timeCreated}
            onChange={handleChange}
            className="form-control"
            placeholder="Date"
          />
        </div>
      </div>
      <div className="form-group row">
        <div className="col-sm-6 ">
          <input
            type="text"
            required
            name="type"
            value={formData.type}
            onChange={handleChange}
            className="form-control"
            placeholder="type"
          />

          {/* <Select
                       options={this.state.typeOptions} 
                       onChange = {setStatus(this.handleChange.bind(this))}
                       placeholder = 'Set Type' 
                       />     */}
        </div>
        <div className="col-sm-6 ">
          <input
            type="text"
            required
            name="Status"
            value={formData.status}
            onChange={handleChange}
            className="form-control"
            placeholder="Status"
          />
          {/*<Select
                       options={statusOptions} 
                       onChange = {setStatus}
                       placeholder = 'Set status'
                       />  */}
        </div>
      </div>
      <div className="form-group row">
        <div className="col-sm-6">
          <input
            type="text"
            required
            name="Priority"
            value={formData.priority}
            onChange={handleChange}
            className="form-control"
            placeholder="Priority"
          />

          {/*<Select
                       options={priorityOptions} 
                       onChange = {setPriority}
                       placeholder = 'Set Priority'                      
                       /> */}
        </div>
        <div className="col-sm-6">
          <input
            type="text"
            required
            name="Estimated Time"
            value={formData.estimatedTime}
            onChange={handleChange}
            className="form-control"
            placeholder="Estimated Time"
          />
        </div>
        <div className="col-sm-12">
          <input
            type="text"
            required
            name="Bug Description"
            value={formData.description}
            onChange={handleChange}
            className="form-control"
            placeholder="Bug Description"
          />
        </div>
      </div>
      <div className="row justify-content-center">
        <button
          className="text-center btn btn-md btn-primary"
          onClick={handleSubmit}
        >
          Submit
        </button>
      </div>
    </form>
  );
}
