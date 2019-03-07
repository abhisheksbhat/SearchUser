Instructions for setting up SearchUserAPI


1. Download or clone the source code 

2. Navigate to Setup folder and run "scripts.sql" to setup UserDb Database

3. Change the connection strings in appsettings.json configuration file

4. Build the code
5. Run the application

6. If application url is say localhost:50851  
	Then, access localhost:50851/swagger which will launch swagger ui for          testing purpose 

7. If not use below url formats for testing the functionality
 State          -locahost:50851/api/searchuser/state/ny
 Age Range      -locahost:50851/api/searchuser/agerange/15-25
 State&AgeRange -locahost:50851/api/searchuser/stateandagerange/NJ/15-25
 StateOrAgeRange-locahost:50851/api/searchuser/stateoragerange/MO/15-25

Change State and Age parameters as appropriate

