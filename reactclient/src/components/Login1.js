import React, { useState, useEffect } from "react";
import axios from "axios";
import { Navigate } from "react-router-dom";

const Login1 = () => {
  const [emailAddress, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [redirect, setRedirect] = useState(false);
  const submit = async (e) => {
    e.preventDefault();

    await fetch("https://localhost:7075/login", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      credentials: "omit",
      body: JSON.stringify({
        emailAddress: emailAddress,
        password: password,
      }),
    });

    setRedirect(true);
  };
  if (redirect) {
    return <Navigate to="/about" />;
  }

  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="col-xl-10 col-lg-12 col-md-9">
          <div className="card o-hidden border-0 shadow-lg my-5">
            <div className="card-body p-0">
              <div className="row">
                <div className="col-lg-6 d-none d-lg-block bg-login-image"></div>
                <div className="col-lg-6">
                  <div className="p-5">
                    <div className="text-center">
                      <h1 className="h4 text-gray-900 mb-4">Welcome Back!</h1>
                    </div>
                    <form onSubmit={submit} className="user">
                      <div className="form-group">
                        <input
                          type="email"
                          className="form-control"
                          required
                          onChange={(e) => setEmail(e.target.value)}
                          placeholder="Enter Email"
                        />
                      </div>
                      <div className="form-group">
                        <input
                          type="password"
                          className="form-control"
                          onChange={(e) => setPassword(e.target.value)}
                          placeholder="Password"
                        />
                      </div>

                      <button type="submit" className="btn btn-info mb-1">
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
};

export default Login1;
