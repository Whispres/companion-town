# companion-town
HTTP API which could be used to power a simple virtual pet style game

### Architecture options 
* Use of [LiteDB](http://www.litedb.org/) in order to remove external dependencies
* Repository pattern in order to easily change to other db engine
* async implementation (LiteDb not compatible)

### Future Improvements
* Improve repository with lazy loading
* Middleware to handler unxpected errors
* Improve swagger documentation (ex.: add enums to endpoint)

