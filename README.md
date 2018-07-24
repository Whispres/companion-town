# companion-town
HTTP API which could be used to power a simple virtual pet style game

### Build
Integration with TravisCI - runs build (Release) and run the tests
[![Build Status](https://travis-ci.org/oandreeeee/companion-town.svg?branch=master)](https://travis-ci.org/oandreeeee/companion-town)

### Development options 
* Use of [LiteDB](http://www.litedb.org/) in order to remove external dependencies
* Soft use of Repository pattern
* Small factory pattern to create `Animal`'s
* async implementation (LiteDb not compatible)
* Use of SOLID Principles
* Hangfire to perform background processing (also integrated with LiteDB)

### Future Improvements
* Improve repository with lazy loading
* Middleware to handler unxpected errors
* Improve swagger documentation (ex.: add enums to endpoint)
* Create a pagination for `GET`'s
* Separate objects (domain vs dto's vs view model)

### Unit Tests
Made only over the `UserService` as example. Focus in cover all the corner cases instead of focus on the code coverage
