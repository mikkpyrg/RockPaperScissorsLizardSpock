# Rock paper scissors spock lizard game
Simple API to play against the computer or against another player in "mail order" fashion (you make your move and you'll have to check for the results yourself later)
## FE
Made in React. Made the most simplest I could, while still keeping some semblence of UI
### Instructions
```console
cd fe
npm i
npm start
```
### Notes/TODO if this wasn't a test assignment
- should probably use typescript and use actual models
- add .env to gitignore. Currently more convinient for reviewer, as that's where the config is to the API location
- create proper low level components, though it was fun building things out of default html elements
- use scss or other preprocessors
- make it look pretty
- loader spinner
- paging + table
- some validation for the FE so can't choose cpu as name
- proper API exception handling
- better API response handling
- config to keep all the API URLs

## BE
simple API in .Net 8 with an in-memory database
### Instructions
Because it's using an in-memory database, it should work without any configuration, when you run it in visual studio.
### Notes/TODO if this wasn't a test assignment
- Microservice aspect was a little bit unclear in the current circumstance, given there's only a simple API. I suppose the service can be separated into it's own service, but didn't see it necessary currently. Thus I didn't build it with that in mind.
- Given the simple state of the project, it is trivial to create a docker file. Visual Studio can generate for you even. Thus I chose not to do it.
- For the sake of a simple setup, the database is in memory. Easy to change to proper storage. Will probably require generating migration files.
- For the multiplayer part I used header to send the preferred name to the BE, so the required game service endpoints stayed like requested. Obviously this isn't proper authentication, but it's probably also not needed here.
- Messed up on the solution naming scheme, should've been Be.Api, but not worth refactoring everything for a test assignment.
- Could've used paging, validation(so "cpu" would error at least?), proper logging, exception handling