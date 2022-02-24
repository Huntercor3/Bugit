import React, { useState, useEffect } from "react";
import axios from "axios";
function Login1(props) {
  const [user, setuser] = useState({ Email: "", Password: "" });
  const apiUrl = "http://localhost:1680/api/user/Login";
  const Login = (e) => {
    e.preventDefault();
    debugger;
    const data = { Email: user.Email, Password: user.Password };
    axios.post(apiUrl, data).then((result) => {
      debugger;
      console.log(result.data);
      const serializedState = JSON.stringify(result.data.UserDetails);
      var a = localStorage.setItem("myData", serializedState);
      console.log("A:", a);
      const user = result.data.UserDetails;
      console.log(result.data.message);
      if (result.data.status == "200") props.history.push("/home");
      else alert("Invalid User");
    });
  };

  const onChange = (e) => {
    e.persist();
    debugger;
    setuser({ ...user, [e.target.name]: e.target.value });
  };
  return (
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-xl-10 col-lg-12 col-md-9">
          <div class="card o-hidden border-0 shadow-lg my-5">
            <div class="card-body p-0">
              <div class="row">
                <div class="col-lg-6 d-none d-lg-block bg-login-image"></div>
                <div class="col-lg-6">
                  <div class="p-5">
                    <div class="text-center">
                      <h1 class="h4 text-gray-900 mb-4">Welcome Back!</h1>
                    </div>
                    <form onSubmit={Login} class="user">
                      <div class="form-group">
                        <input
                          type="email"
                          class="form-control"
                          value={user.Email}
                          onChange={onChange}
                          name="Email"
                          id="Email"
                          aria-describedby="emailHelp"
                          placeholder="Enter Email"
                        />
                      </div>
                      <div class="form-group">
                        <input
                          type="password"
                          class="form-control"
                          value={user.Password}
                          onChange={onChange}
                          name="Password"
                          id="DepPasswordartment"
                          placeholder="Password"
                        />
                      </div>
                      {/* <div class="form-group">  
                          <div class="custom-control custom-checkbox small">  
                            <input type="checkbox" class="custom-control-input" id="customCheck"/>  
                            <label class="custom-control-label" for="customCheck">Remember Me</label>  
                          </div>  
                        </div> */}
                      <button type="submit" className="btn btn-info mb-1" block>
                        <span>Login</span>
                      </button>
                      <hr />
                    </form>
                    <hr />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Login1;
