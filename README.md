# Bugit

Bugit is a bug tracking platform for keeping track of reported bugs, any information regarding the bug, and the progress along with it. The following tasks overlay functionality:
 - Create Account
 - User Login
 - View list of bugs
 - Create bug
 - Edit bug
 - Remove bug
 - More features TBA ... 

## Links

[Website](https://purple-ground-019dc9c0f.1.azurestaticapps.net/)

[Live Swagger API](https://bugitserver.azurewebsites.net/swagger)

## Announcement

The latest build of the platform is currently still in Alpha utilizing ASP.NET 6.0 and React.js. Use of Bugit will be detailed when a beta release is available.

## Issues

Please open any issues within the GitHub repository as they are observed or if anything is missing or needed clearer documentation.

## Viewing Front-End Locally / Installation

Use [npm](https://docs.npmjs.com/cli/v8/commands/npm-install) to install needed dependencies to view the front-end locally.

```bash
npm install
```

```bash
npm start
```

## Usage

Bugit is a web app that is based off the backend framework ASP.NET 6.0 and utilizes React.js for the front-end UI.

In order to run the program you will need to have an IDE that can run ASP.NET 6.0 such as Visual Studio 2022. To run the front-end you will need a javascript/HTML/css editor such as Visual Studio Code.

With the front-end communicating with the back-end the project utilizes the Swagger API to interact with end-points to test them for functionality.

## Currently Supported Endpoints

#### User Endpoints
- POST: Login Controller
  - Logs in a user by user entered username/email and password.
- POST: Register Controller
  - Creates a new user from user/UI entered information.
- GET: Get Cookie
  - Gets the cookies associated with the user session.
- GET: Get UserID by First/Last Name
  - Get UserID by user/UI entered First and Last name
- DELETE: Logout Controller
  - Logs out the user and sets all current cookies to be expired and discarded.

#### Bug Endpoints
- POST: Add Bug to Project by ProjectID
  - Adds a ProjectID to the Bug in the database.
- POST: Create Bug
  - Inserts a new bug into the database.
- POST: Update Bug
  - Updates a Bug by ID entered by the user/UI
- POST: Get all Bugs
  - Gets all the bugs that are in the database.
- GET: Get Bug by BugID
  - Gets a bug by user/UI entered BugID.
- GET: Get bug Comment by ID
  - Gets a bug comment by BugID
- GET: Get Bugs by ProjectID
  - Get all bugs associated with user/UI entered ProjectID
- DELETE: Delete Bug by ID
  - Deleted bug by user/UI entered ID
 
#### Project Endpoints
 - POST: Add new Project by Project Name
   - Adds a Project to the database and assigns it a ProjectID
 - GET: Get Bugs in Project by ID
   - Gets bugs by user/UI entered ProjectID

## Contributors
[Hunter](https://github.com/Huntercor3)
[Jonas](https://github.com/JonasWalker)
[Jarod](https://github.com/JManness71)
[Benjamin](https://github.com/BenjKind)

## License
[MIT](https://choosealicense.com/licenses/mit/)
